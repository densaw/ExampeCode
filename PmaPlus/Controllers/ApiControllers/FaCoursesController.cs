using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PmaPlus.Controllers
{
    public class FaCoursesController : ApiController
    {
        
        public FaCoursesController()
        {
            
        }
        // GET: api/FaCourses
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FaCourses/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FaCourses
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/FaCourses/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FaCourses/5
        public void Delete(int id)
        {
        }
    }
}
