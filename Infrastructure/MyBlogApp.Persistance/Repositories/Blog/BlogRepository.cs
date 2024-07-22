using MyBlogApp.Application.Repositories.Blog;
using MyBlogApp.Persistance.Contexts;

namespace MyBlogApp.Persistance.Repositories.Blog
{
    public class BlogRepository : Repository<MyBlogApp.Domain.Entities.Blog>, IBlogRepository
    {
        public BlogRepository(MyBlogAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
