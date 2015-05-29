using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class NutritionNewsController : ApiController
    {
        private readonly NewsServices _newsServices;

        public NutritionNewsController(NewsServices newsServices)
        {
            _newsServices = newsServices;
        }







    }
}
