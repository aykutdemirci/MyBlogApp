using Microsoft.EntityFrameworkCore;
using MyBlogApp.Application.Abstractions.Services;
using MyBlogApp.Application.Abstractions.UnitOfWork;
using MyBlogApp.Application.Dto.Author;

namespace MyBlogApp.Persistance.Services.Author
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateAsync(CreateAuthorDto dto)
        {
            var isAdded = await _unitOfWork.AuthorRepository.AddAsync(new Domain.Entities.Author
            {
                Name = dto.Name,
                ImageURL = dto.ImageURL,
            });

            if(isAdded) return await _unitOfWork.SaveAsync();

            return false;
        }

        public async Task<List<ListAuthorDto>> GetAllAsync()
        {
            return await _unitOfWork.AuthorRepository.GetAll(tracking: false).Select(a => new ListAuthorDto
            {
                Name = a.Name,
            }).ToListAsync();
        }
    }
}
