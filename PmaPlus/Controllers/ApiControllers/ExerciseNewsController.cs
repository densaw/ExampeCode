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

namespace PmaPlus.Controllers.ApiControllers
{
    public class ExerciseNewsController : ApiController
    {
        private readonly NewsServices _newsServices;

        public ExerciseNewsController(NewsServices newsServices)
        {
            _newsServices = newsServices;
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



    }
}
