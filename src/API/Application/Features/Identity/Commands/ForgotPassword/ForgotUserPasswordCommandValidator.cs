using FluentValidation;

namespace Application.Features.Identity.Commands.ForgotPassword
{
    public class ForgotUserPasswordCommandValidator : AbstractValidator<ForgotUserPasswordCommand>
    {
        public ForgotUserPasswordCommandValidator()
        {
            this.RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Имейлът е невалиден");
        }
    }
}
