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

        public CurriculumsController(CurriculumServices curriculumServices)
        {
            _curriculumServices = curriculumServices;
        }

        public IEnumerable<CurriculumViewModel> Get()
        {
            return Mapper.Map<IEnumerable<Curriculum>, IEnumerable<CurriculumViewModel>>(_curriculumServices.GetAllCurriculums());
        }


        public IHttpActionResult Post([FromBody] CurriculumViewModel curriculumViewModel)
        {
            var curriculum = Mapper.Map<CurriculumViewModel, Curriculum>(curriculumViewModel);
            _curriculumServices.AddCurriculum(curriculum);
            return Ok();
        }



    }
}
