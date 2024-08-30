using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBlogApp.Application.Abstractions.Services;
using MyBlogApp.Application.Abstractions.UnitOfWork;
using MyBlogApp.Application.Repositories.Author;
using MyBlogApp.Application.Repositories.Blog;
using MyBlogApp.Application.Repositories.Post;
using MyBlogApp.Persistance.Contexts;
using MyBlogApp.Persistance.Repositories.Author;
using MyBlogApp.Persistance.Repositories.Blog;
using MyBlogApp.Persistance.Repositories.Post;
using MyBlogApp.Persistance.Services.Author;

namespace MyBlogApp.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services, string environmentName)
        {
            services.AddDbContext<MyBlogAppDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString(environmentName)));

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthorService, AuthorService>();
        }
    }
}
