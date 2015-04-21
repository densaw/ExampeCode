using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
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
        public  FACourse Get(int id)
        {
            return _faCourseServices.GetFaCourse(id);
        }

        // POST: api/FaCourses
        public IHttpActionResult PostFaCourse([FromBody]FACourse faCourse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newFaCourse = _faCourseServices.AddFaCourse(faCourse);
            
            return Created(Request.RequestUri + newFaCourse.Id.ToString(), newFaCourse);
        }

        // PUT: api/FaCourses/5
        public IHttpActionResult PutFacourse(int id, [FromBody]FACourse faCourse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            _faCourseServices.UpdateFaCourse(faCourse);
            return Ok();
        }

        // DELETE: api/FaCourses/5
        public IHttpActionResult Delete(int id)
        {
            if (_faCourseServices.GetFaCourse(id) == null)
                return NotFound();
            _faCourseServices.Delete(id);
            return Ok();
        }
    }
}
