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
    public class CurriculumBlockDetailsController : ApiController
    {
        private readonly CurriculumServices _curriculumServices;

        public CurriculumBlockDetailsController(CurriculumServices curriculumServices)
        {
            _curriculumServices = curriculumServices;
        }


        //[Route("api/FaCourses/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        //public FaCoursePage Get(int pageSize, int pageNumber, string orderBy = "")
        //{
        //    var count = _curriculumServices.GetCurriculumBlocks(id).Count();
        //    var pages = (int)Math.Ceiling((double)count / pageSize);
        //    var items = _curriculumServices.GetCurriculumBlocks(id).OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize);
        //
        //    return new FaCoursePage()
        //    {
        //        Count = count,
        //       Pages = pages,
        //        Items = items
        //    };

        //}


        public IEnumerable<CurriculumDetailViewModel> Get(int id)
        {
            var curriculum = _curriculumServices.GetCurriculumBlocks(id);
            return Mapper.Map<IEnumerable<CurriculumBlock>, IEnumerable<CurriculumDetailViewModel>>(curriculum);
        } 


        public IHttpActionResult PutDetail(int id, [FromBody]CurriculumDetailViewModel detailViewModel)
        {
            var detail = Mapper.Map<CurriculumDetailViewModel, CurriculumDetail>(detailViewModel);
            _curriculumServices.UpdateDetail(detail, detail.Id);
            return Ok();
        }



    }
}
