using FluentValidation;

namespace Application.Features.WorkingSeasons.Commands.Edit
{
    public class EditWorkingSeasonCommandValidator : AbstractValidator<EditWorkingSeasonCommand>
    {
        public EditWorkingSeasonCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Името е задължително");

            this.RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Началната дата е задължителна");

            this.RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("Крайната дата е задължителна");
        }
    }
}
