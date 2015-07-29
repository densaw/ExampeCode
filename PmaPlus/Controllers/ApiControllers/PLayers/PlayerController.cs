using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.WebPages;
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
        [Route("api/Player/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}/{filter?}")]
        public PlayersPage Get(int pageSize, int pageNumber, string orderBy = "", bool direction = false, string filter = "")
        {
            var clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;

            var players = filter.IsEmpty() ? _playerServices.GetPlayersTable(clubId) : _playerServices.GetPlayersTable(clubId).Where(p => p.Name.ToLower().Contains(filter.ToLower()));

            var count = players.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var items = players.OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

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
            var clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;
            return _playerServices.GetFreePlayers(clubId);

        }

        [Route("api/Player/List")]
        public IEnumerable<AvailablePlayersList> GetListPlayers()
        {
            var clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;
            return _playerServices.GetListPlayers(clubId);

        }


        public AddPlayerViewModel Get(int id)
        {
            if (id == -1)
            {
                var user = _userServices.GetUserByEmail(User.Identity.Name);
                return _playerServices.GetPlayerViewModel(user.Id);
            }

            return _playerServices.GetPlayerViewModel(id);
        }

        public IHttpActionResult Post(AddPlayerViewModel playerViewModel)
        {
            if (_userServices.UserExist(playerViewModel.Email))
                return Conflict();

            int clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;

            var player = _playerServices.AddPlayer(playerViewModel, clubId);
            if (player.Id > 0)
            {
                if (_photoManager.FileExists(playerViewModel.ProfilePicture))
                {
                    player.User.UserDetail.ProfilePicture = _photoManager.MoveFromTemp(player.User.UserDetail.ProfilePicture,
                        FileStorageTypes.ProfilePicture, player.User.Id, "ProfilePicture");
                }
                else
                {
                    player.User.UserDetail.ProfilePicture = _photoManager.SetDefaultPrifilePic(
                    FileStorageTypes.ProfilePicture, player.User.Id, "ProfilePicture.jpg");
                }
            }
            _playerServices.UpdatePlayer(player);
            return Ok();
        }

        public IHttpActionResult Put(int id, [FromBody]AddPlayerViewModel playerViewModel)
        {

            if (!_playerServices.PlayerExist(id))
            {
                return NotFound();
            }

            if (_photoManager.FileExists(playerViewModel.ProfilePicture))
            {

                playerViewModel.ProfilePicture = _photoManager.MoveFromTemp(playerViewModel.ProfilePicture,
                    FileStorageTypes.ProfilePicture, id, "ProfilePicture");
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
            _photoManager.Delete(FileStorageTypes.ProfilePicture, id);

            _playerServices.DeletePlayer(id);
            return Ok();
        }

    }
}
