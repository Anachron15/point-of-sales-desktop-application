using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.Entity
{
    public class UnitOfMeasurement
    {
        [Key]
        [Column("UnitOfMeasurementId", Order =1)]
        public Guid UnitOfMeasurementId { get; set; }
        public string UnitOfMeasurementName { get; set; }
    }
}
