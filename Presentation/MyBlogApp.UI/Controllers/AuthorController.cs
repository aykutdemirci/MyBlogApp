using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Application.Abstractions.Services;
using MyBlogApp.Application.Abstractions.Storage;
using MyBlogApp.Application.Dto;
using MyBlogApp.Application.Dto.Author;
using MyBlogApp.Application.ViewModels;
using MyBlogApp.Infrastructure.Configurations;
using MyBlogApp.Infrastructure.Helpers;
using MyBlogApp.UI.Extensions;

namespace MyBlogApp.UI.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthorController(IAuthorService authorService, IStorageService storageService, IWebHostEnvironment webHostEnvironment)
        {
            _authorService = authorService;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var data = Task.Run(async () => await _authorService.GetAllAsync()).GetAwaiter().GetResult();

            List<ListAuthorViewModel> authors = data.Select(q => new ListAuthorViewModel
            {
                Name = q.Name,
                ImageURL = q.ImageUrl
            }).ToList();

            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAuthorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var values = ModelState.Values;

                return BadRequest();
            }

            var dto = new CreateAuthorDto { Name = vm.Name };

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                var fileExtension = Path.GetExtension(formFile.FileName);
                var fileNewName = FileRenameHelper.RenameFile(fileName) + $"_{DateTime.Now.ToFileTime()}" + fileExtension;
                var fileContent = Task.Run(async () => await formFile.GetBytesAsync()).GetAwaiter().GetResult();

                var files = new List<FileUploadDto>
                {
                    new()
                    {
                        FileName = fileNewName,
                        Content = fileContent
                    }
                };

                var authorImagesPath = FileUploadConfig.GetAuthorImagesPath(_webHostEnvironment.EnvironmentName, _storageService.GetType());

                Task.Run(async () => await _storageService.UploadAsync(authorImagesPath, files));

                dto.ImageURL = authorImagesPath.Replace("\\", "/") + "/" + fileNewName;
            }

            var result = Task.Run(async () => await _authorService.CreateAsync(dto)).GetAwaiter().GetResult();

            if (!result) return new StatusCodeResult(500);

            return new StatusCodeResult(201);
        }
    }
}
