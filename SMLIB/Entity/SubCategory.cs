using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMLIB.Entity
{
    public class SubCategory
    {
        [Key]
        [Column("SubCategoryId", Order=1)]
        public Guid SubCategoryId { get; set; }
        public string SubCategoryValue { get; set; }
    }
}