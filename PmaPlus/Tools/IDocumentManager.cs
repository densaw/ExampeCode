using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.ViewModels.Document;

namespace PmaPlus.Tools
{
    public interface IDocumentManager
    {
        Task<string> AddDocument(HttpRequestMessage request, int userId);
        bool CreateDirectory(string dirName, int userId);
        IEnumerable<DirectoryInfo> GetUserDirectories(int userId);
        IEnumerable<FileViewModel> GetDirectoryFiles(string dirName, int userId);
        bool DeleteDirectory(string dirName, int userId);
        bool DeleteFile(string fileName, string dirName, int userId);
    }
}
