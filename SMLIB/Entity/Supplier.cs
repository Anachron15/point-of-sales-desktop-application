using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.Entity
{
    public class Supplier
    {
        [Key]
        [Column("SupplierId", Order = 1)]
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public double SupplierContactNumber { get; set; }
    }
}
