using FluentValidation;

namespace Application.Features.Identity.Commands.CreatePassword
{
    public class CreateUserPasswordCommandValidator : AbstractValidator<CreateUserPasswordCommand>
    {
        public CreateUserPasswordCommandValidator()
        {
            this.RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Имейлът е невалиден");
        }
    }
}
