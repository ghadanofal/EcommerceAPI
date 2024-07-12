using Ecommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTO
{
     public class OrderDTO
    {
       

        public string OrderSatutus { get; set; }
        public DateTime OrderDate { get; set; }
        public string? LocalUsers { get; set; }
        public int LocalUserId { get; set; }
    }
}
