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
            bool added = Task.Run(async () => await _authorService.CreateAsync(new CreateAuthorDto
            {
                Name = "John Doe",
            })).GetAwaiter().GetResult();

            added = Task.Run(async () => await _authorService.CreateAsync(new CreateAuthorDto
            {
                Name = "Mehmet Tığlıoğlu",
            })).GetAwaiter().GetResult();

            added = Task.Run(async () => await _authorService.CreateAsync(new CreateAuthorDto
            {
                Name = "Cevdet Bursalı"
            })).GetAwaiter().GetResult();

            var data = Task.Run(async () => await _authorService.GetAllAsync()).GetAwaiter().GetResult();

            List<ListAuthorViewModel> authors = data.Select(q => new ListAuthorViewModel
            {
                Name = q.Name,
            }).ToList();

            return View(authors);
        }

        //[HttpGet("Index")]
        //public async Task<IActionResult> Index()
        //{
        //    await _authorService.CreateAsync(new CreateAuthorDto
        //    {
        //        Name = "John Doe",
        //    });

        //    await _authorService.CreateAsync(new CreateAuthorDto
        //    {
        //        Name = "Mehmet Tığlıoğlu",
        //    });

        //    await _authorService.CreateAsync(new CreateAuthorDto
        //    {
        //        Name = "Cevdet Bursalı"
        //    });

        //    var data = await _authorService.GetAllAsync();

        //    List<ListAuthorViewModel> authors = data.Select(q => new ListAuthorViewModel
        //    {
        //        Name = q.Name,
        //    }).ToList();

        //    return View(authors);
        //}

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateAuthorDto dto)
        {
            var result = await _authorService.CreateAsync(dto);

            if (result) return new StatusCodeResult(201);

            return new StatusCodeResult(500);
        }
    }
}
