using Ecommerce.Core.IRepositories;
using Ecommerce.Core.Models;
using Ecommerce.Infastructure.Data;
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

       
        public Product FilterProduct()
        {
            throw new NotImplementedException();
        }

      
    }
}
