using FluentValidation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Application.Features.Identity.Commands.CreateAdmin
{
    public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommand>
    {
        public CreateAdminCommandValidator()
        {
            this.RuleFor(x => x.UserEmail)
                .NotEmpty()
                .WithMessage("Имейлът е задължителен")
                .EmailAddress()
                .WithMessage("Имейлът е невалиден");
        }
    }
}
