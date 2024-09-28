using FluentValidation;
using MyBlogApp.Application.ViewModels;

namespace MyBlogApp.Application.Validators
{
    public class UserLoginValidator : AbstractValidator<LoginViewModel>
    {
        public UserLoginValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("E-Posta adresi boş geçilemez");

            RuleFor(p => p.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Şifre boş geçilemez");
        }
    }
}
