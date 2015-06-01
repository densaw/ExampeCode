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
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class SportsScienceExercisesController : ApiController
    {

        private readonly SportsScienceServices _sportsScienceServices;
        private readonly IPhotoManager _photoManager;
        public SportsScienceExercisesController(SportsScienceServices sportsScienceServices, IPhotoManager photoManager)
        {
            _sportsScienceServices = sportsScienceServices;
            _photoManager = photoManager;
        }

        // GET: api/SportsScienceExercises
        [Route("api/SportsScienceExercises/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public SportsScienceExercisePage Get(int pageSize, int pageNumber, string orderBy = "", bool direction = false)
        {
            var count = _sportsScienceServices.GetSportsScienceExercises().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var tests = _sportsScienceServices.GetSportsScienceExercises().AsEnumerable();
            var items = Mapper.Map<IEnumerable<SportsScienceExercise>, IEnumerable<SportsScienceExerciseTableViewModel>>(tests).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

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
            if (_photoManager.FileExists(newExercise.Picture1))
            {
                newExercise.Picture1 = _photoManager.MoveFromTemp(newExercise.Picture1,
                    FileStorageTypes.SportsScienceExercises, newExercise.Id, "Picture_1");
            }
            if (_photoManager.FileExists(newExercise.Picture2))
            {
                newExercise.Picture2 = _photoManager.MoveFromTemp(newExercise.Picture2,
                    FileStorageTypes.SportsScienceExercises, newExercise.Id, "Picture_2");
            } 
            if (_photoManager.FileExists(newExercise.Picture3))
            {
                newExercise.Picture3 = _photoManager.MoveFromTemp(newExercise.Picture3,
                    FileStorageTypes.SportsScienceExercises, newExercise.Id, "Picture_3");
            }
            _sportsScienceServices.UpdateSportsScienceExercise(newExercise,newExercise.Id);
            return Created(Request.RequestUri + newExercise.Id.ToString(), newExercise);
        }

        // PUT: api/SportsScienceExercises/5
        public IHttpActionResult PutSportsScienceExercise(int id, [FromBody] SportsScienceExerciseViewModel exerciseViewModel)
        {
            if (!_sportsScienceServices.SportsScienceExerciseExist(id))
            {
                return NotFound();
            }
            var exercise = Mapper.Map<SportsScienceExerciseViewModel, SportsScienceExercise>(exerciseViewModel);
            if (_photoManager.FileExists(exercise.Picture1))
            {
                exercise.Picture1 = _photoManager.MoveFromTemp(exercise.Picture1,
                    FileStorageTypes.SportsScienceExercises, id, "Picture_1");
            }
            if (_photoManager.FileExists(exercise.Picture2))
            {
                exercise.Picture2 = _photoManager.MoveFromTemp(exercise.Picture2,
                    FileStorageTypes.SportsScienceExercises, id, "Picture_2");
            }
            if (_photoManager.FileExists(exercise.Picture3))
            {
                exercise.Picture3 = _photoManager.MoveFromTemp(exercise.Picture3,
                    FileStorageTypes.SportsScienceExercises, id, "Picture_3");
            }
            _sportsScienceServices.UpdateSportsScienceExercise(exercise, id);
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
            _photoManager.Delete(FileStorageTypes.SportsScienceExercises, id);
            return Ok();
        }
    }
}
