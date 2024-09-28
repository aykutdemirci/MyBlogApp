using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using MyBlogApp.Application.Abstractions.Storage;
using MyBlogApp.Application.Dto;
using MyBlogApp.Infrastructure.Configurations;

namespace MyBlogApp.Infrastructure.Services.Storage
{
    public class AzureStorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureStorageService(IWebHostEnvironment webHostEnvironment)
        {
            var connectionString = FileUploadConfig.GetAzureConnectionString(webHostEnvironment.EnvironmentName);
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public Task DeleteAsync(string containerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string containerName)
        {
            throw new NotImplementedException();
        }

        public bool HasFile(string containerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> UploadAsync(string containerName, List<FileUploadDto> files)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            await blobContainerClient.CreateIfNotExistsAsync();
            await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<string> uploadedFiles = new();

            foreach (var file in files)
            {
                var blobClient = blobContainerClient.GetBlobClient(file.FileName);

                using var stream = new MemoryStream(file.Content);

                await blobClient.UploadAsync(stream);

                uploadedFiles.Add(blobClient.Uri.ToString());
            }

            return uploadedFiles;
        }
    }
}
