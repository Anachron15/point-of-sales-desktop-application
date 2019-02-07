using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.Entity
{
    public class UserDetail
    {
        [Key]
        [Column("UserDetailId",Order=1)]
        public Guid UserDetailId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserAddress { get; set; }
        public double UserContactNumber { get; set; }
        public string UserImage { get; set; }
        public string UserRole { get; set; }
        public string UserStatus { get; set; }
    }
}
