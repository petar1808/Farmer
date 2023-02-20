using FluentValidation;

namespace Application.Features.Identity.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            this.RuleFor(x => x.UserEmail)
                .EmailAddress()
                .WithMessage("Имейлът е невалиден");
        }
    }
}
