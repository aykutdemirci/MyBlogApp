using MyBlogApp.Application.Abstractions.UnitOfWork;
using MyBlogApp.Application.Repositories.Author;
using MyBlogApp.Application.Repositories.Blog;
using MyBlogApp.Application.Repositories.Post;
using MyBlogApp.Application.Repositories.User;
using MyBlogApp.Persistance.Contexts;

namespace MyBlogApp.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPostRepository PostRepository { get; }

        public IBlogRepository BlogRepository { get; }

        public IAuthorRepository AuthorRepository { get; }

        public IUserRepository UserRepository { get; }

        private readonly MyBlogAppDbContext _dbContext;

        public UnitOfWork(IPostRepository postRepository,
                          IBlogRepository blogRepository,
                          IAuthorRepository authorRepository,
                          IUserRepository userRepository,
                          MyBlogAppDbContext dbContext)
        {
            PostRepository = postRepository;
            BlogRepository = blogRepository;
            AuthorRepository = authorRepository;
            UserRepository = userRepository;
            _dbContext = dbContext;
        }

        public async Task<bool> SaveAsync()
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var affectedRows = await _dbContext.SaveChangesAsync();

                transaction.Commit();

                return affectedRows > 0;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
