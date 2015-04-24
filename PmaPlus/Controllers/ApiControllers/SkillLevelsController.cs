using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Data.Repository;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class SkillLevelsController : ApiController
    {
        private readonly SkillServices _skillServices;

        public SkillLevelsController(SkillServices skillServices)
        {
            _skillServices = skillServices;
        }


        [Route("api/SkillLevels/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public IHttpActionResult Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _skillServices.GetSkillLevels().Count();
            var pages = Math.Ceiling((double)count / pageSize);
            var items = _skillServices.GetSkillLevels().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize);

            var result = new
            {
                Count = count,
                Pages = pages,
                Items = items
            };

            return Ok(result);
        }

        // GET: api/SkillLevel
        public IQueryable<SkillLevelViewModel> Get()
        {
            return Mapper.Map<IQueryable<SkillLevel>, IQueryable<SkillLevelViewModel>>(_skillServices.GetSkillLevels());
        }

        // GET: api/SkillLevel/5
        public SkillLevelViewModel Get(int id)
        {
            return Mapper.Map<SkillLevel,SkillLevelViewModel>(_skillServices.GetSkillLevelById(id));
        }

        // POST: api/SkillLevel
        public IHttpActionResult Post([FromBody]SkillLevelViewModel skillLevelViewModel)
        {
            var skillLevel = Mapper.Map<SkillLevelViewModel, SkillLevel>(skillLevelViewModel);
            var newSkillLevel =_skillServices.AddSkillLevel(skillLevel);
            return Created(Request.RequestUri + newSkillLevel.Id.ToString(),newSkillLevel);
        }

        // PUT: api/SkillLevel/5
        public IHttpActionResult Put(int id, [FromBody]SkillLevelViewModel skillLevelViewModel)
        {
            if (_skillServices.SkillLevelExist(id))
            {
                return NotFound();
            }
            var skillLevel = Mapper.Map<SkillLevelViewModel, SkillLevel>(skillLevelViewModel);
                _skillServices.UpdateSkillLevel(skillLevel,id);
            return Ok();
        }

        // DELETE: api/SkillLevel/5
        public IHttpActionResult Delete(int id)
        {
            if (!_skillServices.SkillLevelExist(id))
            {
                return NotFound();
            }
                _skillServices.DeleteSkillLevel(id);
            return Ok();
        }
    }
}
