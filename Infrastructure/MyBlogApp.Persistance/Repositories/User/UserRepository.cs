using MyBlogApp.Application.Repositories.User;
using MyBlogApp.Persistance.Contexts;

namespace MyBlogApp.Persistance.Repositories.User
{
    public class UserRepository : Repository<MyBlogApp.Domain.Entities.User>, IUserRepository
    {
        public UserRepository(MyBlogAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
