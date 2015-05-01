using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class FilesController : ApiController
    {
        private readonly IPhotoManager _photoManager;

        public FilesController(IPhotoManager photoManager)
        {
            _photoManager = photoManager;
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

        [Route("api/File/{storageType}/{fileName}/{id}")]
        public HttpResponseMessage GetPhoto(string storageType,string fileName , int id)
        {
            HttpResponseMessage result;
            FileStorageTypes type;
            Enum.TryParse(storageType,true, out type);
            var photoPath = _photoManager.GetPhoto(type, fileName, id);
            if (File.Exists(photoPath))
            {
            result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StreamContent(new FileStream(photoPath, FileMode.Open, FileAccess.Read));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(Path.GetExtension(fileName)));
                
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            

            return result;
        }
     

    }
}
