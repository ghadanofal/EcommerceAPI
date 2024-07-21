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

        public async Task<IEnumerable<T>> GetAll(int page_Size=2, int page_Number=1, string? includeProperity = null)
        {
            //if(typeof(T)== typeof(Product))
            //{
            //    var query = await context.Products.Include(x => x.categories).ToListAsync();
            //     return (IEnumerable<T>)query;
            //}

            IQueryable<T> query = context.Set<T>();
            if(includeProperity != null)
            {
                //includeProperity = "categories, order"
                foreach (var prority in includeProperity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperity);

                }
            }
            if(page_Size > 0)
            {
                if(page_Size > 4)
                {
                    page_Size = 4;
                }
            }
            query = query.Skip(page_Size * (page_Number - 1)).Take(page_Size);
            return await query.ToListAsync();
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
