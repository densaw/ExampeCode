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
    public class HAFFitnessController : ApiController
    {
        private readonly HealthAndFitnessServices _healthAndFitnessServices;

        public HAFFitnessController(HealthAndFitnessServices healthAndFitnessServices)
        {
            _healthAndFitnessServices = healthAndFitnessServices;
        }


        [Route("api/HAFNutrition/News/Carusel")]
        public IEnumerable<PictureLinkViewModel> GetExerciseNews()
        {
            return _healthAndFitnessServices.GetExerciseNewsAcordion(10);
        }


    }
}
