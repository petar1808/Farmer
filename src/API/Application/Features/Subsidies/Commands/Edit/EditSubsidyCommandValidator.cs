using FluentValidation;

namespace Application.Features.Subsidies.Commands.Edit
{
    public class EditSubsidyCommandValidator : AbstractValidator<EditSubsidyCommand>
    {
        public EditSubsidyCommandValidator()
        {
            this.RuleFor(x => x.Income)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Прихода трябва да е положително число");

            this.RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Датата е задължителна");
        }
    }
}
