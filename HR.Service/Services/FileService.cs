using HR.Service.Services.Contract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HR.Service.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> UploadFileAsync(
            IFormFile file,
            string folderName)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Invalid file.");

            var folderPath = Path.Combine(
                environment.WebRootPath,
                "Uploads",
                folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var extension = Path.GetExtension(file.FileName);

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var fullPath = Path.Combine(
                folderPath,
                fileName);

            await using var stream =
                new FileStream(
                    fullPath,
                    FileMode.Create);

            await file.CopyToAsync(stream);

            return Path.Combine(
                "Uploads",
                folderName,
                fileName)
                .Replace("\\", "/");
        }

        public Task DeleteFileAsync(string filePath)
        {
            var fullPath = Path.Combine(
                environment.WebRootPath,
                filePath);

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }
    }
}