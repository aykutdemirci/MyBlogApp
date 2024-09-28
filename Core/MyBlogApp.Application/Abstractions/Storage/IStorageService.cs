using MyBlogApp.Application.Dto;

namespace MyBlogApp.Application.Abstractions.Storage
{
    public interface IStorageService
    {
        Task<List<string>> UploadAsync(string pathOrContainerName, List<FileUploadDto> files);
        Task DeleteAsync(string pathOrContainerName, string fileName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFile(string pathOrContainerName, string fileName);
    }
}
