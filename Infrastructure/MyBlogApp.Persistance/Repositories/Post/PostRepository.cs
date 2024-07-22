using MyBlogApp.Application.Repositories.Post;
using MyBlogApp.Persistance.Contexts;

namespace MyBlogApp.Persistance.Repositories.Post
{
    public class PostRepository : Repository<MyBlogApp.Domain.Entities.Post>, IPostRepository
    {
        public PostRepository(MyBlogAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
