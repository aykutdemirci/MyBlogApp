using MyBlogApp.Application.Repositories.Author;
using MyBlogApp.Application.Repositories.Blog;
using MyBlogApp.Application.Repositories.Post;
using MyBlogApp.Application.Repositories.User;

namespace MyBlogApp.Application.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }

        IBlogRepository BlogRepository { get; }

        IAuthorRepository AuthorRepository { get; }

        IUserRepository UserRepository { get; }

        Task<bool> SaveAsync();
    }
}
