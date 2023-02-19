using FluentValidation;

namespace Application.Features.Identity.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            this.RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Имейлът е задължителен")
               .EmailAddress()
               .WithMessage("Имейлът е невалиден");
        }
    }
}
