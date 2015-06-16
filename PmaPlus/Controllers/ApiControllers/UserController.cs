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
using PmaPlus.Model.ViewModels.TrainingTeamMember;
using PmaPlus.Model.ViewModels.Users;
using PmaPlus.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class UserController : ApiController
    {
        private readonly UserServices _userServices;
        private readonly IPhotoManager _photoManager;
        private readonly PlayerServices _playerServices;

        public UserController(UserServices userServices, IPhotoManager photoManager)
        {
            _userServices = userServices;
            _photoManager = photoManager;
        }

        public AddTrainingTeamMemberViewModel GetMemberProfile(int id)
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);


            return _userServices.GetDetailedTrainingTeamMemberViewModel(user.Id);
        }

        public IHttpActionResult Put( [FromBody]AddTrainingTeamMemberViewModel memberViewModel)
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            if (!_userServices.UserExist(user.Id))
            {
                return NotFound();
            }
            if (_photoManager.FileExists(memberViewModel.ProfilePicture))
            {
                memberViewModel.ProfilePicture = _photoManager.MoveFromTemp(memberViewModel.ProfilePicture,
                    FileStorageTypes.ProfilePicture, user.Id, "ProfilePicture");

            }
            _userServices.UpdateTrainigTeamMember(memberViewModel, user.Id);
            return Ok();
        }


        [Route("api/User/Name")]
        public string GetUserName()
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            if (user == null)
            {
                return "Undefine";
            }
            return String.Format("{0} {1}", user.UserDetail.FirstName,user.UserDetail.LastName);

        }




        [Route("api/Users/List")]
        public IEnumerable<UsersList> Get([FromUri] Role[] role)
        {
            var users = _userServices.GetUsersByRoles(role);
            return Mapper.Map<IEnumerable<User>, IEnumerable<UsersList>>(users);
        }

        [Route("api/Scouts/List")]
        public IEnumerable<UsersList> GetScouts()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            if (club != null)
            {
                return Mapper.Map<IEnumerable<Scout>,IEnumerable<UsersList>>(_userServices.GetClubScouts(club.Id));
            }
            return null;
        }


        [Route("api/Users/Avatar")]
        public UserAvatar GetAvatar()
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            string role = "";
            string pic = "";

            switch (user.Role)
            {
                case Role.HeadOfAcademies:
                {
                    role = "Head Of Academy";
                    pic = "/api/file/ProfilePicture/" + user.UserDetail.ProfilePicture + "/" + user.Id;
                    break;
                }
                case Role.Coach:
                {
                    role = "Coach";
                    pic = "/api/file/ProfilePicture/" + user.UserDetail.ProfilePicture + "/" + user.Id;
                    break;
                }
                case Role.HeadOfEducation:
                {
                    role = "Head Of Education";
                    pic = "/api/file/ProfilePicture/" + user.UserDetail.ProfilePicture + "/" + user.Id;
                    break;
                }
                case Role.Physiotherapist:
                {
                    role = "Physio";
                    pic = "/api/file/ProfilePicture/" + user.UserDetail.ProfilePicture + "/" + user.Id;
                    break;
                }
                case Role.Player:
                {
                    role = "Player";
                    pic = "/api/file/ProfilePicture/" + user.UserDetail.ProfilePicture + "/" + user.Id;
                    //var player = _playerServices.QueryPlayer(p => p.User.Id == user.Id);
                    //pic = "/api/file/ProfilePicture/" + user.UserDetail.ProfilePicture + "/" + player.Id;


                    break;
                }
                case Role.Scout:
                {
                    role = "Scout";
                    pic = "/api/file/ProfilePicture/" + user.UserDetail.ProfilePicture + "/" + user.Id;
                    break;
                }
                case Role.SportsScientist:
                {
                    role = "Sports Scientist";
                    pic = "/api/file/ProfilePicture/" + user.UserDetail.ProfilePicture + "/" + user.Id;
                    break;
                }
                case Role.WelfareOfficer:
                {
                    role = "Welfare Officer";
                    pic = "/api/file/ProfilePicture/" + user.UserDetail.ProfilePicture + "/" + user.Id;
                    break;
                }
                case Role.SystemAdmin:
                {
                    return new UserAvatar()
                    {
                        Name = "System Administrator",
                        Picture = "/Images/ProfilePicture.jpg",
                        Role = role
                    };
                }


                default:
                {
                    break;
                }
            }





            return new UserAvatar()
            {
                Name = user.UserDetail.FirstName + " " + user.UserDetail.LastName,
                Picture = pic,
                Role = role
            };

        }

    }
}
