using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.Player;
using PmaPlus.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class PlayerController : ApiController
    {
        private readonly PlayerServices _playerServices;
        private readonly UserServices _userServices;
        private readonly IPhotoManager _photoManager;

        public PlayerController(PlayerServices playerServices, UserServices userServices, IPhotoManager photoManager)
        {
            _playerServices = playerServices;
            _userServices = userServices;
            _photoManager = photoManager;
        }

        public IEnumerable<PlayerTableViewModel> Get()
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Id;

            return _playerServices.GetPlayersTable(clubId);


        } 

        public IHttpActionResult Post(AddPlayerViewModel playerViewModel)
        {
            int clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Id;
            var user = _playerServices.AddPlayer(playerViewModel,clubId);
            if (user.Id > 0 && _photoManager.FileExists(playerViewModel.ProfilePicture))
            {
                
                user.UserDetail.ProfilePicture = _photoManager.MoveFromTemp(user.UserDetail.ProfilePicture,
                    FileStorageTypes.ProfilePicture, user.Id, "ProfilePicture");
            }
            _userServices.UpdateUser(user);
            return Ok();
        }


    }
}
