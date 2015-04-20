using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.Models;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers
{
    public class FaCoursesController : ApiController
    {
        private readonly FaCourseServices _faCourseServices;
        public FaCoursesController(FaCourseServices faCourseServices)
        {
            _faCourseServices = faCourseServices;
        }

        // GET: api/FaCourses
        public IEnumerable<FACourse> Get()
        {
            //return _faCourseServices.GetFaCourses();
            return new List<FACourse>(){new FACourse()};
        }

        // GET: api/FaCourses/5
        public FACourse Get(int id)
        {
            return _faCourseServices.GetFaCourse(id);
        }

        // POST: api/FaCourses
        public HttpResponseMessage PostFaCourse([FromBody]FACourse faCourse)
        {
            if(faCourse == null)
                return new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            _faCourseServices.AddFaCourse(faCourse);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT: api/FaCourses/5
        public HttpResponseMessage PutFacourse(int id, [FromBody]FACourse faCourse)
        {
            _faCourseServices.UpdateFaCourse(faCourse);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE: api/FaCourses/5
        public void Delete(int id)
        {
            _faCourseServices.Delete(id);
        }
    }
}
