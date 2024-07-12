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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CreateProduct(T request)
        {
           await  context.Set<T>().AddAsync(request);
        }

        public void DeleteProduct(int id)
        {
            
            var model = context.Set<T>().FindAsync(id);
            context.Remove(model);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T)== typeof(Product))
            {
                var query = await context.Products.Include(x => x.categories).ToListAsync();
                 return (IEnumerable<T>)query;
            }
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var model = await context.Set<T>().FindAsync(id);
            return model;
        }

        public void updateProduct(T request)
        {
            context.Update(request);
        }
    }
}
