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
    public class NutritionRecipesController : ApiController
    {
        private readonly NutritionServices _nutritionServices;

        public NutritionRecipesController(NutritionServices nutritionServices)
        {
            _nutritionServices = nutritionServices;
        }


        [Route("api/NutritionRecipes/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public RecipePage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _nutritionServices.GetRecipes().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var recipes = _nutritionServices.GetRecipes().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize).AsEnumerable();
            var items = Mapper.Map<IEnumerable<NutritionRecipe>, IEnumerable<NutritionRecipeTableViewModel>>(recipes);

            return new RecipePage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        // GET: api/NutritionRecipes/
        public IEnumerable<NutritionRecipeTableViewModel> Get()
        {
            return Mapper.Map<IEnumerable<NutritionRecipe>, IEnumerable<NutritionRecipeTableViewModel>>(_nutritionServices.GetRecipes());
        }

        // GET: api/NutritionRecipes/5
        public NutritionRecipeViewModel Get(int id)
        {
            return Mapper.Map<NutritionRecipe, NutritionRecipeViewModel>(_nutritionServices.GetRecipeById(id));
        }

        // POST: api/NutritionRecipes/
        public IHttpActionResult Post([FromBody]NutritionRecipeViewModel recipeViewModel)
        {
            var recipe = Mapper.Map<NutritionRecipeViewModel, NutritionRecipe>(recipeViewModel);
            var newRecipe = Mapper.Map<NutritionRecipe, NutritionRecipeViewModel>(_nutritionServices.AddRecipe(recipe));
            return Created(Request.RequestUri + newRecipe.Id.ToString(), newRecipe);
        }

        // PUT: api/NutritionRecipes/5
        public IHttpActionResult Put(int id, [FromBody]NutritionRecipeViewModel recipeViewModel)
        {
            if (!_nutritionServices.RecipeExist(id))
            {
                return NotFound();
            }
            var foodType = Mapper.Map<NutritionRecipeViewModel, NutritionRecipe>(recipeViewModel);
            _nutritionServices.UpdateRecipe(foodType, id);
            return Ok();
        }

        // DELETE: api/NutritionRecipes/5
        public IHttpActionResult Delete(int id)
        {
            if (!_nutritionServices.RecipeExist(id))
            {
                return NotFound();
            }
            _nutritionServices.DeleteRecipe(id);
            return Ok();
        }
    }

}
