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



    }
}
