using Microsoft.AspNetCore.Http;

namespace HR.Service.Services.Contract
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(
            IFormFile file,
            string folderName);

        Task DeleteFileAsync(string filePath);
    }
}