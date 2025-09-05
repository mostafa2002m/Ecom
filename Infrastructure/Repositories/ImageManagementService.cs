using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Infrastructure.Repositories
{
    public class ImageManagementService(IFileProvider fileProvider) : IImageManagementService
    {
       

        public void DeleteImageAsync(string publicId)
        {
            var info = fileProvider.GetFileInfo(publicId);
            var root = info.PhysicalPath;
            File.Delete(root);
        }

        public async Task<List<string>> UploadImageAsync(IFormFileCollection files, string folderName)
        {
            List<string> SaveImageSrc = new List<string>();
            var imageDirectory = Path.Combine("www", "Images",folderName);
            if (!File.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    //get image name
                    var imageName = file.FileName;
                    var imageSrc = Path.Combine(imageDirectory, imageName);
                    using (FileStream stream = new FileStream(imageSrc, FileMode.Create))
                    {
                        await stream.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(imageName);
                }

            }

            return SaveImageSrc;
        }
    }
}
