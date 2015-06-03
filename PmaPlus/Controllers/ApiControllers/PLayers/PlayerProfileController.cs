using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.DashboardContent;
using PmaPlus.Model.ViewModels.Player;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.PLayers
{
    public class PlayerProfileController : ApiController
    {
        private readonly PlayerServices _playerServices;
        private readonly UserServices _userServices;

        public PlayerProfileController(PlayerServices playerServices, UserServices userServices)
        {
            _playerServices = playerServices;
            _userServices = userServices;
        }


        [Route("api/Player/Profile/CurriclumsScoreGraph/1")]
        public IEnumerable<GraphBoxViewModel> GetCurriclumsScoreGraph()
        {
            var temp = new List<GraphBoxViewModel>();

            for (int i = 0; i < 11; i++)
            {
                temp.Add(new GraphBoxViewModel() { Month = i, PlayersScore = 0 });
            }

            return temp; //TODO: Players curriculum score graph
        }



        [Route("api/Player/Profile/CurriclumsScoreGraph/2")]
        public IEnumerable<GraphBoxViewModel> GetCurriclumsScoreGraph2()
        {
            var temp = new List<GraphBoxViewModel>();

            for (int i = 0; i < 11; i++)
            {
                temp.Add(new GraphBoxViewModel() { Month = i, PlayersScore = 0 });
            }

            return temp; //TODO: Players curriculum score graph
        }

        [Route("api/Player/Profile/SkillsProgress")]
        public InfoBoxViewModel GetSkillsProgress()
        {
            return null; //TODO: Skills progress for player
        }


        [Route("api/Player/Profile/SportSience")]
        public InfoBoxViewModel GetSportSience()
        {
            return null; //TODO: radar sport scientist for player
        }

        [Route("api/Player/Profile/AttendanceGraph")]
        public IEnumerable<GraphBoxViewModel> GetAttendanceGraph()
        {
            var temp = new List<GraphBoxViewModel>();

            var today = DateTime.Now.Month;

            for (int i = 0; i < 11; i++)
            {
                if (today + 1 > 12)
                {
                    today = 1;
                }
                temp.Add(new GraphBoxViewModel() { Month = today, PlayersScore = 0 });
                today++;
            }

            return temp; //TODO: Players Attendance graph
        }

        [Route("api/Player/Profile/AttendanceGraph/Team")]
        public IEnumerable<GraphBoxViewModel> GetTeamAttendanceGraph()
        {
            var temp = new List<GraphBoxViewModel>();

            var today = DateTime.Now.Month;

            for (int i = 0; i < 11; i++)
            {
                if (today + 1 > 12)
                {
                    today = 1;
                }
                temp.Add(new GraphBoxViewModel() { Month = today, PlayersScore = 0 });
                today++;
            }

            return temp; //TODO: Players team Attendance graph
        }

        [Route("api/Player/Profile/MatchFormGraph")]
        public IEnumerable<GraphBoxViewModel> GetMatchFormGraph()
        {
            var temp = new List<GraphBoxViewModel>();

            for (int i = 0; i < 11; i++)
            {
                temp.Add(new GraphBoxViewModel() { Month = i, PlayersScore = 0 });
            }

            return temp; //TODO: Players MatchFormGraph 
        }


        [Route("api/Player/Profile/Statistic")]
        public PlayerDetailTableViewModel GetPlayerDetail()
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            var userId = 0;
            if (user != null)
            {
                userId = user.Id;
            }
            return _playerServices.GetPlayersDetailTable(club.Id).FirstOrDefault(p => p.Id == userId);
        }
        
        
        


    }
}
