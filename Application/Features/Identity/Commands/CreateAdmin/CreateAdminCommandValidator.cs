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
