using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.Curriculums
{
    public class SessionsController : ApiController
    {
        private readonly CurriculumServices _curriculumServices;

        public SessionsController(CurriculumServices curriculumServices)
        {
            _curriculumServices = curriculumServices;
        }

        




    }
}
