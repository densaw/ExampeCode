using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.News;
using PmaPlus.Model.ViewModels.Nutrition;
using PmaPlus.Services.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class ExerciseNewsController : ApiController
    {
        private readonly NewsServices _newsServices;
        private readonly IPhotoManager _photoManager;

        public ExerciseNewsController(NewsServices newsServices, IPhotoManager photoManager)
        {
            _newsServices = newsServices;
            _photoManager = photoManager;
        }

        [Route("api/ExerciseNews/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public ExerciseNewPage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _newsServices.GetExcerciseNews().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var foodTypes = _newsServices.GetExcerciseNews().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize).AsEnumerable();
            var items = Mapper.Map<IEnumerable<ExcerciseNew>, IEnumerable<ExerciseNewTableViewModel>>(foodTypes);

            return new ExerciseNewPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }


        public ExerciseNewViewModel Get(int id)
        {
            return Mapper.Map<ExcerciseNew, ExerciseNewViewModel>(_newsServices.GetExcerciseNewById(id));
        }

        public IHttpActionResult Post([FromBody] ExerciseNewViewModel exerciseNewViewModel)
        {
            var exerciseNew =  _newsServices.AddExcerciseNew(Mapper.Map<ExerciseNewViewModel, ExcerciseNew>(exerciseNewViewModel));
            if (exerciseNew != null)
            {
                if (_photoManager.FileExists(exerciseNew.AuthorPicture))
                {
                    exerciseNew.AuthorPicture = _photoManager.MoveFromTemp(exerciseNew.AuthorPicture,
                        FileStorageTypes.ExerciseNews, exerciseNew.Id, "AuthorPicture");
                }

                if (_photoManager.FileExists(exerciseNew.MainPicture))
                {
                    exerciseNew.MainPicture = _photoManager.MoveFromTemp(exerciseNew.MainPicture,
                        FileStorageTypes.ExerciseNews, exerciseNew.Id, "MainPicture");
                }

                if (_photoManager.FileExists(exerciseNew.SponsoredBy))
                {
                    exerciseNew.SponsoredBy = _photoManager.MoveFromTemp(exerciseNew.SponsoredBy,
                        FileStorageTypes.ExerciseNews, exerciseNew.Id, "SponsoredBy");
                }

                if (_photoManager.FileExists(exerciseNew.Picture))
                {
                    exerciseNew.Picture = _photoManager.MoveFromTemp(exerciseNew.Picture,
                        FileStorageTypes.ExerciseNews, exerciseNew.Id, "Picture");
                }

                _newsServices.UpdateExerciseNew(exerciseNew,exerciseNew.Id);


            }
            return Ok();

        }


        public IHttpActionResult Put(int id,[FromBody] ExerciseNewViewModel exerciseNewViewModel)
        {
            if (!_newsServices.ExerciseNewExist(id))
            {
                return NotFound();
            }

            var exerciseNew = Mapper.Map<ExerciseNewViewModel, ExcerciseNew>(exerciseNewViewModel);
            
            exerciseNew.Id = id;


            if (_photoManager.FileExists(exerciseNew.AuthorPicture))
            {
                exerciseNew.AuthorPicture = _photoManager.MoveFromTemp(exerciseNew.AuthorPicture,
                    FileStorageTypes.ExerciseNews, exerciseNew.Id, "AuthorPicture");
            }

            if (_photoManager.FileExists(exerciseNew.MainPicture))
            {
                exerciseNew.MainPicture = _photoManager.MoveFromTemp(exerciseNew.MainPicture,
                    FileStorageTypes.ExerciseNews, exerciseNew.Id, "MainPicture");
            }

            if (_photoManager.FileExists(exerciseNew.SponsoredBy))
            {
                exerciseNew.SponsoredBy = _photoManager.MoveFromTemp(exerciseNew.SponsoredBy,
                    FileStorageTypes.ExerciseNews, exerciseNew.Id, "SponsoredBy");
            }

            if (_photoManager.FileExists(exerciseNew.Picture))
            {
                exerciseNew.Picture = _photoManager.MoveFromTemp(exerciseNew.Picture,
                    FileStorageTypes.ExerciseNews, exerciseNew.Id, "Picture");
            }

            _newsServices.UpdateExerciseNew(exerciseNew, exerciseNew.Id);

            return Ok();


        }

        public IHttpActionResult Delete(int id)
        {
            _newsServices.DeleteExcerciseNew(id);
            return Ok();
        }

    }
}
