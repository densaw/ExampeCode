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
        public SkillVideoPage Get(int skillLevelId, int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _skillServices.GetSkillVideosForSlillLevel(skillLevelId).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var skillVideos = _skillServices.GetSkillVideosForSlillLevel(skillLevelId)
                    .OrderQuery(orderBy, f => f.Id)
                    .Paged(pageNumber, pageSize);

            var items = Mapper.Map<IEnumerable<SkillVideo>, IEnumerable<SkillVideoTableViewModel>>(skillVideos);

           return new SkillVideoPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };
        }

        // GET: api/SkillVideo/5
        public SkillVideoViewModel Get(int id)
        {
            return Mapper.Map<SkillVideo, SkillVideoViewModel>(_skillServices.GetSkillVideoById(id));
        }

        // POST: api/SkillVideo
        public IHttpActionResult Post(int id,[FromBody]SkillVideoViewModel skillVideoViewModel)
        {
            var skillVideo = Mapper.Map<SkillVideoViewModel, SkillVideo>(skillVideoViewModel);
            var newSkillVideo = _skillServices.AddSkillVideo(skillVideo,id);
            return Created(Request.RequestUri + newSkillVideo.Id.ToString(), newSkillVideo);

        }

        // PUT: api/SkillVideo/5
        public IHttpActionResult Put(int id, [FromBody]SkillVideoViewModel skillVideoViewModel)
        {
            if (!_skillServices.SkillVideoExist(id))
            {
                return NotFound();
            }
            var skillVideo = Mapper.Map<SkillVideoViewModel, SkillVideo>(skillVideoViewModel);
            _skillServices.UpdateSkillVideo(skillVideo, id);
            return Ok();
        }

        // DELETE: api/SkillVideo/5
        public IHttpActionResult Delete(int id)
        {
            if (!_skillServices.SkillVideoExist(id))
            {
                return NotFound();
            }
            _skillServices.DeleteSkillVideo(id);
            return Ok();
        }
    }
}
