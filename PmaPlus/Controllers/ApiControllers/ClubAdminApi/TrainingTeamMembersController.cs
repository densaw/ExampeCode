using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.TrainingTeamMember;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class TrainingTeamMembersController : ApiController
    {
        private readonly UserServices _userServices;

        public TrainingTeamMembersController(UserServices userServices)
        {
            _userServices = userServices;
        }

        public IEnumerable<TrainingTeamMemberViewModel> Get()
        {
            return  _userServices.GetTrainingTeamMembers();
        }

        public IHttpActionResult Post(AddTrainingTeamMemberViewModel memberViewModel)
        {
             _userServices.AddTrainingTeamMember(memberViewModel,User.Identity.Name);
            return Ok();
        }

    }
}
