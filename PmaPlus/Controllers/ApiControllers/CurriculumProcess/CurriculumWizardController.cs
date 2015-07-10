using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.CurriculumProcess;
using PmaPlus.Services;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.CurriculumProcess
{
    public class CurriculumWizardController : ApiController
    {
        private readonly CurriculumProcessServices _curriculumProcessServices;
        private readonly UserServices _userServices;
        private readonly TeamServices _teamServices;

        public CurriculumWizardController(CurriculumProcessServices curriculumProcessServices, UserServices userServices, TeamServices teamServices)
        {
            _curriculumProcessServices = curriculumProcessServices;
            _userServices = userServices;
            _teamServices = teamServices;
        }
        //Wizard tools

        #region Wizard tools

        [Route("api/Curriculum/Players/Statistic/{teamId:int}")]
        public IEnumerable<CurriculumPlayersStatisticViewModel> GetPlayersStatistics(int teamId)
        {
            return _curriculumProcessServices.CurriculumPlayersStatistic(teamId);
        }

        [Route("api/Curriculum/Wizard/{teamId:int}")]
        public IEnumerable<SessionsWizardViewModel> GetWizard(int teamId)
        {
            return Mapper.Map<IEnumerable<SessionResult>,IEnumerable<SessionsWizardViewModel>>(_curriculumProcessServices.GetCurriculumSessionsWizard(teamId));
        }

        [Route("api/Curriculum/Wizard/Session/Save/{teamId:int}/{sessionId:int}")]
        public IHttpActionResult Post(int teamId, int sessionId)
        {
            _curriculumProcessServices.SaveSession(sessionId, teamId);
            return Ok();
        }

        [Route("api/Curriculum/Wizard/Team/Archive/{teamId:int}/")]
        public IHttpActionResult PutTeamArchive(int teamId)
        {
            _curriculumProcessServices.ArchiveTeam(teamId);
            return Ok();
        }

        #endregion

        #region Attendance


        [Route("api/Curriculum/Wizard/Session/AttendanceTable/{teamId:int}/{sessionId:int}")]
        public IEnumerable<SessionAttendanceTableViewModel> GetPlayersAttendance(int teamId, int sessionId)
        {
            return _curriculumProcessServices.GetPlayersTableForAttendance(teamId, sessionId);
        }

        [Route("api/Curriculum/Wizard/Session/AttendanceTable/{teamId:int}/{sessionId:int}")]
        public IHttpActionResult Post(int teamId, int sessionId, [FromBody]IList<SessionAttendanceTableViewModel> sessionAttendanceTable)
        {
            var attendances =
                Mapper.Map<IList<SessionAttendanceTableViewModel>, List<SessionAttendance>>(sessionAttendanceTable);
            _curriculumProcessServices.UpdateAttendance(attendances, teamId, sessionId);
            return Ok();
        }

        #endregion

        #region Sesion Objectives
        //Add objectives
        [Route("api/Curriculum/Wizard/Session/StartObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IEnumerable<AddPlayerObjectiveTableViewModel> GetPlayerObjectives(int teamId, int sessionId)
        {
            return Mapper.Map<IEnumerable<PlayerObjective>,IEnumerable<AddPlayerObjectiveTableViewModel>>(_curriculumProcessServices.GetPlayerObjectiveTableForAdd(teamId, sessionId));
        }

        [Route("api/Curriculum/Wizard/Session/StartObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IHttpActionResult PostPlayerObjectives(int teamId, int sessionId, IList<AddPlayerObjectiveTableViewModel> playerObjectiveTable)
        {
            _curriculumProcessServices.AddObjectives(playerObjectiveTable, teamId, sessionId);
            return Ok();
        }

        //add Outcaome for objective
        [Route("api/Curriculum/Wizard/Session/ReportObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IEnumerable<PlayerObjectiveTableViewModel> GetPlayerReportObjectives(int teamId, int sessionId)
        {
            return Mapper.Map<IEnumerable<PlayerObjective>, IEnumerable<PlayerObjectiveTableViewModel>>(_curriculumProcessServices.GetPlayerObjectiveTableForReport(teamId, sessionId)); ;
        }
        [Route("api/Curriculum/Wizard/Session/ReportObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IHttpActionResult PostPlayerReportObjectives(int teamId, int sessionId, [FromBody]IList<PlayerObjectiveTableViewModel> playerObjectiveTable)
        {
            _curriculumProcessServices.AddReportObjectives(playerObjectiveTable, teamId, sessionId);
            return Ok();
        }
        #endregion


        //Block Objectives
        #region Block Objectives
        [Route("api/Curriculum/Wizard/Session/StartBlockObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IEnumerable<AddPlayerBlockObjectiveTableViewModel> GetPlayerBlockObjectiveTable(int teamId, int sessionId)
        {
            return Mapper.Map<IEnumerable<PlayerBlockObjective>,IEnumerable<AddPlayerBlockObjectiveTableViewModel>>(_curriculumProcessServices.GetBlockObjectiveTableForAdd(teamId, sessionId));
        }

        [Route("api/Curriculum/Wizard/Session/StartBlockObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IHttpActionResult PostPlayerBlockObjectiveTable(int teamId, int sessionId,
            [FromBody] IList<AddPlayerBlockObjectiveTableViewModel> playerObjectivesTable)
        {
            _curriculumProcessServices.AddBlockPreObjectives(playerObjectivesTable,teamId,sessionId);
            return Ok();
        }

        [Route("api/Curriculum/Wizard/Session/ReportBlockObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IEnumerable<PlayerBlockObjectiveTableViewModel> GetReportPlayerBlockObjectiveTable(int teamId, int sessionId)
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            return _curriculumProcessServices.GetBlockObjectiveTableForReport(teamId, sessionId,user.Id);
        }

        [Route("api/Curriculum/Wizard/Session/ReportBlockObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IHttpActionResult PostReportPlayerBlockObjectiveTable(int teamId, int sessionId,[FromBody] IList<PlayerBlockObjectiveTableViewModel> playerObjectivesTable)
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            _curriculumProcessServices.ReportBlockPreObjectives(playerObjectivesTable, teamId, sessionId,user.Id);
            return Ok();
        }
        #endregion



        #region Ratings
        

        [Route("api/Curriculum/Wizard/Session/RatingTable/{teamId:int}/{sessionId:int}")]
        public IEnumerable<PlayerRatingsTableViewModel> GetPlayersRating(int teamId, int sessionId)
        {
            return _curriculumProcessServices.GetPlayerRatingsTable(teamId, sessionId);
        }

        [Route("api/Curriculum/Wizard/Session/RatingTable/{teamId:int}/{sessionId:int}")]
        public IHttpActionResult Post(int teamId, int sessionId, [FromBody]IList<PlayerRatingsTableViewModel> playerRatingsTable)
        {
            var ratings = Mapper.Map<IList<PlayerRatingsTableViewModel>, List<PlayerRatings>>(playerRatingsTable);
            _curriculumProcessServices.UpdatePlayersRating(ratings, teamId, sessionId);
            return Ok();
        }

        #endregion



    }

    
}
