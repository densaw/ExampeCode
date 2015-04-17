
using System.Web.Http;
using PmaPlus.Model;
using PmaPlus.Services;

namespace PmaPlus.Controllers
{
    public class DashboardController : ApiController
    {
       
        private readonly PlayerServices _playerServices;
        private readonly UserServices _userServices;
        private readonly ClubServices _clubServices;


        public DashboardController(PlayerServices playerServices, UserServices userServices, ClubServices clubServices)
        {
            _playerServices = playerServices;
            _userServices = userServices;
            _clubServices = clubServices;
        }

        [Route("api/dashboard/active/players")]
        public IHttpActionResult GetActivePlayers()
        {
            return Ok(_playerServices.GetActivePlayersForLastSixMonth());
        }

        [Route("api/dashboard/logged/players")]
        public IHttpActionResult GetWeekLoggedPlayers()
        {
            return Ok(_userServices.GetUsersLoggedThisWeek(Role.Player));
        }

        [Route("api/dashboard/logged/coaches")]
        public IHttpActionResult GetWeekLoggedCoahes()
        {
            return Ok(_userServices.GetUsersLoggedThisWeek(Role.Coach));
        }

        [Route("api/dashboard/logged/clubs")]
        public IHttpActionResult GetWeekLoggedClubs()
        {
            return Ok(_clubServices.GetClubLoggedThisWeek());
        }

        [Route("api/dashboard/logged/users")]
        public IHttpActionResult GetWeekLoggedUsers()
        {
            return Ok(_userServices.GetUsersLoggedThisWeek());
        }


    }
}
