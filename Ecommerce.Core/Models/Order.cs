using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey(nameof(LocalUser))]
        public int LocalUserId { get; set; }
        
        public string OrderSatutus { get; set; }
        public DateTime OrderDate {  get; set; }
        public LocalUser? LocalUsers { get; set; }
        public ICollection<OrderDetails> orderDetails = new HashSet<OrderDetails>();

    }
}
