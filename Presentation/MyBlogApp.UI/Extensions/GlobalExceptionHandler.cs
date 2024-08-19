using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Net;

namespace MyBlogApp.UI.Extensions
{
    public static class GlobalExceptionHandler
    {
        public static void AddExceptionHandler<T>(this WebApplication webApplication, ILogger<T> logger)
        {
            webApplication.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    //context.Response.ContentType = MediaTypeNames.Application.Json;

                    var feature = context.Features.Get<IExceptionHandlerFeature>();

                    if (feature != null)
                    {
                        logger.LogError(message: feature.Error.Message);

                        //await context.Response.WriteAsJsonAsync(new
                        //{
                        //    Title = "Hata alındı",
                        //    Message = feature.Error.Message,
                        //    StatusCode = context.Response.StatusCode,
                        //});
                    }
                });
            });
        }
    }
}
