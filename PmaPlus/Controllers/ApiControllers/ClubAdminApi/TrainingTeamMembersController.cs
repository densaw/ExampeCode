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
            var trainTeamMemb = _userServices.GetTrainingTeamMembers();
            return Mapper.Map<IQueryable<User>, IQueryable<TrainingTeamMemberViewModel>>(trainTeamMemb).AsEnumerable();
        }


    }
}
