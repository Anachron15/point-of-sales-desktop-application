using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.Entity
{
    public class Transaction
    {
        [Key]
        [Column("TransactionId", Order=1)]
        public Guid TransactionId { get; set; }
        public double TransactionAmountReceived { get; set; }
        public double TransactionAmountReturned { get; set; }
        public double TransactionNewAmountReturned { get; set; }
        public double TransactionSubTotal { get; set; }
        public string TransactionDate { get; set; }
        public string Transaction_Remarks { get; set; }
        public string Transaction_Status { get; set; }
        public Guid  Transaction_OwnerId { get; set; }
        public Guid Transaction_CashierId { get; set; }
        public string Transaction_Time { get; set; }
        public string TransactionDateModified { get; set; }
        public string TransactionTimeModified { get; set; }

    }
}
