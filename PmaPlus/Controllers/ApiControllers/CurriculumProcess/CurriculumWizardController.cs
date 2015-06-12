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

        [Route("api/Curriculum/Wizard/{teamId:int}")]
        public IEnumerable<SessionsWizardViewModel> GetWizard(int teamId)
        {
            return _curriculumProcessServices.GetCurriculumSessionsWizard(teamId);
        }




        [Route("api/Curriculum/Wizard/Session/Save/{teamCurriculumId:int}/{sessionId:int}")]
        public IHttpActionResult Post(int teamCurriculumId,int sessionId)
        {

            _curriculumProcessServices.SaveSession(sessionId,teamCurriculumId);

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
            _curriculumProcessServices.UpdateAttendance(attendances,teamId,sessionId);
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



    }
}
