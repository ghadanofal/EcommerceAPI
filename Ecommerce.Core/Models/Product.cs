﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public decimal price { get; set; }
        public string? Image {  get; set; }

        public int CategoryId { get; set; }
        public virtual Category? categories { get; set; }

       
        public virtual ICollection<OrderDetails>? orderDetails { get; set; } = new HashSet<OrderDetails>();
    }
}
