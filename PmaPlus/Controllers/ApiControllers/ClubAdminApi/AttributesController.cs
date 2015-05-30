using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.PlayerAttribute;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class AttributesController : ApiController
    {
        private readonly PlayerAttributeServices _playerAttributeServices;

        public AttributesController(PlayerAttributeServices playerAttributeServices)
        {
            _playerAttributeServices = playerAttributeServices;
        }

        // GET: api/Attributes
        [Route("api/Attributes/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:alpha?}")]
        public PlayerAttributePage Get(int pageSize, int pageNumber, string orderBy = "",string direction = "")
        {
            var count = _playerAttributeServices.GetPlayerAttributes().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var scenario = _playerAttributeServices.GetPlayerAttributes();
            var items = Mapper.Map<IEnumerable<PlayerAttribute>, IEnumerable<PlayerAttributeTableViewModel>>(scenario).OrderQuery(orderBy,x => x.Id,direction).Paged(pageNumber, pageSize);

            return new PlayerAttributePage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        // GET: api/Attributes/5
        public PlayerAttributeViewModel Get(int id)
        {
            return Mapper.Map<PlayerAttribute, PlayerAttributeViewModel>(_playerAttributeServices.GetPlayerAttributeById(id));
        }

        // POST: api/Attributes
        public IHttpActionResult Post([FromBody]PlayerAttributeViewModel scenarioViewModel)
        {
            var scenario = Mapper.Map<PlayerAttributeViewModel, PlayerAttribute>(scenarioViewModel);
          
            var newPlayerAttribute = _playerAttributeServices.AddPlayerAttribute(scenario);
            return Created(Request.RequestUri + newPlayerAttribute.Id.ToString(), newPlayerAttribute);
        }

        // PUT: api/Attributes/5
        public IHttpActionResult Put(int id, [FromBody]PlayerAttributeViewModel scenarioViewModel)
        {
            if (!_playerAttributeServices.PlayerAttributeExist(id))
            {
                return NotFound();
            }
            var scenario = Mapper.Map<PlayerAttributeViewModel, PlayerAttribute>(scenarioViewModel);
            _playerAttributeServices.UpdatePlayerAttribute(scenario, id);
            return Ok();
        }

        // DELETE: api/Attributes/5
        public IHttpActionResult Delete(int id)
        {
            if (!_playerAttributeServices.PlayerAttributeExist(id))
            {
                return NotFound();
            }
            _playerAttributeServices.DeletePlayerAttribute(id);
            return Ok();

        }





    }
}
