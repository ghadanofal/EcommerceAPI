using Ecommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.IRepositories
{
    public interface IProductsRepository: IGenericRepository<Product>
    {
        public Task<Product> FilterProduct();

        public Task<IEnumerable<Product>> GetAllProductByCategoryId(int cat_id);
    }
}
