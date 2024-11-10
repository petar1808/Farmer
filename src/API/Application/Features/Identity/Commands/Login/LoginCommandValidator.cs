using FluentValidation;

namespace Application.Features.Identity.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            this.RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Имейлът е невалиден");

            this.RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Паролата е задължителна");
        }
    }
}
