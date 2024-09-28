using Microsoft.AspNetCore.Http;

namespace MyBlogApp.Application.ViewModels
{
    public class CreateAuthorViewModel
    {
        public string Name { get; set; }

        public IFormFile Image { get; set; }
    }
}
