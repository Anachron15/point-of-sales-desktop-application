using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMLIB.Entity
{
    public class Status
    {
        [Key]
        [Column("StatusId",Order=1)]
        public Guid StatusId { get; set; }
        public string StatusValue { get; set; }
    }
}