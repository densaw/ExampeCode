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
using PmaPlus.Services.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class NutritionNewsController : ApiController
    {
        private readonly NewsServices _newsServices;
        private readonly IPhotoManager _photoManager;

        public NutritionNewsController(NewsServices newsServices, IPhotoManager photoManager)
        {
            _newsServices = newsServices;
            _photoManager = photoManager;
        }


        [Route("api/NutritionNews/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public NutritionNewPage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _newsServices.GetNutritionNews().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var news = _newsServices.GetNutritionNews().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize).AsEnumerable();
            var items = Mapper.Map<IEnumerable<NutritionNew>, IEnumerable<NutritionNewTableViewModel>>(news);

            return new NutritionNewPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }


        public NutritionNewViewModel Get(int id)
        {
            return Mapper.Map<NutritionNew, NutritionNewViewModel>(_newsServices.GetNutritionNewById(id));
        }

        public IHttpActionResult Post([FromBody] NutritionNewViewModel nutritionNewViewModel)
        {
            var nutritionNew = _newsServices.AddNutrittionNew(Mapper.Map<NutritionNewViewModel, NutritionNew>(nutritionNewViewModel));
            if (nutritionNew != null)
            {
                if (_photoManager.FileExists(nutritionNew.AuthorPicture))
                {
                    nutritionNew.AuthorPicture = _photoManager.MoveFromTemp(nutritionNew.AuthorPicture,
                        FileStorageTypes.NutritionNews, nutritionNew.Id, "AuthorPicture");
                }

                if (_photoManager.FileExists(nutritionNew.MainPicture))
                {
                    nutritionNew.MainPicture = _photoManager.MoveFromTemp(nutritionNew.MainPicture,
                        FileStorageTypes.NutritionNews, nutritionNew.Id, "MainPicture");
                }

                if (_photoManager.FileExists(nutritionNew.SponsoredBy))
                {
                    nutritionNew.SponsoredBy = _photoManager.MoveFromTemp(nutritionNew.SponsoredBy,
                        FileStorageTypes.NutritionNews, nutritionNew.Id, "SponsoredBy");
                }

                if (_photoManager.FileExists(nutritionNew.Picture))
                {
                    nutritionNew.Picture = _photoManager.MoveFromTemp(nutritionNew.Picture,
                        FileStorageTypes.NutritionNews, nutritionNew.Id, "Picture");
                }

                _newsServices.UpdateNutritionNew(nutritionNew, nutritionNew.Id);


            }
            return Ok();

        }


        public IHttpActionResult Put(int id, [FromBody] NutritionNewViewModel nutritionNewViewModel)
        {
            if (!_newsServices.NutritionNewExist(id))
            {
                return NotFound();
            }

            var nutritionNew = Mapper.Map<NutritionNewViewModel, NutritionNew>(nutritionNewViewModel);

            nutritionNew.Id = id;


            if (_photoManager.FileExists(nutritionNew.AuthorPicture))
            {
                nutritionNew.AuthorPicture = _photoManager.MoveFromTemp(nutritionNew.AuthorPicture,
                    FileStorageTypes.NutritionNews, nutritionNew.Id, "AuthorPicture");
            }

            if (_photoManager.FileExists(nutritionNew.MainPicture))
            {
                nutritionNew.MainPicture = _photoManager.MoveFromTemp(nutritionNew.MainPicture,
                    FileStorageTypes.NutritionNews, nutritionNew.Id, "MainPicture");
            }

            if (_photoManager.FileExists(nutritionNew.SponsoredBy))
            {
                nutritionNew.SponsoredBy = _photoManager.MoveFromTemp(nutritionNew.SponsoredBy,
                    FileStorageTypes.NutritionNews, nutritionNew.Id, "SponsoredBy");
            }

            if (_photoManager.FileExists(nutritionNew.Picture))
            {
                nutritionNew.Picture = _photoManager.MoveFromTemp(nutritionNew.Picture,
                    FileStorageTypes.NutritionNews, nutritionNew.Id, "Picture");
            }

            _newsServices.UpdateNutritionNew(nutritionNew, nutritionNew.Id);

            return Ok();


        }

        public IHttpActionResult Delete(int id)
        {
            _newsServices.DeleteNutritionNew(id);
            return Ok();
        }




    }
}
