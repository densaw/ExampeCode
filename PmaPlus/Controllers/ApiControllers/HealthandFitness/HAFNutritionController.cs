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
    public class HAFNutritionController : ApiController
    {
        private readonly HealthAndFitnessServices _healthAndFitnessServices;

        public HAFNutritionController(HealthAndFitnessServices healthAndFitnessServices)
        {
            _healthAndFitnessServices = healthAndFitnessServices;
        }

        [Route("api/HAFNutrition/News/Carusel")]
        public IEnumerable<PictureLinkViewModel> GetNutritionNews()
        {
            return _healthAndFitnessServices.GetNutritionNewsAcordion(10);
        }

        [Route("api/HAFNutrition/Recipes/Last")]
        public string GetLastRecipePic()
        {
            return _healthAndFitnessServices.GetLastEcipePicture();
        }




    }
}
