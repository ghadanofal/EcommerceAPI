using Ecommerce.Core.IRepositories;
using Ecommerce.Core.Models;
using Ecommerce.Infastructure.Data;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ecommerce.Core.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWork<Product> unitOfWork;
        private readonly IMapper mapper;
        public ApiResponse response;

        public ProductController(ApplicationDbContext context, IUnitOfWork<Product> unitOfWork, IMapper mapper)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            response = new ApiResponse();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]
        public async Task<ActionResult<ApiResponse>> GetAllProduct([FromQuery]string? category_name = null, int pageSize = 2 , int pageNumber= 1)
        {
            Expression<Func<Product, bool>> filter = null;
            if (!string.IsNullOrEmpty(category_name))
            {
                filter = x => x.categories.Name.Contains(category_name);
            }
            var models = await unitOfWork.productRepository.GetAll(filter : filter ,page_Size: pageSize, page_Number: pageNumber,
                includeProperity : "categories");
            var check = models.Any();
            if (check)
            {
                response.StatusCode = 200;
                response.IsSuccess = check;
                var mappedProduct = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(models);
                response.Result = mappedProduct;
                return response;
            }
            else
            {
                response.Message = "No products found";
                response.StatusCode = 200;
                response.IsSuccess = false;
                return response;
            }
        }

        [HttpGet("get_id")]
        public async Task<ActionResult<ApiResponse>> GetById([FromQuery]int id)
        {
            

            var model = await unitOfWork.productRepository.GetById(id);
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiValidationResponse(new List<string> { "Invalid id", "try positive id" }, 400));
                }
                else if (model == null)
                {
                    var x = model.ToString();
                    return NotFound(new ApiResponse(400, "Product not found"));
                }

                var mappedProduct = mapper.Map<Product, ProductDTO>(model); // Adjusted mapping
                return Ok(new ApiResponse(200, result: mappedProduct));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ApiValidationResponse(new List<string> { "internal server error", ex.Message },
                                  StatusCodes.Status500InternalServerError));
            }
        }



        [HttpPost]
        public  async Task<ActionResult<ApiResponse>> CreateProduct(Create_UpdateProductDTO request)
        {
            //igenericRepo.CreateProduct(request);
            // iproductRepo.CreateProduct(request);
            //await unitOfWork.productRepository.CreateProduct(request);
            //await unitOfWork.Save();
            ////context.SaveChanges();

            //return Ok();
            if (request == null)
            {
                response.Message = "Invalid product data.";
                response.StatusCode = 400;
                response.IsSuccess = false;
                return BadRequest(response);
            }

            var product = mapper.Map<Product>(request);
            await unitOfWork.productRepository.CreateProduct(product);
            await unitOfWork.Save();

            response.StatusCode = 200;
            response.IsSuccess = true;
            response.Result = product;
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> updateProduct(Product request)
        {
            //igenericRepo.updateProduct(request);
            //iproductRepo.updateProduct(request);
            unitOfWork.productRepository.updateProduct(request);
            await unitOfWork.Save();
            //context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            //igenericRepo.DeleteProduct(id);
            //iproductRepo.DeleteProduct(id);
            unitOfWork.productRepository.DeleteProduct(id);
            await unitOfWork.Save();
            //context.SaveChanges();
            return Ok();
        }

        [HttpGet("productById/({cat_id})")]

        public async Task <ActionResult<ApiResponse>> GetAllProductById(int cat_id)
        {
            var products = await unitOfWork.productRepository.GetAllProductByCategoryId(cat_id);
            var check = products.Any();
            if (check)
            {
                response.StatusCode = 200;
                response.IsSuccess = check;
                var mappedProduct = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
                response.Result = mappedProduct;
                return response;
            }
            else
            {
                response.Message = "No products found";
                response.StatusCode = 200;
                response.IsSuccess = false;
                return response;
            }

        }

    }
}
