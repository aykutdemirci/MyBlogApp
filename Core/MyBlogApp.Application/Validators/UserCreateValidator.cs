using FluentValidation;
using MyBlogApp.Application.ViewModels;

namespace MyBlogApp.Application.Validators
{
    public class UserCreateValidator : AbstractValidator<RegisterViewModel>
    {
        public UserCreateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Kullanıcı adı boş geçilemez")
                .MaximumLength(50)
                .MinimumLength(3)
                .WithMessage("Kullanıcı adı en az 3, en fazla 50 karakter olabilir");

            RuleFor(p => p.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("E-Posta adresi boş geçilemez")
                .EmailAddress()
                .WithMessage("E-Posta adresi beklenen formatta değil");

            RuleFor(p => p.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Şifre boş geçilemez")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.")
                .Equal(p => p.ConfirmPassword)
                .WithMessage("Şifreler eşleşmiyor");
        }
    }
}
