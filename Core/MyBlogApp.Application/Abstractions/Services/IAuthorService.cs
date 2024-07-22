using MyBlogApp.Application.Dto.Author;

namespace MyBlogApp.Application.Abstractions.Services
{
    public interface IAuthorService
    {
        Task<bool> CreateAsync(CreateAuthorDto dto);

        Task<List<ListAuthorDto>> GetAllAsync();
    }
}
