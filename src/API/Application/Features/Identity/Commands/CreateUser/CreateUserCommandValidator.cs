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

            this.RuleFor(x => x.FirstName)
                .Length(2, 50)
                .WithMessage("Името трябва да е между 2 и 50 символа");

            this.RuleFor(x => x.LastName)
                .Length(2, 50)
                .WithMessage("Фамилията трябва да е между 2 и 50 символа");
        }
    }
}
