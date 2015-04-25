using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PmaPlus.Controllers.ApiControllers
{
    public class PhysioBodyPartsController : ApiController
    {
        // GET: api/PhysioBodyParts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PhysioBodyParts/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PhysioBodyParts
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PhysioBodyParts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PhysioBodyParts/5
        public void Delete(int id)
        {
        }
    }
}
