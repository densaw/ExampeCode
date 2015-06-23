using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class SharingFoldersServices
    {
        private readonly ISharedFolderRepository _sharedFolderRepository;

        public SharingFoldersServices(ISharedFolderRepository sharedFolderRepository)
        {
            _sharedFolderRepository = sharedFolderRepository;
        }


        public IList<Role> GetDirectoryRoles(string dir, int id)
        {
            return _sharedFolderRepository.Get(f => f.FolderName.ToLower() == dir.ToLower()).Roles.Select(r => r.Role).ToList();
        }


        public void ShareDirectory(string name, int userId, IList<Role> roles)
        {
            if (_sharedFolderRepository.GetMany(f => f.FolderName.ToLower() == name.ToLower() && f.UserId == userId).Any())
            {
                var dir = _sharedFolderRepository.Get(f => f.FolderName.ToLower() == name.ToLower() && f.UserId == userId);

                foreach (var role in roles)
                {
                    if (dir.Roles.Any(r => r.Role == role))
                    {
                        dir.Roles.Add(new SharedFolderRole()
                        {
                            Role = role,
                            FolderId = dir.Id
                        });
                    }
                }

                foreach (var role in dir.Roles.Where(r => !roles.Contains(r.Role)).ToList())
                {
                    dir.Roles.Remove(role);
                }
            }
            else
            {
                var newDir = _sharedFolderRepository.Add(new SharedFolder()
                {
                    FolderName = name,
                    UserId = userId,
                });

                foreach (var role in roles)
                {
                    newDir.Roles.Add(new SharedFolderRole()
                    {
                        Role = role,
                        FolderId = newDir.Id
                    });
                }
            }

        }
    }
}
