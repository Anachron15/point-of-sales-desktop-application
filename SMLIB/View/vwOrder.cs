using SMLIB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.View
{
    public class vwOrder
    {
        public Guid OrderId { get; set; }
        public Guid Order_CashierId { get; set; }
        public DateTime Order_DateTime { get; set; }
        public Guid Order_Transaction { get; set; }
        public Guid Order_User { get; set; }
        public Guid Order_ProductId { get; set; }
        public double Order_Quantity { get; set; }
    }
}
