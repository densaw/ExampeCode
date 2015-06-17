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

        [Route("api/Curriculum/Players/Statistic/{teamId:int}")]
        public IEnumerable<CurriculumPlayersStatisticViewModel> GetPlayersStatistics(int teamId)
        {
            return _curriculumProcessServices.CurriculumPlayersStatistic(teamId);

        }






        [Route("api/Curriculum/Wizard/{teamId:int}")]
        public IEnumerable<SessionsWizardViewModel> GetWizard(int teamId)
        {
            return _curriculumProcessServices.GetCurriculumSessionsWizard(teamId);
        }

        [Route("api/Curriculum/Wizard/Session/Save/{teamCurriculumId:int}/{sessionId:int}")]
        public IHttpActionResult Post(int teamCurriculumId, int sessionId)
        {

            _curriculumProcessServices.SaveSession(sessionId, teamCurriculumId);

            return Ok();
        }

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

        [Route("api/Curriculum/Wizard/Session/ObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IEnumerable<PlayerObjectiveTableViewModel> GetPlayerObjectives(int teamId, int sessionId)
        {
            return _curriculumProcessServices.GetPlayerObjectiveTable(teamId, sessionId);
        }

        [Route("api/Curriculum/Wizard/Session/ObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IHttpActionResult PostPlayerObjectives(int teamId, int sessionId, IList<PlayerObjectiveTableViewModel> playerObjectiveTable)
        {
            var objectives = Mapper.Map<IList<PlayerObjectiveTableViewModel>, List<PlayerObjective>>(playerObjectiveTable);

            _curriculumProcessServices.UpdateObjectives(objectives, teamId, sessionId);

            return Ok();
        }


        [Route("api/Curriculum/Wizard/Session/BlockObjectiveTable/{teamId:int}/{sessionId:int}")]
        public IEnumerable<PlayerBlockObjectiveTableViewModel> GetPlayerBlockObjectiveTable(int teamId, int sessionId)
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            if (user == null)
            {
                return null;
            }

            return _curriculumProcessServices.GetBlockObjectiveTable(teamId, sessionId, user.Id);


        }


        [Route("api/Curriculum/Wizard/Session/BlockObjective/{teamId:int}/{sessionId:int}")]
        public IHttpActionResult PostPlayerBlockObjective(int teamId, int sessionId, [FromBody]PlayerBlockObjectiveTableViewModel blockObjectiveViewModel)
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            var blockObj =
                Mapper.Map<PlayerBlockObjectiveTableViewModel, BlockObjectiveStatement>(blockObjectiveViewModel);


            _curriculumProcessServices.UpdateBlockObgectiveStatement(blockObj, blockObjectiveViewModel.PlayerId, teamId, sessionId, user.Id);
            return Ok();
        }


        [Route("api/Curriculum/Wizard/Rating/AttendanceTable/{teamId:int}/{sessionId:int}")]
        public IEnumerable<PlayerRatingsTableViewModel> GetPlayersRating(int teamId, int sessionId)
        {
            return _curriculumProcessServices.GetPlayerRatingsTable(teamId, sessionId);
        }

        [Route("api/Curriculum/Wizard/Rating/AttendanceTable/{teamId:int}/{sessionId:int}")]
        public IHttpActionResult Post(int teamId, int sessionId, [FromBody]IList<PlayerRatingsTableViewModel> playerRatingsTable)
        {
            var ratings =
                Mapper.Map<IList<PlayerRatingsTableViewModel>, List<PlayerRatings>>(playerRatingsTable);
            _curriculumProcessServices.UpdatePlayersRating(ratings, teamId, sessionId);
            return Ok();
        }




    }

    public class CurriculumPlayersStatisticViewModel
    {
        public string PlayerName { get; set; }
        public int Age { get; set; }
        public decimal Atl { get; set; }
        public decimal Att { get; set; }
        public int Mom { get; set; }
        public int Gls { get; set; }
        public int Sho { get; set; }
        public int Sht { get; set; }
        public int Asi { get; set; }
        public int Tck { get; set; }
        public int Pas { get; set; }
        public int Sav { get; set; }
        public int Crn { get; set; }
        public int Frk { get; set; }
        public int Frm { get; set; }
        public int Inj { get; set; }
        public decimal AttPercent { get; set; }
        public decimal WbPercent { get; set; }
        public decimal Cur { get; set; }

    }
}
