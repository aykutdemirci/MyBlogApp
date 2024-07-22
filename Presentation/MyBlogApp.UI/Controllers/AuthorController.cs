using Microsoft.AspNetCore.Mvc;

namespace MyBlogApp.UI.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
