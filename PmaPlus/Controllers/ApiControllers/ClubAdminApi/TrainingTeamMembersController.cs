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
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class TrainingTeamMembersController : ApiController
    {
        private readonly UserServices _userServices;
        private readonly IPhotoManager _photoManager;

        public TrainingTeamMembersController(UserServices userServices, IPhotoManager photoManager)
        {
            _userServices = userServices;
            _photoManager = photoManager;
        }

        public IEnumerable<TrainingTeamMemberViewModel> Get()
        {
            return  _userServices.GetTrainingTeamMembers();
        }

        public IHttpActionResult Post(AddTrainingTeamMemberViewModel memberViewModel)
        {

            var newUser =  _userServices.AddTrainingTeamMember(memberViewModel,User.Identity.Name);
            if (newUser != null)
            {
                newUser.UserDetail.ProfilePicture = _photoManager.MoveFromTemp(newUser.UserDetail.ProfilePicture,
                    FileStorageTypes.ProfilePicture, newUser.Id, "ProfilePicture");
            }
            return Ok();
        }

    }
}
