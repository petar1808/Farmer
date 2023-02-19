using FluentValidation;

namespace Application.Features.Identity.Commands.CreatePassword
{
    public class CreateUserPasswordCommandValidator : AbstractValidator<CreateUserPasswordCommand>
    {
        public CreateUserPasswordCommandValidator()
        {
            this.RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Имейлът е задължителен")
                .EmailAddress()
                .WithMessage("Имейлът е невалиден");
        }
    }
}
