using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.DashboardContent;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class ClubAdminDashboardController : ApiController
    {

        public ClubAdminDashboardController()
        {
            
        }

        [Route("api/ClubAdminDashboard/Players/ScoreGraph")]
        public IEnumerable<PlayersScoreGraph> GetPlayersScoreGraphs()
        {
            return null; //TODO: Players score graph
        }


        [Route("api/ClubAdminDashboard/Players/Attendance/Week")]
        public InfoBoxViewModel GetWeekAttendance()
        {
            return null; //TODO: Week attendance players for club
        }

        [Route("api/ClubAdminDashboard/Players/Attendance/Quarter")]
        public InfoBoxViewModel GetQuarterAttendance()
        {
            return null; //TODO: quarter attendance players for club
        }

        [Route("api/ClubAdminDashboard/Players/Ijuries")]
        public InfoBoxViewModel GetPlayersIjuries()
        {
            return null; //TODO:  players with active injuries for club
        }






    }
}
