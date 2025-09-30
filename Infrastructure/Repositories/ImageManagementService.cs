using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Infrastructure.Repositories
{
    public class ImageManagementService(IFileProvider fileProvider, IWebHostEnvironment webHostEnvironment) : IImageManagementService
    {
        private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
       
        readonly string ImageFolder = "Uploads/Images";

        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var SaveImageSrc = new List<string>();

            var contentPath = webHostEnvironment.WebRootPath;
            string path = Path.Combine(contentPath, ImageFolder, src);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            foreach (var item in files)
            {
                // check the allowed extensions
                var ext = Path.GetExtension(item.FileName);
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                if (!allowedExtensions.Contains(ext.ToLower()))
                {
                    string msg= string.Format("only {0} extensions are allowed", string.Join(",", allowedExtensions)) ; // Skip files with disallowed extensions
                    return new Tuple<List<string>, string>(null, msg).Item1;
                }


                if (item.Length > 0)
                {
                    var uniqueFileName = $"{Guid.NewGuid()}{ext}";
                    var imageName = uniqueFileName;
                    var imageSrc = $"{ImageFolder}/{src}/{imageName}";
                    var root = Path.Combine(path, imageName);
                    using (FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    SaveImageSrc.Add(imageSrc);
                }
            }
            return SaveImageSrc;
        }
        public void DeleteImageAsync(string src)
        {
            var info = fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            System.IO.File.Delete(root);
        }

       
    }
}
