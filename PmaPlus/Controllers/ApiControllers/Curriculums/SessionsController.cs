using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Model.ViewModels.Nutrition;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.Curriculums
{
    public class SessionsController : ApiController
    {
        private readonly CurriculumServices _curriculumServices;

        public SessionsController(CurriculumServices curriculumServices)
        {
            _curriculumServices = curriculumServices;
        }

        [Route("api/Sessions/{curriculumId:int}/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public SessionPage Get(int curriculumId, int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _curriculumServices.GetSessions(curriculumId).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var sessions = _curriculumServices.GetSessions(curriculumId)
                    .OrderQuery(orderBy, f => f.Id)
                    .Paged(pageNumber, pageSize);

            var items = Mapper.Map<IEnumerable<Session>, IEnumerable<SessionTableViewModel>>(sessions);

            return new SessionPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };
        }


        public SessionViewModel Get(int id)
        {
            return Mapper.Map<Session,SessionViewModel>(_curriculumServices.GetSessionById(id));
        }

        public IHttpActionResult Post(int id, [FromBody]SessionViewModel sessionViewModel)
        {
            var session = Mapper.Map<SessionViewModel, Session>(sessionViewModel);
            _curriculumServices.AddSession(session, id, sessionViewModel.Scenarios);

            return Ok();
            
        }

        public IHttpActionResult Put(int id, [FromBody] SessionViewModel sessionViewModel)
        {
            return Ok();
        }





    }
}
