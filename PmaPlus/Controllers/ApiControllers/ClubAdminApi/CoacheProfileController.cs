using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class CoacheProfileController : ApiController
    {
        private readonly UserServices _userServices;

        public CoacheProfileController(UserServices userServices)
        {
            _userServices = userServices;
        }





    }
}
