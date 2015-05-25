using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
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


        //[Route("api/CurriculumDetail/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        //public CurriculumDetailPage Get(int pageSize, int pageNumber, string orderBy = "")
        //{
        //    var count = _curriculumServices.GetCurriculumBlocks(id).Count();
        //    var pages = (int)Math.Ceiling((double)count / pageSize);
        //    var  details = _curriculumServices.GetCurriculumBlocks(id).OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize).AsEnumerable();

        //    var items = Mapper.Map<IEnumerable<CurriculumDetail> ,IEnumerable<CurriculumDetailViewModel>>(details);

        //    return new CurriculumDetailPage()
        //    {
        //        Count = count,
        //        Pages = pages,
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
