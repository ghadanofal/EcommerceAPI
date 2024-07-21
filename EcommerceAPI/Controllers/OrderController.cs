using AutoMapper;
using Azure;
using Ecommerce.Core.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Core.Models;
using Ecommerce.Infastructure.Data;
using Ecommerce.Infastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWork<Order> unitOfWork;
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;
        public ApiResponse response;

        public OrderController(ApplicationDbContext context, IUnitOfWork<Order>unitOfWork, IMapper mapper)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            response = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllOrder( int pageSize = 2, int pageNumber = 1)
        {
            var models = await unitOfWork.orderRepository.GetAll(page_Size: pageSize, page_Number: pageNumber,
                includeProperity: "categories");
            var check = models.Any();
            if (check)
            {
                response.StatusCode = 200;
                response.IsSuccess = check;
                var mappedProduct = mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(models);
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
        [HttpGet("orders/{user_id}")]
        public async Task<ActionResult<ApiResponse>> GetAllProductById(int user_id)
        {
            var products = await unitOfWork.orderRepository.GetAllOrderByUser(user_id);
            var check = products.Any();
            if (check)
            {
                response.StatusCode = 200;
                response.IsSuccess = check;
                var mappedProduct = mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(products);
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
