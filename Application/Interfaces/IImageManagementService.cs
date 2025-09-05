using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IImageManagementService
    {
        Task<List<string>> UploadImageAsync(IFormFileCollection files, string folderName);
        void DeleteImageAsync(string publicId);
    }
}
