using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Data;
using PmaPlus.Model;
using PmaPlus.Model.ViewModels.Club;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class ClubsController : ApiController
    {
        private readonly ClubServices _clubServices;

        public ClubsController(ClubServices clubServices)
        {
            _clubServices = clubServices;
        }

        [Route("api/Clubs/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public ClubPage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _clubServices.GetClubsTableViewModels().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var items = _clubServices.GetClubsTableViewModels().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize);

            return new ClubPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        // GET: api/Clubs
        public IEnumerable<ClubTableViewModel> Get()
        {
            return _clubServices.GetClubsTableViewModels();
        }

        // GET: api/Clubs/5
        public AddClubViewModel Get(int id)
        {
            return _clubServices.GetClubById(id);
        }

        // POST: api/Clubs
        public IHttpActionResult Post([FromBody]AddClubViewModel value)
        {
            var newClub = _clubServices.AddClub(value);
            return Created(Request.RequestUri + newClub.Id.ToString(), newClub);
        }

        // PUT: api/Clubs/5
        public IHttpActionResult Put(int id, [FromBody]AddClubViewModel value)
        {
            if (!_clubServices.ClubIsExist(id))
            {
                return NotFound();
            }
            _clubServices.UpdateClub(value,id);
            return Ok();
        }

        // DELETE: api/Clubs/5
        public IHttpActionResult Delete(int id)
        {
            if (!_clubServices.ClubIsExist(id))
            {
                return NotFound();
            }
            _clubServices.DeleteClub(id);
            return Ok();

        }
    }
}
