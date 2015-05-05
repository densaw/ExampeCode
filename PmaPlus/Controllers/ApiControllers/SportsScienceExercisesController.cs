using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.SportsScience;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class SportsScienceExercisesController : ApiController
    {

        private readonly SportsScienceServices _sportsScienceServices;

        public SportsScienceExercisesController(SportsScienceServices sportsScienceServices)
        {
            _sportsScienceServices = sportsScienceServices;
        }

        // GET: api/SportsScienceExercises
        [Route("api/SportsScienceExercises/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public SportsScienceExercisePage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _sportsScienceServices.GetSportsScienceExercises().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var tests = _sportsScienceServices.GetSportsScienceExercises().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize).AsEnumerable();
            var items = Mapper.Map<IEnumerable<SportsScienceExercise>, IEnumerable<SportsScienceExerciseTableViewModel>>(tests);

            return new SportsScienceExercisePage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        // GET: api/SportsScienceExercises/5
        public SportsScienceExerciseViewModel GetSportsScienceExercise(int id)
        {
            return
                Mapper.Map<SportsScienceExercise, SportsScienceExerciseViewModel>(
                    _sportsScienceServices.GetSportsScienceExerciseById(id));
        }

        // POST: api/SportsScienceExercises
        public IHttpActionResult PostSportsScienceExercise([FromBody]SportsScienceExerciseViewModel exerciseViewModel)
        {
            var exercise = Mapper.Map<SportsScienceExerciseViewModel, SportsScienceExercise>(exerciseViewModel);
            var newExercise = _sportsScienceServices.AddSportsScienceExercise(exercise);
            return Created(Request.RequestUri + newExercise.Id.ToString(), newExercise);
        }

        // PUT: api/SportsScienceExercises/5
        public IHttpActionResult PutSportsScienceExercise(int id, [FromBody] SportsScienceExerciseViewModel testViewModel)
        {
            if (!_sportsScienceServices.SportsScienceExerciseExist(id))
            {
                return NotFound();
            }
            var test = Mapper.Map<SportsScienceExerciseViewModel, SportsScienceExercise>(testViewModel);
            _sportsScienceServices.UpdateSportsScienceExercise(test, id);
            return Ok();
        }

        // DELETE: api/SportsScienceExercises/5
        public IHttpActionResult DeleteSportsScienceExercise(int id)
        {
            if (!_sportsScienceServices.SportsScienceExerciseExist(id))
            {
                return NotFound();
            }
            _sportsScienceServices.DeleteSportsScienceExercise(id);
            return Ok();
        }
    }
}
