using Ecommerce.Core.IRepositories;
using Ecommerce.Core.Models;
using Ecommerce.Infastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWork<Product> unitOfWork;
        private readonly IProductsRepository iproductRepo;
        private readonly IGenericRepository<Product> igenericRepo;
        private readonly IProductsRepository productRepositories;

        public ProductController(ApplicationDbContext context,/*IProductsRepository iproductRepo */ IUnitOfWork<Product>unitOfWork)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
            /*this.iproductRepo = iproductRepo;
            this.igenericRepo = igenericRepo;*/
            //this.iproductRepo = iproductRepo;
            //this.productRepositories = productRepositories;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            //igenericRepo.GetAll();
           // var models = iproductRepo.GetAll();
            var models = await unitOfWork.productRepository.GetAll();

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            //var model = igenericRepo.GetById(id);
            //var model = iproductRepo.GetById(id);
            var model = await unitOfWork.productRepository.GetById(id);
            return Ok(model);
        }
        [HttpPost]
        public  async Task<ActionResult> CreateProduct(Product request)
        {
            //igenericRepo.CreateProduct(request);
           // iproductRepo.CreateProduct(request);
            await unitOfWork.productRepository.CreateProduct(request);
            unitOfWork.Save();
            //context.SaveChanges();

            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> updateProduct(Product request)
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
    }
}
