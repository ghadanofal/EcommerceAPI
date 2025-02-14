﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.IRepositories
{
    public interface IUnitOfWork<T> where T : class
    {
        public IProductsRepository productRepository { get; set; }
        public IOrderRepository orderRepository { get; set; }
        public Task<int> Save();
        
    }
}
