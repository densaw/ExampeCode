using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model;
using PmaPlus.Model.ViewModels;

namespace PmaPlus.Services.Extensions
{
    public static class UserExtensions
    {
        public static IQueryable<UsersList> QueryUsersList(this IQueryable<User> users)
        {
            if (users != null)
            {
                return users.Select(x => new UsersList
                {
                    Id = x.Id,
                    Name = (x.UserDetail == null) ? x.UserName : x.UserDetail.FirstName + " " + x.UserDetail.LastName,
                    UserAva = (x.Role == Role.ClubAdmin || x.Role == Role.SystemAdmin) ? "/Images/ProfilePicture.jpg" : "/api/file/ProfilePicture/" + x.UserDetail.ProfilePicture + "/" + x.Id,
                });
            }
            return null;
        } 
    }
}
