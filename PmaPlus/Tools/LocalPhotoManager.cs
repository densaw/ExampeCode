using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using PmaPlus.Filters;
using PmaPlus.Models;

namespace PmaPlus.Tools
{
    public class LocalPhotoManager : IPhotoManager
    {
        private readonly string _workingFolder;


        public LocalPhotoManager(string workingFolder)
        {
            _workingFolder = workingFolder;
            CheckTargetDirectory();
        }

        public string GetPhoto(FileStorageTypes type, string filename, int id)
        {
            string filePath = "";
            switch (type)
            {
                case FileStorageTypes.PhysioBodyParts:
                    filePath = _workingFolder + "\\" + FileStorageTypes.PhysioBodyParts.ToString();
                    break;
                default:
                    break;
            }

            filePath += "\\" + id + "\\" + filename;


            return filePath;
        }

        public IEnumerable<PhotoViewModel> Get()
        {
            List<PhotoViewModel> photos = new List<PhotoViewModel>();

            DirectoryInfo photoFolder = new DirectoryInfo(_workingFolder);

            photos = photoFolder.EnumerateFiles()
                                        .Where(fi => new[] { ".jpg", ".bmp", ".png" }.Contains(fi.Extension.ToLower()))
                                        .Select(fi => new PhotoViewModel
                                        {
                                            Name = fi.Name,
                                            //Created = fi.CreationTime,
                                            //Modified = fi.LastWriteTime,
                                            //Size = fi.Length / 1024
                                        })
                                        .ToList();

            return photos;
        }

        public PhotoActionResult Delete(string fileName)
        {
            try
            {
                var filePath = Directory.GetFiles(_workingFolder, fileName)
                                .FirstOrDefault();

                File.Delete(filePath);

                return new PhotoActionResult { Successful = true, Message = fileName + "deleted successfully" };
            }
            catch (Exception ex)
            {
                return new PhotoActionResult { Successful = false, Message = "error deleting fileName " + ex.GetBaseException().Message };
            }
        }


        public async Task<PhotoViewModel> AddMassageWallPhoto(HttpRequestMessage request)
        {
            var provider = new PhotoStreamProvider(_workingFolder + "\\" + FileStorageTypes.MessageWall.ToString());

            await request.Content.ReadAsMultipartAsync(provider);

            var photos = new List<PhotoViewModel>();

            var file = provider.FileData.First();
            if (file == null)
            {
                return null;
            }

            var fileInfo = new FileInfo(file.LocalFileName);

            return new PhotoViewModel
            {
                Name = fileInfo.Name,
                //Created = fileInfo.CreationTime,
                //Modified = fileInfo.LastWriteTime,
                //Size = fileInfo.Length / 1024
            };

        }



        public async Task<PhotoViewModel> Add(HttpRequestMessage request)
        {
            var provider = new PhotoStreamProvider(_workingFolder + "\\temp");

            await request.Content.ReadAsMultipartAsync(provider);

            var photos = new List<PhotoViewModel>();

            //foreach (var file in provider.FileData)
            //{
            var file = provider.FileData.First();
            if (file == null)
            {
                return null;
            }

            var fileInfo = new FileInfo(file.LocalFileName);

            return new PhotoViewModel
            {
                Name = fileInfo.Name,
                //Created = fileInfo.CreationTime,
                //Modified = fileInfo.LastWriteTime,
                //Size = fileInfo.Length / 1024
            };
            //}

        }

        /// <summary>
        /// Move file from temp storage
        /// </summary>
        /// <param name="tempFileName">Temp file name</param>
        /// <param name="storageType"> </param>
        /// <param name="id">ID table row</param>
        /// <param name="newFileName">new file name without extention</param>
        /// <returns></returns>
        public string MoveFromTemp(string tempFileName, FileStorageTypes storageType, int id, string newFileName)
        {
            var tempFilePath = _workingFolder + "\\" + "temp" + "\\" + tempFileName;
            if (File.Exists(tempFilePath))
            {
                var newPath = _workingFolder + "\\" + storageType.ToString() + "\\" + id + "\\";
                var newFileFullPath = newPath + newFileName + Path.GetExtension(tempFileName);
                try
                {
                    Directory.CreateDirectory(newPath);
                    foreach (string file in Directory.GetFiles(newPath, "*" + newFileName + "*"))
                    {
                        File.Delete(file);
                    }
                    File.Move(tempFilePath, newFileFullPath);
                    return newFileName + Path.GetExtension(tempFileName);

                }
                catch (Exception)
                {

                    return "";
                }



            }
            else
            {
                return "";
            }

        }

        public string SetDefaultPrifilePic(FileStorageTypes storageType, int id, string newFileName)
        {
            var tempFilePath = _workingFolder + "\\" + "DefaultPics" + "\\" + "ProfilePicture.jpg";
            if (File.Exists(tempFilePath))
            {
                var newPath = _workingFolder + "\\" + storageType.ToString() + "\\" + id + "\\";
                var newFileFullPath = newPath + newFileName;
                try
                {
                    Directory.CreateDirectory(newPath);
                    foreach (string file in Directory.GetFiles(newPath, "*" + newFileName + "*"))
                    {
                        File.Delete(file);
                    }
                    File.Copy(tempFilePath, newFileFullPath);
                    return newFileName;
                }
                catch (Exception)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public FileStream GetFileStream(string fileName, FileStorageTypes storageTypes, int id)
        {
            var fullFilePath = _workingFolder + "\\" + storageTypes.ToString() + "\\" + id + "\\" + fileName;
            if (File.Exists(fullFilePath))
            {
                return new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
            }
            return null;
        }

        public byte[] GetFileBytes(string fileName, FileStorageTypes storageTypes, int id)
        {
            var fullFilePath = _workingFolder + "\\" + storageTypes.ToString() + "\\" + id + "\\" + fileName;
            if (File.Exists(fullFilePath))
            {
                return File.ReadAllBytes(fullFilePath);
            }
            return null;
        }

        public string Move(string fileName, string path, string newFileName)
        {
            if (File.Exists(_workingFolder + "\\" + "temp" + "\\" + fileName))
            {
                string globalPath = HttpContext.Current.Server.MapPath(path);
                try
                {
                    Directory.CreateDirectory(globalPath);
                    File.Move(_workingFolder + "\\" + fileName, globalPath + "\\" + newFileName + Path.GetExtension(fileName));
                    return path + newFileName + Path.GetExtension(fileName);
                }
                catch (Exception)
                {
                    return "";
                }
            }

            return "";
        }

        public string Copy(string fileName, string path)
        {
            if (File.Exists(_workingFolder + "\\" + fileName))
            {
                string globalPath = HttpContext.Current.Server.MapPath(path);
                try
                {
                    Directory.CreateDirectory(globalPath);
                    File.Copy(_workingFolder + "\\" + fileName, globalPath + Path.GetExtension(fileName), true);
                    return path + Path.GetExtension(fileName);
                }
                catch (Exception)
                {
                    return "";
                }
            }
            return "";
        }


        public void Delete(FileStorageTypes storageType, int id)
        {
            var path = _workingFolder + "\\" + storageType.ToString() + "\\" + id + "\\";
            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
            }

        }
        //public bool Resize

        public bool FileExistInStorage(FileStorageTypes storageType, string fileName, int id)
        {
            var path = _workingFolder + "\\" + storageType.ToString() + "\\" + id + "\\" + fileName;
            return File.Exists(path);
        }

        public bool FileExists(string fileName)
        {
            var file = _workingFolder + "\\" + "temp" + "\\" + fileName;

            return File.Exists(file);
        }
        private void CheckTargetDirectory()
        {
            if (!Directory.Exists(_workingFolder))
            {
                throw new ArithmeticException("Directory" + _workingFolder + "not exist!");
            }
        }
    }
}