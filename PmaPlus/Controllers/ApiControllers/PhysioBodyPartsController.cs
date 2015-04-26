using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Physio;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class PhysioBodyPartsController : ApiController
    {
        private readonly PhysiotherapyServices _physiotherapyServices;

        public PhysioBodyPartsController(PhysiotherapyServices physiotherapyServices)
        {
            _physiotherapyServices = physiotherapyServices;
        }

        [Route("api/PhysioBodyParts/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public BodyPartPage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _physiotherapyServices.GetBodyParts().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var bodyParts = _physiotherapyServices.GetBodyParts().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize);
            var items = Mapper.Map<IQueryable<BodyPart>,IQueryable<PhysioBodyPartTableViewModel>>(bodyParts);

            return new BodyPartPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        // GET: api/PhysioBodyParts
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/PhysioBodyParts/5
        public PhysioBodyPartViewModel Get(int id)
        {
            var bodyPart = _physiotherapyServices.GetBodyPartById(id);
            if (bodyPart == null)
            {
                return null;
            }
            return Mapper.Map<BodyPart, PhysioBodyPartViewModel>(bodyPart);
        }

        // POST: api/PhysioBodyParts
        public IHttpActionResult Post([FromBody]PhysioBodyPartViewModel bodyPartViewModel)
        {
            var bodyPart = Mapper.Map<PhysioBodyPartViewModel, BodyPart>(bodyPartViewModel);
            var newBodyPart =_physiotherapyServices.AddBodyPart(bodyPart);
            return Created(Request.RequestUri + newBodyPart.Id.ToString(),newBodyPart);
        }

        // PUT: api/PhysioBodyParts/5
        public IHttpActionResult Put(int id, [FromBody]PhysioBodyPartViewModel bodyPartViewModel)
        {
            if (!_physiotherapyServices.BodyPartExist(id))
            {
                return NotFound();
            }
            var bodyPart = Mapper.Map<PhysioBodyPartViewModel, BodyPart>(bodyPartViewModel);
            _physiotherapyServices.UpdateBodyPart(bodyPart,id);
            return Ok();

        }

        // DELETE: api/PhysioBodyParts/5
        public IHttpActionResult Delete(int id)
        {
            if (!_physiotherapyServices.BodyPartExist(id))
            {
                return NotFound();
            }
            _physiotherapyServices.DeleteBodyPart(id);
            return Ok();

        }
    }
}
