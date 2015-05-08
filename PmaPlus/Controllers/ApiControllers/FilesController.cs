using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
       [Route("api/Files")]
        public async Task<IHttpActionResult> PostPhoto()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
            var photo = await _photoManager.Add(Request);
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
            FileStream _fileStream = _photoManager.GetFileStream(fileName, type, id);
            if (_fileStream == null)
            {
                result = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
            result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StreamContent(_fileStream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(Path.GetExtension(fileName)));
            }
            return result;
        }
     

    }
}
