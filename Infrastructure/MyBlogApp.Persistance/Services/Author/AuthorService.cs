using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBlogApp.Application.Abstractions.Caching;
using MyBlogApp.Application.Abstractions.Services;
using MyBlogApp.Application.Abstractions.UnitOfWork;
using MyBlogApp.Application.Dto.Author;

namespace MyBlogApp.Persistance.Services.Author
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IUnitOfWork unitOfWork, ICacheService cacheService, ILogger<AuthorService> logger)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(CreateAuthorDto dto)
        {
            _logger.LogInformation($"START {nameof(AuthorService)} - {nameof(CreateAsync)}");

            var isAdded = await _unitOfWork.AuthorRepository.AddAsync(new Domain.Entities.Author
            {
                Name = dto.Name,
                ImageURL = dto.ImageURL,
            });

            if (isAdded)
            {
                _logger.LogInformation($"Author created successfully: {dto.Name}");
                return await _unitOfWork.SaveAsync();
            }

            _logger.LogWarning($"Author created failed: {dto.Name}");
            return false;
        }

        public async Task<List<ListAuthorDto>> GetAllAsync()
        {
            bool isExists = _cacheService.TryGetValue("Authors", out List<ListAuthorDto> authorsInCache);
            if (isExists)
            {
                return authorsInCache;
            }

            var authorsInDb = await _unitOfWork.AuthorRepository.GetAll(tracking: false).Select(a => new ListAuthorDto
            {
                Name = a.Name,
                ImageUrl = a.ImageURL
            }).ToListAsync();

            _cacheService.Add("Authors", authorsInDb);

            return authorsInDb;
        }
    }
}
