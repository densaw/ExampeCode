using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels.Document;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class DocumentsController : ApiController
    {
        private readonly IDocumentManager _documentManager;

        public DocumentsController(IDocumentManager documentManager)
        {
            _documentManager = documentManager;
        }


        [Route("api/File/Doc/{userId:int}")]
        public IList<FileViewModel> GetDir(int userId)
        {
            return null;
        }
    }
}
