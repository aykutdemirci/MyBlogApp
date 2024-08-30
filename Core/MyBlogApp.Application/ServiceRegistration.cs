using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyBlogApp.Application.Validators;
using MyBlogApp.Application.ViewModels;

namespace MyBlogApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateAuthorViewModel>, AuthorCreateValidator>();
        }
    }
}
