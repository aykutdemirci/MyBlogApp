using MyBlogApp.Application.Repositories.Author;
using MyBlogApp.Application.Repositories.Blog;
using MyBlogApp.Application.Repositories.Post;

namespace MyBlogApp.Application.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }

        IBlogRepository BlogRepository { get; }

        IAuthorRepository AuthorRepository { get; }

        Task<bool> SaveAsync();
    }
}
