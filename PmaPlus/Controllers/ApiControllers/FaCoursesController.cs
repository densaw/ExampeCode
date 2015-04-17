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
            return _faCourseServices.GetFaCourses();
        }

        // GET: api/FaCourses/5
        public FACourse Get(int id)
        {
            return _faCourseServices.GetFaCourse(id);
        }

        // POST: api/FaCourses
        public void Post([FromBody]FACourse faCourse)
        {
            _faCourseServices.InsertOrUpdate(faCourse);
        }

        // PUT: api/FaCourses/5
        public void Put(int id, [FromBody]FACourse faCourse)
        {
            _faCourseServices.InsertOrUpdate(faCourse);
        }

        // DELETE: api/FaCourses/5
        public void Delete(int id)
        {
            _faCourseServices.Delete(id);
        }
    }
}
