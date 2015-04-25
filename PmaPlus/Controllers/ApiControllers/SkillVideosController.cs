using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using AutoMapper.Internal;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class SkillVideosController : ApiController
    {
        private readonly SkillServices _skillServices;

        public SkillVideosController(SkillServices skillServices)
        {
            _skillServices = skillServices;
        }

        
        [Route("api/SkillVideos/{skillLevelId:int}/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public IHttpActionResult Get(int skillLevelId, int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _skillServices.GetSkillVideosForSlillLevel(skillLevelId).Count();
            var pages = Math.Ceiling((double)count / pageSize);
            var skillVideos = _skillServices.GetSkillVideosForSlillLevel(skillLevelId)
                    .OrderQuery(orderBy, f => f.Id)
                    .Paged(pageNumber, pageSize);

            var items = Mapper.Map<IQueryable<SkillVideo>, IQueryable<SkillVideoTableViewModel>>(skillVideos);

            var result = new
            {
                Count = count,
                Pages = pages,
                Items = items
            };
            return Ok(result);
        }

        // GET: api/SkillVideo/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

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
