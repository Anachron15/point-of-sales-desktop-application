using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMLIB.Entity
{
    public class Category
    {
        [Key]
        [Column("CategoryId",Order=1)]
        public Guid CategoryId { get; set; }
        public string CategoryValue { get; set; }
    }
}