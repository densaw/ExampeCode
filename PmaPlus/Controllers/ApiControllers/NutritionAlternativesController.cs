using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.Nutrition;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class NutritionAlternativesController : ApiController
    {
        private readonly NutritionServices _nutritionServices;

        public NutritionAlternativesController(NutritionServices nutritionServices)
        {
            _nutritionServices = nutritionServices;
        }


        [Route("api/NutritionAlternatives/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public AlternativesPage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _nutritionServices.GetAlternatives().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var alternatives = _nutritionServices.GetAlternatives().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize).AsEnumerable();
            var items = Mapper.Map<IEnumerable<NutritionAlternative>, IEnumerable<NutritionAlternativeTableViewModel>>(alternatives);

            return new AlternativesPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        // GET: api/NutritionAlternatives/
        public IEnumerable<NutritionAlternativeTableViewModel> Get()
        {
            return Mapper.Map<IEnumerable<NutritionAlternative>, IEnumerable<NutritionAlternativeTableViewModel>>(_nutritionServices.GetAlternatives());
        }

        // GET: api/NutritionAlternatives/5
        public NutritionAlternativeViewModel Get(int id)
        {
            return Mapper.Map<NutritionAlternative, NutritionAlternativeViewModel>(_nutritionServices.GetAlternativeById(id));
        }

        // POST: api/NutritionAlternatives/
        public IHttpActionResult Post([FromBody]NutritionAlternativeViewModel alternativeViewModel)
        {
            var alternative = Mapper.Map<NutritionAlternativeViewModel, NutritionAlternative>(alternativeViewModel);
            var newAlternative = Mapper.Map<NutritionAlternative, NutritionAlternativeViewModel>(_nutritionServices.AddAlternative(alternative));
            return Created(Request.RequestUri + newAlternative.Id.ToString(), newAlternative);
        }

        // PUT: api/NutritionAlternatives/5
        public IHttpActionResult Put(int id, [FromBody]NutritionAlternativeViewModel alternativeViewModel)
        {
            if (!_nutritionServices.AlternativeExist(id))
            {
                return NotFound();
            }
            var alternative = Mapper.Map<NutritionAlternativeViewModel, NutritionAlternative>(alternativeViewModel);
            _nutritionServices.UpdateAlternative(alternative, id);
            return Ok();
        }

        // DELETE: api/NutritionAlternatives/5
        public IHttpActionResult Delete(int id)
        {
            if (!_nutritionServices.AlternativeExist(id))
            {
                return NotFound();
            }
            _nutritionServices.DeleteAlternative(id);
            return Ok();
        }
    }}
