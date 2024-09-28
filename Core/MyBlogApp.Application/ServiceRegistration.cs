using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MyBlogApp.Application.Validators;
using MyBlogApp.Application.ViewModels;

namespace MyBlogApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<CreateAuthorViewModel>, AuthorCreateValidator>();
        }
    }
}
