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
                    Name = x.UserName
                });
            }
            return null;
        } 
    }
}
