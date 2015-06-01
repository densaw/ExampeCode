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
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class NutritionRecipesController : ApiController
    {
        private readonly NutritionServices _nutritionServices;
        private readonly IPhotoManager _photoManager;

        public NutritionRecipesController(NutritionServices nutritionServices, IPhotoManager photoManager)
        {
            _nutritionServices = nutritionServices;
            _photoManager = photoManager;
        }


        [Route("api/NutritionRecipes/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public RecipePage Get(int pageSize, int pageNumber, string orderBy = "",bool direction = false)
        {
            var count = _nutritionServices.GetRecipes().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var recipes = _nutritionServices.GetRecipes().AsEnumerable();
            var items = Mapper.Map<IEnumerable<NutritionRecipe>, IEnumerable<NutritionRecipeViewModel>>(recipes).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize); ;

            return new RecipePage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        // GET: api/NutritionRecipes/
        public IEnumerable<NutritionRecipeViewModel> Get()
        {
            return Mapper.Map<IEnumerable<NutritionRecipe>, IEnumerable<NutritionRecipeViewModel>>(_nutritionServices.GetRecipes());
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
            var newRecipe = _nutritionServices.AddRecipe(recipe);
            if (_photoManager.FileExists(newRecipe.Picture))
            {
                newRecipe.Picture = _photoManager.MoveFromTemp(newRecipe.Picture, FileStorageTypes.Recipes, newRecipe.Id,
                    "Recipe");
                _nutritionServices.UpdateRecipe(newRecipe,newRecipe.Id);
            }

            return Created(Request.RequestUri + newRecipe.Id.ToString(), newRecipe);
        }

        // PUT: api/NutritionRecipes/5
        public IHttpActionResult Put(int id, [FromBody]NutritionRecipeViewModel recipeViewModel)
        {
            if (!_nutritionServices.RecipeExist(id))
            {
                return NotFound();
            }
            var recipe = Mapper.Map<NutritionRecipeViewModel, NutritionRecipe>(recipeViewModel);
            if (_photoManager.FileExists(recipe.Picture))
            {
                recipe.Picture = _photoManager.MoveFromTemp(recipe.Picture, FileStorageTypes.Recipes, id,
                    "Recipe");
            }
            _nutritionServices.UpdateRecipe(recipe, id);
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
            _photoManager.Delete(FileStorageTypes.Recipes, id);
            return Ok();
        }
    }

}
