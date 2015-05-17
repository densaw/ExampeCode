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
    public class CurriculumDetailsController : ApiController
    {
        private readonly CurriculumServices _curriculumServices;

        public CurriculumDetailsController(CurriculumServices curriculumServices)
        {
            _curriculumServices = curriculumServices;
        }

        public IEnumerable<CurriculumDetailViewModel> Get(int id)
        {
            return Mapper.Map<IEnumerable<CurriculumDetail>, IEnumerable<CurriculumDetailViewModel>>(_curriculumServices.GetCurriculumDetails(id));
        } 

        public IHttpActionResult PostDetail(int id, [FromBody]CurriculumDetailViewModel detailViewModel)
        {
            var detail = Mapper.Map<CurriculumDetailViewModel, CurriculumDetail>(detailViewModel);
            var newDetail = _curriculumServices.AddCurriculumDetails(detail,id);
            if (newDetail == null)
            {
                return Conflict();
            }
            return Ok();

        }



    }
}
