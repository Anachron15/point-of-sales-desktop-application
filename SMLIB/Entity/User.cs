using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.Entity
{
    public class User
    {
        [Key]
        [Column("UserId",Order=1)]
        public Guid UserId { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public UserDetail UserDetail { get; set; }
       
    }
}
