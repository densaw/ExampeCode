using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Models;

namespace PmaPlus.Tools
{
    public interface IPhotoManager
    {
        IEnumerable<PhotoViewModel> Get();
        PhotoActionResult Delete(string fileName);
        Task<IEnumerable<PhotoViewModel>> Add(HttpRequestMessage request);
        string GetPhoto(FileStorageTypes type, string filename, int id);
        string Copy(string fileName, string path);
        string Move(string fileName, string path, string newFileName);
        bool FileExists(string fileName); 
    }
}
