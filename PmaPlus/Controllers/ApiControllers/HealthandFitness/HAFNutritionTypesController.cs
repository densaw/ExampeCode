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
    public class HAFNutritionTypesController : ApiController
    {
        private readonly HealthAndFitnessServices _healthAndFitnessServices;

        public HAFNutritionTypesController(HealthAndFitnessServices healthAndFitnessServices)
        {
            _healthAndFitnessServices = healthAndFitnessServices;
        }


        [Route("api/HAFNutritionTypes/FoodTypes/List")]
        public IEnumerable<PictureLinkViewModel> GetNutritionFoodTypesList()
        {
            return _healthAndFitnessServices.GetNutritionFoodTypesList(10);
        }






    }
}
