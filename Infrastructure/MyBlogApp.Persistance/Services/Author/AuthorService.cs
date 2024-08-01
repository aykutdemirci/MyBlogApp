using Microsoft.EntityFrameworkCore;
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

        public AuthorService(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<bool> CreateAsync(CreateAuthorDto dto)
        {
            var isAdded = await _unitOfWork.AuthorRepository.AddAsync(new Domain.Entities.Author
            {
                Name = dto.Name,
                ImageURL = dto.ImageURL,
            });

            if (isAdded) return await _unitOfWork.SaveAsync();

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
            }).ToListAsync();

            _cacheService.Add("Authors", authorsInDb);

            return authorsInDb;
        }
    }
}
