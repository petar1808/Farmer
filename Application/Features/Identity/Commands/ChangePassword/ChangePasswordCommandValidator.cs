using FluentValidation;

namespace Application.Features.Identity.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            this.RuleFor(x => x.Email)
               .EmailAddress()
               .WithMessage("Имейлът е невалиден");
        }
    }
}
