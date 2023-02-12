using FluentValidation;

namespace Application.Features.WorkingSeasons.Commands.Create
{
    public class CreateWorkingSeasonCommandValidator : AbstractValidator<CreateWorkingSeasonCommand>
    {
        public CreateWorkingSeasonCommandValidator()
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
