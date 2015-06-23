using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.ViewModels.Document;
using PmaPlus.Models;

namespace PmaPlus.Tools
{
    public interface IPhotoManager
    {
        IEnumerable<PhotoViewModel> Get();
        PhotoActionResult Delete(string fileName);
        Task<PhotoViewModel> AddMassageWallPhoto(HttpRequestMessage request);
        Task<PhotoViewModel> Add(HttpRequestMessage request);
        string GetPhoto(FileStorageTypes type, string filename, int id);
        string Copy(string fileName, string path);
        string MoveFromTemp(string tempFileName, FileStorageTypes storageType, int id, string newFileName);
        string SetDefaultPrifilePic(FileStorageTypes storageType, int id, string newFileName);
        FileStream GetFileStream(string fileName, FileStorageTypes storageTypes, int id);
        FileStream GetFileStream(string fileName, FileStorageTypes storageTypes);
        byte[] GetFileBytes(string fileName, FileStorageTypes storageTypes, int id);
        bool FileExistInStorage(FileStorageTypes storageType, string fileName, int id);
        void Delete(FileStorageTypes storageType, int id);
        string Move(string fileName, string path, string newFileName);
        bool FileExists(string fileName);
        IList<FileViewModel> GetListOfDocuments(int userId);
    }
}
