using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal price {  get; set; }
        public int quantity { get; set; }

        public Order? Orders { get; set; }
        public Product? Products { get; set; }
    }
}
