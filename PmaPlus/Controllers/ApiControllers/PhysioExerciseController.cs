using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Physio;
using PmaPlus.Services.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class PhysioExerciseController : ApiController
    {
       private readonly PhysiotherapyServices _physiotherapyServices;
        private readonly IPhotoManager _photoManager;
       public PhysioExerciseController(PhysiotherapyServices physiotherapyServices, IPhotoManager photoManager)
       {
           _physiotherapyServices = physiotherapyServices;
           _photoManager = photoManager;
       }

        [Route("api/PhysioExercise/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
       public PhysioExercisePage Get(int pageSize, int pageNumber, string orderBy = "", bool direction = false)
        {
            var count = _physiotherapyServices.GetExercises().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var exercises = _physiotherapyServices.GetExercises().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize);
            var items = Mapper.Map<IEnumerable<PhysiotherapyExercise>, IEnumerable<PhysiotherapyExerciseTableViewModel>>(exercises).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

            return new PhysioExercisePage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        //// GET: api/PhysioBodyParts
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/PhysioBodyParts/5
        public PhysiotherapyExerciseViewModel Get(int id)
        {
            var exercises = _physiotherapyServices.GetExerciseById(id);
            if (exercises == null)
            {
                return null;
            }
            return Mapper.Map<PhysiotherapyExercise, PhysiotherapyExerciseViewModel>(exercises);
        }

        // POST: api/PhysioBodyParts
        public IHttpActionResult Post([FromBody]PhysiotherapyExerciseViewModel exerciseTableViewModel)
        {
            var exercise = Mapper.Map<PhysiotherapyExerciseViewModel, PhysiotherapyExercise>(exerciseTableViewModel);
            var newExercise =_physiotherapyServices.AddExercise(exercise);
            if (_photoManager.FileExists(newExercise.Picture))
            {
                newExercise.Picture = _photoManager.MoveFromTemp(newExercise.Picture, FileStorageTypes.PhysioExercises,
                    newExercise.Id, "Exercise");
                _physiotherapyServices.UpdateExercise(newExercise,newExercise.Id);
            }
            return Created(Request.RequestUri + newExercise.Id.ToString(),newExercise);
        }

        // PUT: api/PhysioBodyParts/5
        public IHttpActionResult Put(int id, [FromBody]PhysiotherapyExerciseViewModel exerciseViewModel)
        {
            if (!_physiotherapyServices.ExerciseExist(id))
            {
                return NotFound();
            }
            var exercise = Mapper.Map<PhysiotherapyExerciseViewModel, PhysiotherapyExercise>(exerciseViewModel);
            if (_photoManager.FileExists(exercise.Picture))
            {
                exercise.Picture = _photoManager.MoveFromTemp(exercise.Picture, FileStorageTypes.PhysioExercises,
                    id, "Exercise");
            }
            _physiotherapyServices.UpdateExercise(exercise, id);
            return Ok();

        }

        // DELETE: api/PhysioBodyParts/5
        public IHttpActionResult Delete(int id)
        {
            if (!_physiotherapyServices.ExerciseExist(id))
            {
                return NotFound();
            }
            _physiotherapyServices.DeleteExercise(id);
            _photoManager.Delete(FileStorageTypes.PhysioExercises, id);
            return Ok();

        
    }    }
}
