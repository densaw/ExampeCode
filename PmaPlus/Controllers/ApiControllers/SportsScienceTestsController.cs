using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Model.ViewModels.SportsScience;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class SportsScienceTestsController : ApiController
    {

        private readonly SportsScienceServices _sportsScienceServices;

        public SportsScienceTestsController(SportsScienceServices sportsScienceServices)
        {
            _sportsScienceServices = sportsScienceServices;
        }

        
        // GET: api/SportsScienceTests
        [Route("api/SportsScienceTests/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public SportsScienceTestPage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _sportsScienceServices.GetSportsScienceTests().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var tests = _sportsScienceServices.GetSportsScienceTests().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize).AsEnumerable();
            var items = Mapper.Map<IEnumerable<SportsScienceTest>, IEnumerable<SportsScienceTestTableViewModel>>(tests);

            return new SportsScienceTestPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        // GET: api/SportsScienceTests/5
        public SportsScienceTestViewModel GetSportsScienceTest(int id)
        {
            return
                Mapper.Map<SportsScienceTest, SportsScienceTestViewModel>(
                    _sportsScienceServices.GetSportsScienceTestById(id));
        }

        // POST: api/SportsScienceTests
        public IHttpActionResult PostSportsScienceTest([FromBody]SportsScienceTestViewModel testViewModel)
        {
            var test = Mapper.Map<SportsScienceTestViewModel, SportsScienceTest>(testViewModel);
            var newTest = _sportsScienceServices.AddSportsScienceTest(test);
            return Created(Request.RequestUri + newTest.Id.ToString(), newTest);
        }

        // PUT: api/SportsScienceTests/5
        public IHttpActionResult PutSportsScienceTest(int id, [FromBody] SportsScienceTestViewModel testViewModel)
        {
            if (!_sportsScienceServices.SportsScienceTestExist(id))
            {
            return NotFound();
            }
                var test = Mapper.Map<SportsScienceTestViewModel, SportsScienceTest>(testViewModel);
                _sportsScienceServices.UpdateSportsScienceTest(test,id);
            return Ok();
        }

    
        // DELETE: api/SportsScienceTests/5
        public IHttpActionResult DeleteSportsScienceTest(int id)
        {
            if (!_sportsScienceServices.SportsScienceTestExist(id))
            {
                return NotFound();
            }
            _sportsScienceServices.DeleteSportsScienceTest(id);
            return Ok();
        }
    
    }
}
