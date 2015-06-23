using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace PmaPlus.Filters
{
    public class DocumentStreamProvider : MultipartFormDataStreamProvider
    {
        public DocumentStreamProvider(string uploadPath)
                : base(uploadPath)
            {
            }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            var filename = headers.ContentDisposition.FileName.Replace("\"", string.Empty);

            if (filename.IndexOf('.') < 0)
                return Stream.Null;

            return base.GetStream(parent, headers);

        }
        
    }
}