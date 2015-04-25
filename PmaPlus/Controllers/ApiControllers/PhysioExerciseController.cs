using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PmaPlus.Controllers.ApiControllers
{
    public class PhysioExerciseController : ApiController
    {
        // GET: api/Physio
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Physio/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Physio
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Physio/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Physio/5
        public void Delete(int id)
        {
        }
    }
}
