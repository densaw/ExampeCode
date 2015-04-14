using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace PmaPlus.Model
{
    public class User : IUser<int>
    {

        public int Id { get; set; }

        public string UserName { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public UserDetail UserDetail { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}
