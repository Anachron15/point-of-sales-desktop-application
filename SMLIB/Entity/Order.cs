using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.Entity
{
    public class Order
    {
        [Key]
        [Column("OrderId",Order=1)]
        public Guid OrderId { get; set; }
        public Guid Order_Transaction { get; set; }
        public Guid Order_UserId { get; set; }
        public Guid Order_ProductId { get; set; }
        public DateTime Order_DateTime { get; set; }
        public double Order_Quantity { get; set; }
    }
}
