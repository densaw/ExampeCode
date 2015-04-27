using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.Nutrition;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class NutritionFoodTypesController : ApiController
    {
        private readonly NutritionServices _nutritionServices;

        public NutritionFoodTypesController(NutritionServices nutritionServices)
        {
            _nutritionServices = nutritionServices;
        }


        [Route("api/NutritionFoodTypes/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public FoodTypePage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _nutritionServices.GetFoodTypes().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var foodTypes = _nutritionServices.GetFoodTypes().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize).AsEnumerable();
            var items = Mapper.Map<IEnumerable<NutritionFoodType>, IEnumerable<NutritionFoodTypeTableViewModel>>(foodTypes);

            return new FoodTypePage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        // GET: api/NutritionFoodTypes/
        public IEnumerable<NutritionFoodTypeTableViewModel> Get()
        {
            return Mapper.Map<IEnumerable<NutritionFoodType>, IEnumerable<NutritionFoodTypeTableViewModel>>(_nutritionServices.GetFoodTypes());
        }

        // GET: api/NutritionFoodTypes/5
        public NutritionFoodTypeViewModel Get(int id)
        {
            return Mapper.Map<NutritionFoodType, NutritionFoodTypeViewModel>(_nutritionServices.GetFoodTypeById(id));
        }

        // POST: api/NutritionFoodTypes/
        public IHttpActionResult Post([FromBody]NutritionFoodTypeViewModel foodTypeViewModel)
        {
            var foodType = Mapper.Map<NutritionFoodTypeViewModel, NutritionFoodType>(foodTypeViewModel);
            var newFoodType = Mapper.Map<NutritionFoodType, NutritionFoodTypeViewModel>(_nutritionServices.AddFoodType(foodType));
            return Created(Request.RequestUri + newFoodType.Id.ToString(), newFoodType);
        }

        // PUT: api/NutritionFoodTypes/5
        public IHttpActionResult Put(int id, [FromBody]NutritionFoodTypeViewModel foodTypeViewModel)
        {
            if (!_nutritionServices.FoodTypeExist(id))
            {
                return NotFound();
            }
            var foodType = Mapper.Map<NutritionFoodTypeViewModel, NutritionFoodType>(foodTypeViewModel);
            _nutritionServices.UpdateFoodType(foodType, id);
            return Ok();
        }

        // DELETE: api/NutritionFoodTypes/5
        public IHttpActionResult Delete(int id)
        {
            if (!_nutritionServices.FoodTypeExist(id))
            {
                return NotFound();
            }
            _nutritionServices.DeleteFoodType(id);
            return Ok();
        }
    }
}

