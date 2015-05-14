using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Qualification;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class QualificationController : ApiController
    {
        private readonly TrainingTeamMembersServices _teamMembersServices;

        public QualificationController(TrainingTeamMembersServices teamMembersServices)
        {
            _teamMembersServices = teamMembersServices;
        }

        [Route("api/Qualification/{userId:int}")]
        public IEnumerable<QualificationViewModel> Get(int userId)
        {
            var qualificationsViewModel = Mapper.Map<IEnumerable<Qualification>, IEnumerable<QualificationViewModel>>(_teamMembersServices.GetTrainingTeamMemberQualifications(userId));
            return qualificationsViewModel;
        }

        [Route("api/Qualification/{userId:int}")]
        public IHttpActionResult Post(int userId, [FromBody]QualificationViewModel qualificationViewModel)
        {
            var qualification = Mapper.Map<QualificationViewModel, Qualification>(qualificationViewModel);
            _teamMembersServices.AddQualificationForTeamMember(qualification,userId);
            return Ok();
        }

        public IHttpActionResult Put(int id,[FromBody]QualificationViewModel qualificationViewModel)
        {
            if (!_teamMembersServices.QualificationExist(id))
            {
                return NotFound();
            }
            var qualification = Mapper.Map<QualificationViewModel, Qualification>(qualificationViewModel);
            _teamMembersServices.UpdateQualification(qualification,id);
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (!_teamMembersServices.QualificationExist(id))
            {
                return NotFound();
            }
            _teamMembersServices.DeleteQualification(id);
            return Ok();
        }
    }
}
