using FluentValidation;

namespace Application.Features.Subsidies.Commands.Create
{
    public class CreateSubsidyCommandValidator : AbstractValidator<CreateSubsidyCommand>
    {
        public CreateSubsidyCommandValidator()
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
