using MyBlogApp.Application.Repositories.Author;
using MyBlogApp.Persistance.Contexts;

namespace MyBlogApp.Persistance.Repositories.Author
{
    public class AuthorRepository : Repository<MyBlogApp.Domain.Entities.Author>, IAuthorRepository
    {
        public AuthorRepository(MyBlogAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
