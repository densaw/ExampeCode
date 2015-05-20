using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model;
using PmaPlus.Model.Models;
using PmaPlus.Model;
using PmaPlus.Model.ViewModels;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class UserController : ApiController
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }


        [Route("api/Users/List")]
        public IEnumerable<UsersList> Get([FromUri] Role[] role)
        {
            var users = _userServices.GetUsersByRoles(role);
            return Mapper.Map<IEnumerable<User>, IEnumerable<UsersList>>(users);

        }




    }
}
