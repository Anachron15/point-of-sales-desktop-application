using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.View
{
    public class vwTransaction
    {
        public Guid TransactionId { get; set; }
        public double TransactionAmountReceived { get; set; }
        public double TransactionAmountReturned { get; set; }
        public double TransactionNewAmountReturned { get; set; }
        public double TransactionSubTotal { get; set; }
        public string TransactionDate { get; set; }
        public string Transaction_Remarks { get; set; }
        public string Transaction_Status { get; set; }
        public Guid Transaction_OwnerId { get; set; }
        public string TransactionTime { get; set; }
        public string TransactionDateModified { get; set; }
        public string TransactionTimeModified { get; set; }
        public string TransactionCashier { get; set; }
        public string TransactionCustomer { get; set; }
    }
}
