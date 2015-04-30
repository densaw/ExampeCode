using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace PmaPlus.Filters
{
    public class PhotoStreamProvider : MultipartFormDataStreamProvider
    {
        public PhotoStreamProvider(string uploadPath)
                : base(uploadPath)
            {

            }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            // restrict what images can be selected
            var extensions = new[] { "png", "bmp", "jpg" };
            var filename = headers.ContentDisposition.FileName.Replace("\"", string.Empty);

            if (filename.IndexOf('.') < 0)
                return Stream.Null;

            var extension = filename.Split('.').Last();

            return extensions.Any(i => i.Equals(extension, StringComparison.InvariantCultureIgnoreCase))
                       ? base.GetStream(parent, headers)
                       : Stream.Null;

        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            // override the filename which is stored by the provider (by default is bodypart_x)
            string oldfileName = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(oldfileName);

            return newFileName;
        }
    }
}