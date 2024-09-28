using FluentValidation;
using MyBlogApp.Application.ViewModels;

namespace MyBlogApp.Application.Validators
{
    public class AuthorCreateValidator : AbstractValidator<CreateAuthorViewModel>
    {
        public AuthorCreateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Yazar adı boş geçilemez")
                .MaximumLength(50)
                .MinimumLength(3)
                .WithMessage("Yazar adı en az 3, en fazla 50 karakter olabilir");
        }
    }
}
