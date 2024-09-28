using MyBlogApp.Application.Dto.User;

namespace MyBlogApp.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<bool> CreateAsync(CreateUserDto dto);

        public bool IsUserExists(string email, string password);
    }
}
