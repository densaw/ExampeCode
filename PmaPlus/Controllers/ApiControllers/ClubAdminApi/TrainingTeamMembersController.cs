using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PmaPlus.Model;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.TrainingTeamMember;
using PmaPlus.Services;
using PmaPlus.Services.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class TrainingTeamMembersController : ApiController
    {
        private readonly UserServices _userServices;
        private readonly IPhotoManager _photoManager;
        private readonly TrainingTeamMembersServices _teamMembersServices;

        public TrainingTeamMembersController(UserServices userServices, IPhotoManager photoManager, TrainingTeamMembersServices teamMembersServices)
        {
            _userServices = userServices;
            _photoManager = photoManager;
            _teamMembersServices = teamMembersServices;
        }

        public IEnumerable<TrainingTeamMemberPlateViewModel> Get()
        {
            return  _userServices.GetTrainingTeamMembers();
        }

        //[ResponseType(typeof(AddTrainingTeamMemberViewModel))]
        //[Route("api/TrainingTeamMembers/{id}/detailed")]
        //public IHttpActionResult GetDetailedMemberProfile(int id)
        //{
        //    return Ok(_userServices.GetDetailedTrainingTeamMemberViewModel(id));
        //}


        public AddTrainingTeamMemberViewModel GetMemberProfile(int id)
        {
            return _userServices.GetDetailedTrainingTeamMemberViewModel(id);
        }

        public IHttpActionResult Post(AddTrainingTeamMemberViewModel memberViewModel)
        {

            var newUser =  _userServices.AddTrainingTeamMember(memberViewModel,User.Identity.Name);
            if (newUser != null)
            {
                if (_photoManager.FileExists(memberViewModel.ProfilePicture))
                {
                    newUser.UserDetail.ProfilePicture = _photoManager.MoveFromTemp(newUser.UserDetail.ProfilePicture,
                   FileStorageTypes.ProfilePicture, newUser.Id, "ProfilePicture");
                }
                else
                {
                    newUser.UserDetail.ProfilePicture = _photoManager.SetDefaultPrifilePic(
                    FileStorageTypes.ProfilePicture, newUser.Id, "ProfilePicture.jpg");
                }
                    _userServices.UpdateUser(newUser);
            }
            return Ok();
        }

        public IHttpActionResult Put(int id, [FromBody]AddTrainingTeamMemberViewModel memberViewModel)
        {
            if (!_userServices.UserExist(id))
            {
                return NotFound();
            }
            if (_photoManager.FileExists(memberViewModel.ProfilePicture))
            {
                memberViewModel.ProfilePicture = _photoManager.MoveFromTemp(memberViewModel.ProfilePicture,
                    FileStorageTypes.ProfilePicture, id, "ProfilePicture");
                
            }
            _userServices.UpdateTrainigTeamMember(memberViewModel,id);
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (!_userServices.UserExist(id))
            {
                return NotFound();
            }

            _userServices.DeleteTrainigTeamMember(id);

            return Ok();
        }

        [Route("api/Coaches/list")]
        public IEnumerable<CoachesList> GetCoaches()
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
            return _teamMembersServices.GetClubCoaches(clubId);
        }
    
    }
}
