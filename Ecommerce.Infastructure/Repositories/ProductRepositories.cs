using Ecommerce.Core.IRepositories;
using Ecommerce.Core.Models;
using Ecommerce.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infastructure.Repositories
{
    public class ProductRepositories :  GenericRepository<Product>, IProductsRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepositories(ApplicationDbContext context): base(context)
        {
            this.context = context;
        }

       
        public Task<Product> FilterProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductByCategoryId(int cat_id)
        {
            //var product = await context.Products
            //    .Include(x => x.categories)
            //    .Where(c => c.CategoryId == cat_id)
            //    .ToListAsync();
            //return product;

            //lazy loading for related data
            var products = await context.Products
                .Where(c => c.CategoryId == cat_id)
                .ToListAsync();
            return products;
        }
    }
}
