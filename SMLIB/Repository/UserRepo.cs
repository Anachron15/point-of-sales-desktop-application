using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.View;

namespace SMLIB.Repository
{
    public class UserRepo
    {
        public static List<vwUserDetail> login(string userEmail,string password) {
            List<vwUserDetail> user;
            using (SMLIB.Context.Context context = new SMLIB.Context.Context()) {
                user = (from users in context.Users
                           from userDetail in context.UserDetails
                        where users.UserEmail == userEmail && users.UserPassword == password && users.UserId == userDetail.UserDetailId
                        select new vwUserDetail {
                               UserDetailsId= userDetail.UserDetailId,
                               UserAddress = userDetail.UserAddress,
                               UserContactNumber = userDetail.UserContactNumber,
                               UserFirstName=userDetail.UserFirstName,
                               UserImage = userDetail.UserImage,
                               UserLastName = userDetail.UserLastName,
                               UserRole = userDetail.UserRole,
                               UserStatus = userDetail.UserStatus
                           }  ).ToList();
            }
            return user;
        }
    }
}
