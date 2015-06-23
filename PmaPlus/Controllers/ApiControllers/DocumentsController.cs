using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels.Document;
using PmaPlus.Services;
using PmaPlus.Services.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class DocumentsController : ApiController
    {
        private readonly IDocumentManager _documentManager;
        private readonly SharingFoldersServices _sharingFoldersServices;
        private readonly UserServices _userServices;

        public DocumentsController(IDocumentManager documentManager, SharingFoldersServices sharingFoldersServices, UserServices userServices)
        {
            _documentManager = documentManager;
            _sharingFoldersServices = sharingFoldersServices;
            _userServices = userServices;
        }

        [Route("api/Documents/Directories")]
        public IEnumerable<DirectoryViewModel> GetDirectories()
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);




            var dirs = _documentManager.GetUserDirectories(user.Id);
            return dirs.Select(dir => new DirectoryViewModel()
            {
                Name = dir.Name,
                Roles = _sharingFoldersServices.GetDirectoryRoles(dir.Name, user.Id)
            }).ToList();
        }


        [Route("api/Documents/Directories")]
        public IHttpActionResult PostDirectory(DirectoryViewModel directory)
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            if (_documentManager.CreateDirectory(directory.Name, user.Id))
            {
                if (directory.Roles.Count > 0)
                {
                    _sharingFoldersServices.ShareDirectory(directory.Name, user.Id, directory.Roles);
                }
                return Ok();
            }


            return Conflict();
        }

    }
}
