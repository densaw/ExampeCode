using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Services;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Physio;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Services.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class PhysioBodyPartsController : ApiController
    {
        private readonly PhysiotherapyServices _physiotherapyServices;
        private readonly IPhotoManager _photoManager;

        public PhysioBodyPartsController(PhysiotherapyServices physiotherapyServices, IPhotoManager photoManager)
        {
            _physiotherapyServices = physiotherapyServices;
            _photoManager = photoManager;
        }

        [Route("api/PhysioBodyParts/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public BodyPartPage Get(int pageSize, int pageNumber, string orderBy = "", bool direction = false)
        {
            var count = _physiotherapyServices.GetBodyParts().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var bodyParts = _physiotherapyServices.GetBodyParts();
            var items = Mapper.Map<IEnumerable<BodyPart>, IEnumerable<PhysioBodyPartTableViewModel>>(bodyParts).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

            return new BodyPartPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        [Route("api/PhysioBodyParts/GetAll")]
        public IEnumerable<PhysioBodyPartTableViewModel> GetAll()
        {
            var bodyParts = _physiotherapyServices.GetBodyParts();
            return Mapper.Map<IEnumerable<BodyPart>, IEnumerable<PhysioBodyPartTableViewModel>>(bodyParts);
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
            if (_photoManager.FileExists(newBodyPart.Picture))
            {
                newBodyPart.Picture = _photoManager.MoveFromTemp(newBodyPart.Picture, FileStorageTypes.PhysioBodyParts,
                    newBodyPart.Id, "BodyPart");
                _physiotherapyServices.UpdateBodyPart(newBodyPart,newBodyPart.Id);
            }
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
            if (_photoManager.FileExists(bodyPart.Picture))
            {
                bodyPart.Picture = _photoManager.MoveFromTemp(bodyPart.Picture, FileStorageTypes.PhysioBodyParts,
                    id, "BodyPart");
            }
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
            _photoManager.Delete(FileStorageTypes.PhysioBodyParts, id);
            return Ok();

        }
    }
}
