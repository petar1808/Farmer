using FluentValidation;

namespace Application.Features.Identity.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            this.RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Имейлът е задължителен")
                .EmailAddress()
                .WithMessage("Имейлът е невалиден");
        }
    }
}
