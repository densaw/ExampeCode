using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Web;
using System.Web.Http;
using PmaPlus.Data;
using PmaPlus.Filters;
using PmaPlus.Model;
using PmaPlus.Model.ViewModels.Club;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class ClubsController : ApiController
    {
        private readonly ClubServices _clubServices;
        private readonly IPhotoManager _photoManager;

        public ClubsController(ClubServices clubServices)
        {
            _clubServices = clubServices;
            _photoManager = new LocalPhotoManager(HttpContext.Current.Server.MapPath(@"~/App_Data/temp"));
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
        public IHttpActionResult Post([FromBody]AddClubViewModel clubViewModel)
        {
            var newClub = _clubServices.AddClub(clubViewModel);
            if (newClub != null)
            {
                string logoPath = @"~/Images/Clubs/" + newClub.Id;
                newClub.Logo = _photoManager.Move(newClub.Logo, logoPath + "/",newClub.Name + "_Logo");
                newClub.Background = _photoManager.Move(newClub.Background, logoPath + "/" ,newClub.Name + "_Background");
                _clubServices.UpdateClub(newClub,newClub.Id);
            }
            return Created(Request.RequestUri + newClub.Id.ToString(), newClub);
        }

        // PUT: api/Clubs/5
        public IHttpActionResult Put(int id, [FromBody]AddClubViewModel clubViewModel)
        {
            if (!_clubServices.ClubIsExist(id))
            {
                return NotFound();
            }
            string logoPath = @"~/Images/Clubs/" + id;
            var temp = _clubServices.GetClubById(id);
            if (temp.Logo != clubViewModel.Logo)
            {
                clubViewModel.Logo = _photoManager.Move(clubViewModel.Logo, logoPath + "/", clubViewModel.Name + "_Logo");
            }
            if (temp.Background != clubViewModel.Background)
            {
                clubViewModel.Background = _photoManager.Move(clubViewModel.Background, logoPath + "/", clubViewModel.Name + "_Background");
            }

            _clubServices.UpdateClub(clubViewModel, id);
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
