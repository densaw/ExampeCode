using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Data.Repository;
using PmaPlus.Model.Models;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class SkillLevelController : ApiController
    {
        private readonly SkillServices _skillServices;

        public SkillLevelController(SkillServices skillServices)
        {
            _skillServices = skillServices;
        }

        // GET: api/SkillLevel
        public IEnumerable<SkillLevel> Get()
        {
            return _skillServices.GetSkillLevels();
        }

        // GET: api/SkillLevel/5
        public SkillLevel Get(int id)
        {
            return _skillServices.GetSkillLevelById(id);
        }

        // POST: api/SkillLevel
        public void Post([FromBody]SkillLevel skillLevel)
        {
            _skillServices.AddSkillLevel(skillLevel);
        }

        // PUT: api/SkillLevel/5
        public void Put(int id, [FromBody]SkillLevel skillLevel)
        {
            if (_skillServices.GetSkillLevelById(id) != null)
            {
                _skillServices.UpdateSkillLevel(skillLevel,id);
            }
        }

        // DELETE: api/SkillLevel/5
        public void Delete(int id)
        {
            if (_skillServices.GetSkillLevelById(id) != null)
            {
                _skillServices.DeleteSkillLevel(id);
            }
        }
    }
}
