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

        public async Task<string> AddDocument(HttpRequestMessage request,string folder ,int userId)
        {
            string userPath = _workingFolder + "\\" + userId + "\\" + folder;
            if (!Directory.Exists(userPath))
            {
                Directory.CreateDirectory(userPath);
            }

            var provider = new DocumentStreamProvider(userPath);

            await request.Content.ReadAsMultipartAsync(provider);

            var file = provider.FileData.First();
            if (file == null)
            {
                return "";
            }

            var fileInfo = new FileInfo(file.LocalFileName);

            return fileInfo.Name;
        }

        public FileStream GetFileStream(string fileName, string folder, int userId)
        {
            var fullFilePath = _workingFolder + "\\" + userId + "\\" + folder +"\\" + fileName;
            if (File.Exists(fullFilePath))
            {
                return new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
            }
            return null;
        }

        public bool CreateDirectory(string dirName, int userId)
        {
            try
            {
                Directory.CreateDirectory(String.Format("{0}\\{1}\\{2}", _workingFolder, userId, dirName));
            }
            catch (Exception)
            { return false; }

            return true;
        }

        public IEnumerable<DirectoryInfo> GetUserDirectories(int userId)
        {
            var dirPath = String.Format("{0}\\{1}\\", _workingFolder, userId);
            
                DirectoryInfo info = new DirectoryInfo(dirPath);
                if (info.Exists)
                {
                    return info.EnumerateDirectories();
                }
                return null;
        }

        public IEnumerable<FileViewModel> GetDirectoryFiles(string dirName, int userId)
        {
            var dirPath = String.Format("{0}\\{1}\\{2}", _workingFolder, userId, dirName);

            if (Directory.Exists(dirPath))
            {
                DirectoryInfo dir = new DirectoryInfo(dirPath);

                return from file in dir.EnumerateFiles()
                       select new FileViewModel()
                       {
                           Name = file.Name,
                           Size = file.Length,
                           FileType = file.Extension,
                           AddDate = file.CreationTime
                       };

            }
            return null;
        }

        public bool DeleteDirectory(string dirName, int userId)
        {
            var dirPath = String.Format("{0}\\{1}\\{2}", _workingFolder, userId, dirName);

            if (Directory.Exists(dirPath))
            {
                try
                {
                    Directory.Delete(dirPath, true);
                }
                catch (Exception)
                {
                    return false;
                }


            }
            return false;
        }

        public bool DeleteFile(string fileName, string dirName, int userId)
        {
            var filePath = String.Format("{0}\\{1}\\{2}\\{3}", _workingFolder, userId, dirName, fileName);
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception)
                {
                    return true;
                }
            }
            return false;
        }

    }
}