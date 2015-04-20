using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model;
using PmaPlus.Model.ViewModels.Club;
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
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clubs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clubs/5
        public void Delete(int id)
        {
        }
    }
}
