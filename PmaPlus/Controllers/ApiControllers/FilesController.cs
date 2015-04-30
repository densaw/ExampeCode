using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class FilesController : ApiController
    {
        private readonly IPhotoManager _photoManager;

        public FilesController()
        {
            _photoManager = new LocalPhotoManager(HttpContext.Current.Server.MapPath(@"~/App_Data/temp"));
        }

        public IHttpActionResult PostPhoto()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
            var photo = _photoManager.Add(Request);
            return Ok(photo);
            }

                return BadRequest("Unsuported type");
        }

     

    }
}
