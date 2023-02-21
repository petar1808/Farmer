using FluentValidation;

namespace Application.Features.Identity.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            this.RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Имейлът е невалиден");
        }
    }
}
