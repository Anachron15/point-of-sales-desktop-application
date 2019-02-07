using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.Entity
{
    public class Warehouse
    {
        [Key]
        [Column("WarehouseId",Order=1)]
        public Guid WarehouseId { get; set; }
        public string   WarehouseName { get; set; }
        public string WarehouseAddress { get; set; }
        public double WarehouseContactNumber { get; set; }
        public string WarehouseSubLocation { get; set; }
    }
}
