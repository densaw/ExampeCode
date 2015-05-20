using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.Curriculums
{
    public class CurriculumStatementController : ApiController
    {
        private readonly CurriculumServices _curriculumServices;

        public CurriculumStatementController(CurriculumServices curriculumServices)
        {
            _curriculumServices = curriculumServices;
        }


        public IHttpActionResult Get()
        {
            return null;
        }


    }
}
