using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.HealthandFitness
{
    public class HAFNutritionRecipesController : ApiController
    {
        private readonly HealthAndFitnessServices _healthAndFitnessServices;

        public HAFNutritionRecipesController(HealthAndFitnessServices healthAndFitnesServices)
        {
            _healthAndFitnessServices = healthAndFitnesServices;
        }


        [Route("api/HAFNutritionRecipes/Recipes/List")]
        public IEnumerable<PictureLinkViewModel> GetNutritionRecipesList()
        {
            return _healthAndFitnessServices.GetNutritionRecipesList(10);
        }



    }
}
