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

        //[HttpPost()]
        //public async Task<HttpResponseMessage> Post()
        //{
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    var streamProvider = new MultipartFormDataStreamProvider(HttpContext.Current.Server.MapPath("~/App_Data/temp"));
        //    List<string> files = new List<string>();

        //    try
        //    {
        //        // Read the MIME multipart content using the stream provider we just created.
        //        await Request.Content.ReadAsMultipartAsync(streamProvider);
        //        //await Request.Content.ReadAsMultipartAsync();

        //        foreach (MultipartFileData file in streamProvider.FileData)
        //        {
        //            files.Add(file.LocalFileName);
        //        }

        //        // Send OK Response along with saved file names to the client. 
        //        return Request.CreateResponse(HttpStatusCode.OK, files);
        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}

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
