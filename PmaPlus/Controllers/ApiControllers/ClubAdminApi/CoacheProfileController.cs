using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using PmaPlus.Model;
using PmaPlus.Model.ViewModels.TrainingTeamMember;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class CoachProfileController : ApiController
    {
        private readonly UserServices _userServices;

        public CoachProfileController(UserServices userServices)
        {
            _userServices = userServices;
        }


       

    }
}
