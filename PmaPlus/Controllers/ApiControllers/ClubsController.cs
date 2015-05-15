using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private readonly UserServices _userServices;

        public ClubsController(ClubServices clubServices, IPhotoManager photoManager, UserServices userServices)
        {
            _clubServices = clubServices;
            _photoManager = photoManager;
            _userServices = userServices;
            //_photoManager = new LocalPhotoManager(HttpContext.Current.Server.MapPath(@"~/App_Data/temp"));
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

        [Route("api/Clubs/Current")]
        public AddClubViewModel GetCurrentClub()
        {

            return _clubServices.GetClubById(_userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id);
        }


        // GET: api/Clubs/5
        public AddClubViewModel Get(int id)
        {
            return _clubServices.GetClubById(id);
        }

        [Route("api/Clubs/{id}/logo")]
        public HttpResponseMessage GetLogo(int id)
        {
            if (!_clubServices.ClubIsExist(id))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var tempClub = _clubServices.GetClubById(id);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(_photoManager.GetFileStream(tempClub.Logo,FileStorageTypes.Clubs, id));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(Path.GetExtension(tempClub.Logo)));
            return result;
        }

        [Route("api/Clubs/{id}/background")]
        public HttpResponseMessage GetBackground(int id)
        {
            if (!_clubServices.ClubIsExist(id))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var tempClub = _clubServices.GetClubById(id);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(_photoManager.GetFileStream(tempClub.Background, FileStorageTypes.Clubs, id));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(Path.GetExtension(tempClub.Background)));
            return result;

        }

        // POST: api/Clubs
        public IHttpActionResult Post([FromBody]AddClubViewModel clubViewModel)
        {
            var newClub = _clubServices.AddClub(clubViewModel);
            if (newClub != null)
            {
                newClub.Logo = _photoManager.MoveFromTemp(newClub.Logo, FileStorageTypes.Clubs, newClub.Id,"logo");
                newClub.Background = _photoManager.MoveFromTemp(newClub.Background, FileStorageTypes.Clubs, newClub.Id, "Background");
                _clubServices.UpdateClub(newClub, newClub.Id);
                return Created(Request.RequestUri + newClub.Id.ToString(), newClub);
            }
            return BadRequest();
        }

        // PUT: api/Clubs/5
        public IHttpActionResult Put(int id, [FromBody]AddClubViewModel clubViewModel)
        {
            if (!_clubServices.ClubIsExist(id))
            {
                return NotFound();
            }
            if (!clubViewModel.Logo.Contains("logo"))
            {
                clubViewModel.Logo = _photoManager.MoveFromTemp(clubViewModel.Logo, FileStorageTypes.Clubs, clubViewModel.Id,"logo");
            }
            if (!clubViewModel.Logo.Contains("Background"))
            {
                clubViewModel.Background = _photoManager.MoveFromTemp(clubViewModel.Background, FileStorageTypes.Clubs,
                    clubViewModel.Id, "Background");
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
            _photoManager.Delete(FileStorageTypes.Clubs, id);
            return Ok();

        }
    }
}
