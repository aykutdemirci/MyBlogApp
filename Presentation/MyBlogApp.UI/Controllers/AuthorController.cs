using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Application.Abstractions.Services;
using MyBlogApp.Application.Dto.Author;
using MyBlogApp.UI.Models;

namespace MyBlogApp.UI.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public IActionResult Index()
        {
            var data = Task.Run(async () => await _authorService.GetAllAsync()).GetAwaiter().GetResult();

            List<ListAuthorViewModel> authors = data.Select(q => new ListAuthorViewModel
            {
                Name = q.Name,
            }).ToList();

            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAuthorVM vm)
        {
            var dto = new CreateAuthorDto
            {
                Name = vm.Name,
                ImageURL = vm.ImageURL,
            };

            var result = Task.Run(async () => await _authorService.CreateAsync(dto)).GetAwaiter().GetResult();

            if (result) return new StatusCodeResult(201);

            return new StatusCodeResult(500);
        }
    }
}
