using Microsoft.Extensions.Logging;
using MyBlogApp.Application.Abstractions.Services;
using MyBlogApp.Application.Abstractions.UnitOfWork;
using MyBlogApp.Application.Dto.User;

namespace MyBlogApp.Persistance.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(CreateUserDto dto)
        {
            _logger.LogInformation($"START {nameof(UserService)} - {nameof(CreateAsync)}");

            var isAdded = await _unitOfWork.UserRepository.AddAsync(new Domain.Entities.User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
            });

            if (isAdded)
            {
                _logger.LogInformation($"User created successfully: {dto.Email}");
                return await _unitOfWork.SaveAsync();
            }

            _logger.LogWarning($"User created failed: {dto.Email}");
            return false;
        }

        public bool IsUserExists(string email, string password)
        {
            _logger.LogInformation($"START {nameof(UserService)} - {nameof(IsUserExists)}");

            var isExists = _unitOfWork.UserRepository.GetWhere(q => q.Email == email && q.Password == password).Any();

            if (isExists)
            {
                _logger.LogInformation($"User found successfully: {email}");
            }
            else
            {
                _logger.LogWarning($"User not found: {email}");
            }

            return isExists;
        }
    }
}
