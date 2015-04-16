using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Controllers
{
    public class UserController : ApiController
    {
        User[] user = new User[] 
        { 
            new User { Id = 1, Email = "1@2.3", Password = "1", Role = Role.SystemAdmin, UserDetail = null, UserName = "Andrew"}, 
            new User { Id = 1, Email = "1@2.3", Password = "1", Role = Role.SystemAdmin, UserDetail = null, UserName = "Igor"}
        };
        public IList<User> GetAllUsers()
        {
            
            //TODO: Return All Users {area = SysAdmin} 
            return user;
        }

    }
}
