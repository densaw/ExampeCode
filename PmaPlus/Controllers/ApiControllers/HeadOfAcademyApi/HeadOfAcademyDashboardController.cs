using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.DashboardContent;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.HeadOfAcademyApi
{
    public class HeadOfAcademyDashboardController : ApiController
    {
          private readonly UserServices _userServices;

          public HeadOfAcademyDashboardController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [Route("api/HeadOfAcademyDashboard/ClubName")]
        public string GetClubName()
        {
            var club = _userServices.GetClubByUserName(User.Identity.Name);
            if (club == null)
            {
                return "";
            }
            return club.Name;
        }



        [Route("api/HeadOfAcademyDashboard/Players/ScoreGraph")]
        public IEnumerable<GraphBoxViewModel> GetPlayersScoreGraphs()
        {
            var temp = new List<GraphBoxViewModel>();

            var today = DateTime.Now.Month;

            for (int i = 0; i < 11; i++)
            {
                if (today + 1 > 12)
                {
                    today = 1;
                }
                temp.Add(new GraphBoxViewModel(){Month = today,PlayersScore = 0});
                today++;
            }

            return temp; //TODO: Players score graph
        }


        [Route("api/HeadOfAcademyDashboard/Players/Attendance/Week")]
        public InfoBoxViewModel GetWeekAttendance()
        {
            return null; //TODO: Week attendance players for club
        }

        [Route("api/HeadOfAcademyDashboard/Players/Attendance/Quarter")]
        public InfoBoxViewModel GetQuarterAttendance()
        {
            return null; //TODO: quarter attendance players for club
        }

        [Route("api/HeadOfAcademyDashboard/Players/Ijuries")]
        public InfoBoxViewModel GetPlayersIjuries()
        {
            return null; //TODO:  players with active injuries for club
        }
    }
}
