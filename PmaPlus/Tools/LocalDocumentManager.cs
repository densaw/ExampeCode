using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using PmaPlus.Filters;
using PmaPlus.Model.ViewModels.Document;

namespace PmaPlus.Tools
{
    public class LocalDocumentManager : IDocumentManager
    {
        private readonly string _workingFolder;

        public LocalDocumentManager(string workingFolder)
        {
            _workingFolder = workingFolder;
        }


        public async Task<string> AddDocument(HttpRequestMessage request,int userId)
        {
            string userPath = _workingFolder + "\\" + userId;
            if (!Directory.Exists(userPath))
            {
                Directory.CreateDirectory(userPath);
            }

            var provider = new DocumentStreamProvider(_workingFolder + "\\" + userId);

            await request.Content.ReadAsMultipartAsync(provider);

            var file = provider.FileData.First();
            if (file == null)
            {
                return "";
            }

            var fileInfo = new FileInfo(file.LocalFileName);

            return fileInfo.Name;
        }



        public bool CreateDirectory(string dirName, int userId)
        {
            try
            {
                Directory.CreateDirectory(String.Format("{0}\\{1}\\{2}",_workingFolder, userId, dirName));
            }
            catch (Exception)
            {return false;}
            
            return true;
        }

        public IEnumerable<string> GetUserDirectories(int userId)
        {
            if (userId>0)
            {
                return Directory.GetDirectories(String.Format("{0}\\{1}\\", _workingFolder, userId));
            }
            return null;
        }

    }
}