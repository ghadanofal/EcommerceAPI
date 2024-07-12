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
        public async Task<ActionResult<ApiResponse>> GetAll()
        {
            var models = await unitOfWork.productRepository.GetAll();
            var check = models.Any();
            if (check)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = check;
                var mappedProduct = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(models);
                response.Result = mappedProduct;
                return response;
            }
            else
            {
                response.ErrorMessage = "No products found";
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = false;
                return response;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetById(int id)
        {
            //var model = igenericRepo.GetById(id);
            //var model = iproductRepo.GetById(id);
            var model = await unitOfWork.productRepository.GetById(id);
            var check = model !=null;
            if (check)
            {
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.IsSuccess = check;
                response.Result = model;
                return response;
            }
            else
            {
                response.ErrorMessage = "no products found";
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.IsSuccess = false;
                return response;
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
                response.ErrorMessage = "Invalid product data.";
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                return BadRequest(response);
            }

            var product = mapper.Map<Product>(request);
            await unitOfWork.productRepository.CreateProduct(product);
            await unitOfWork.Save();

            response.StatusCode = System.Net.HttpStatusCode.OK;
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
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = check;
                var mappedProduct = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
                response.Result = mappedProduct;
                return response;
            }
            else
            {
                response.ErrorMessage = "No products found";
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = false;
                return response;
            }

        }

    }
}
