using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.Player;
using PmaPlus.Model.ViewModels.PlayerAttribute;
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
        [Route("api/Player/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public PlayersPage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;

            _playerServices.GetPlayersTable(clubId);


            var count = _playerServices.GetPlayersTable(clubId).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var items = _playerServices.GetPlayersTable(clubId).Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new PlayersPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };
        }

        [Route("api/Player/Free")]
        public IEnumerable<AvailablePlayersList> GetFreePlayers()
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
            return _playerServices.GetFreePlayers(clubId);

        }

        public AddPlayerViewModel Get(int id)
        {
            return _playerServices.GetPlayerViewModel(id);
        }

        public IHttpActionResult Post(AddPlayerViewModel playerViewModel)
        {
            int clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;

            var player = _playerServices.AddPlayer(playerViewModel, clubId);
            if (player.Id > 0)
            {
                if (_photoManager.FileExists(playerViewModel.ProfilePicture))
                {
                    player.User.UserDetail.ProfilePicture = _photoManager.MoveFromTemp(player.User.UserDetail.ProfilePicture,
                        FileStorageTypes.PlayerProfilePicture, player.Id, "ProfilePicture");
                }
                else
                {
                    player.User.UserDetail.ProfilePicture = _photoManager.SetDefaultPrifilePic(
                    FileStorageTypes.PlayerProfilePicture, player.Id, "ProfilePicture.jpg");
                }
            }
            _playerServices.UpdatePlayer(player);
            return Ok();
        }

        public IHttpActionResult Put(int id, AddPlayerViewModel playerViewModel)
        {

            if (!_playerServices.PlayerExist(id))
            {
                return NotFound();
            }

            if (_photoManager.FileExists(playerViewModel.ProfilePicture))
            {

                playerViewModel.ProfilePicture = _photoManager.MoveFromTemp(playerViewModel.ProfilePicture,
                    FileStorageTypes.PlayerProfilePicture, id, "ProfilePicture");
            }
            _playerServices.UpdatePlayer(playerViewModel, id);
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (!_playerServices.PlayerExist(id))
            {
                return NotFound();
            }
            _photoManager.Delete(FileStorageTypes.PlayerProfilePicture, id);

            _playerServices.DeletePlayer(id);
            return Ok();
        }

    }
}
