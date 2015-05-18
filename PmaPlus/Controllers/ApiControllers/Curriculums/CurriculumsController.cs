using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class CurriculumsController : ApiController
    {
        private readonly CurriculumServices _curriculumServices;
        private readonly UserServices _userServices;

        public CurriculumsController(CurriculumServices curriculumServices, UserServices userServices)
        {
            _curriculumServices = curriculumServices;
            _userServices = userServices;
        }

        [Route("api/Curriculums/List")]
        public IEnumerable<CurriculumsList> GetCurriculumsLists()
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
            return _curriculumServices.GetClubCurriculumsList(clubId);
        } 

        public IEnumerable<CurriculumViewModel> Get()
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
            return Mapper.Map<IEnumerable<Curriculum>, IEnumerable<CurriculumViewModel>>(_curriculumServices.GetClubCurriculums(clubId));
        }


        public IHttpActionResult Post([FromBody] CurriculumViewModel curriculumViewModel)
        {
            var curriculum = Mapper.Map<CurriculumViewModel, Curriculum>(curriculumViewModel);
            _curriculumServices.AddCurriculum(curriculum);
            return Ok();
        }



    }
}
