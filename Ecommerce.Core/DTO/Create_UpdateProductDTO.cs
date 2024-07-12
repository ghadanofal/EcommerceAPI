using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO
{
    public class Create_UpdateProductDTO
    {
        

        public string Name { get; set; } = null!;
        public decimal price { get; set; }
        public string? Image { get; set; }

       public int CategoryId { get; set; }
       
    }
}
