using Microsoft.AspNetCore.Hosting;
using MyBlogApp.Application.Abstractions.Storage;
using MyBlogApp.Application.Dto;

namespace MyBlogApp.Infrastructure.Services.Storage
{
    public class LocalStorageService : IStorageService
    {
        private readonly string _webRootPath;

        public string WebRootPath { get { return _webRootPath; } }

        public LocalStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webRootPath = webHostEnvironment.WebRootPath;
        }

        public Task DeleteAsync(string path, string fileName)
        {
            var deletePath = Path.Combine(WebRootPath, path);

            return Task.Run(() => { File.Delete($"{deletePath}\\{fileName}"); });
        }

        public List<string> GetFiles(string path)
        {
            var filesPath = Path.Combine(WebRootPath, path);

            return Directory.GetFiles(filesPath).ToList();
        }

        public bool HasFile(string path, string fileName)
        {
            var searchPath = Path.Combine(WebRootPath, path);

            return File.Exists($"{searchPath}\\{fileName}");
        }

        public async Task<List<string>> UploadAsync(string path, List<FileUploadDto> files)
        {
            var uploadPath = Path.Combine(WebRootPath, path);

            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            List<string> uploadedFiles = new();

            foreach (var file in files)
            {
                var filePath = $"{uploadPath}\\{file.FileName}";

                using var sourceFile = new MemoryStream(file.Content);
                using var destinationFile = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                await sourceFile.CopyToAsync(destinationFile);

                string relativePath = path.Replace("\\", "/") + "/" + file.FileName;

                uploadedFiles.Add(relativePath);
            }

            return uploadedFiles;
        }
    }
}
