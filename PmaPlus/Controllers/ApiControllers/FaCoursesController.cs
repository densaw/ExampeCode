﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;
using Microsoft.Ajax.Utilities;
using Microsoft.Data.OData;
using PmaPlus.Model.Models;
using PmaPlus.Services.Services;
using System.Web.OData.Query;
using PmaPlus.Data;

namespace PmaPlus.Controllers
{
    public class FaCoursesController : ApiController
    {
        private readonly FaCourseServices _faCourseServices;
        public FaCoursesController(FaCourseServices faCourseServices)
        {
            _faCourseServices = faCourseServices;
        }

        // GET: api/Clubs/pageSize/pageNumber/orderBy(optional) 
        [Route("api/FaCourses/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public IHttpActionResult Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _faCourseServices.GetFaCourses().Count();
            var pages = Math.Ceiling((double)count / pageSize);
            var items = _faCourseServices.GetFaCourses().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize);

            var result = new
            {
                Count = count,
                Pages = pages,
                Items = items
            };

            return Ok(result);
        }

      

         //GET: api/FaCourses
        public IQueryable<FACourse> Get()
        {
            return _faCourseServices.GetFaCourses();
        }

        // GET: api/FaCourses/5
        public  FACourse Get(int id)
        {
            return _faCourseServices.GetFaCourseById(id);
        }

        // POST: api/FaCourses
        public IHttpActionResult PostFaCourse([FromBody]FACourse faCourse)
        {
            var newFaCourse = _faCourseServices.AddFaCourse(faCourse);
            
            return Created(Request.RequestUri + newFaCourse.Id.ToString(), newFaCourse);
        }

        // PUT: api/FaCourses/5
        public IHttpActionResult PutFacourse(int id, [FromBody]FACourse faCourse)
        {
            if (!_faCourseServices.FaCourseExist(id))
                return NotFound();

            _faCourseServices.UpdateFaCourse(faCourse,id);
            return Ok();
        }

        // DELETE: api/FaCourses/5
        public IHttpActionResult Delete(int id)
        {
            if (!_faCourseServices.FaCourseExist(id))
                return NotFound();
            _faCourseServices.Delete(id);
            return Ok();
        }
    }
}
