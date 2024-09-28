using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Application.Abstractions.Services;
using MyBlogApp.Application.ViewModels;
using MyBlogApp.Infrastructure.Extensions;

namespace MyBlogApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAdded = await _userService.CreateAsync(new Application.Dto.User.CreateUserDto
            {
                Name = vm.Name,
                Email = vm.Email,
                Password = vm.Password.ToMD5(),
            });

            if (!isAdded) return new StatusCodeResult(500);

            return new StatusCodeResult(201);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cryptedPassword = vm.Password.ToMD5();

            var isExists = _userService.IsUserExists(vm.Email, cryptedPassword);

            if (!isExists) 
            {
                return NotFound($"User not found by email: {vm.Email}");
            }

            return Ok();
        }
    }
}
