using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class SkillVideoController : ApiController
    {
        private readonly SkillServices _skillServices;

        public SkillVideoController(SkillServices skillServices)
        {
            _skillServices = skillServices;
        }

        // GET: api/SkillVideo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SkillVideo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SkillVideo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SkillVideo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SkillVideo/5
        public void Delete(int id)
        {
        }
    }
}
