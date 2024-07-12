using Ecommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public decimal price { get; set; }
        public string? Image { get; set; }

        public int CategoryId { get; set; }
        public string? category_Name { get; set; }


    }
}
