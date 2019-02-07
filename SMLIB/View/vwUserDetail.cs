using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLIB.View
{
    public class vwUserDetail
    {
        public Guid UserDetailsId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserAddress { get; set; }
        public Double UserContactNumber { get; set; }
        public string UserImage { get; set; }
        public string UserRole { get; set; }
        public string UserStatus { get; set; }
    }
}
