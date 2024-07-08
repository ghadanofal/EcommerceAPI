using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }

        [ForeignKey(nameof(Product)) ]
        public int ProductId { get; set; }

        public ICollection<Product> Products = new HashSet<Product>();
    }
}
