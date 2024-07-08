using Ecommerce.Core.IRepositories;
using Ecommerce.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infastructure.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly IProductsRepository productsRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            productRepository = new ProductRepositories(context);
        }
        public IProductsRepository productRepository { get ; set ; }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }
    }
}
