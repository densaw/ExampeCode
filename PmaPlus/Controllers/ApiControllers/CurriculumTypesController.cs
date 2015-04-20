using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IEnumerable<CurriculumTypesTableViewModel> Get()
        {
            return _curriculumServices.GetCurriculumTypes();
        }

        // GET: api/CurriculumTypes/5
        public CurriculumType Get(int id)
        {
            return _curriculumServices.GetCurriculumType(id);
        }

        // POST: api/CurriculumTypes
        public void Post([FromBody]CurriculumType curriculumType)
        {
            _curriculumServices.InsertCurriculumType(curriculumType);
        }

        // PUT: api/CurriculumTypes/5
        public void Put(int id, [FromBody]CurriculumType curriculumType)
        {
            _curriculumServices.UpdateCurriculumTypes(curriculumType,id);
        }

        // DELETE: api/CurriculumTypes/5
        public void Delete(int id)
        {
            _curriculumServices.Delete(id);
        }
    }
}
