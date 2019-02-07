using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.Entity
{
    public class Store
    {
        [Key]
        [Column("StoreId",Order=1)]
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public double StoreContactNumber { get; set; }
        //public User StoreOwner { get; set; }
    }
}
