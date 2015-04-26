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

namespace PmaPlus.Controllers.ApiControllers
{
    public class CurriculumTypesController : ApiController
    {
        private readonly CurriculumServices _curriculumServices;
        public CurriculumTypesController(CurriculumServices curriculumServices)
        {
            _curriculumServices = curriculumServices;
        }

        // GET: api/CurriculumTypes
        public IQueryable<CurriculumTypesTableViewModel> Get()
        {
            return _curriculumServices.GetCurriculumTypes();
        }

        [Route("api/CurriculumTypes/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public CurriculumTypePage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _curriculumServices.GetCurriculumTypes().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var items = _curriculumServices.GetCurriculumTypes().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize);

            return new CurriculumTypePage()
                        {
                            Count = count,
                            Pages = pages,
                            Items = items
                        };

        }

        // GET: api/CurriculumTypes/5
        public CurriculumTypeViewModel Get(int id)
        {
            var curriculumType = _curriculumServices.GetCurriculumType(id);
            return Mapper.Map<CurriculumType, CurriculumTypeViewModel>(curriculumType);
        }

        // POST: api/CurriculumTypes
        public IHttpActionResult Post([FromBody]CurriculumTypeViewModel curriculumTypeViewModel)
        {

            var curriculumType = Mapper.Map<CurriculumTypeViewModel, CurriculumType>(curriculumTypeViewModel);
            var newCurriculumType = _curriculumServices.InsertCurriculumType(curriculumType);

            return Created(Request.RequestUri + newCurriculumType.Id.ToString(), newCurriculumType);
        }

        // PUT: api/CurriculumTypes/5
        public IHttpActionResult Put(int id, [FromBody]CurriculumTypeViewModel curriculumTypeViewModel)
        {
            if (!_curriculumServices.CurriculumTypeExist(id))
                return NotFound();

            var curriculumType = Mapper.Map<CurriculumTypeViewModel, CurriculumType>(curriculumTypeViewModel);
            _curriculumServices.UpdateCurriculumTypes(curriculumType, id);
            return Ok();
        }

        // DELETE: api/CurriculumTypes/5
        public IHttpActionResult Delete(int id)
        {
            if (!_curriculumServices.CurriculumTypeExist(id))
                return NotFound();

            _curriculumServices.Delete(id);
            return Ok();

        }
    }
}
