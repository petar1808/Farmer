using FluentValidation;

namespace Application.Features.PerformedWorks.Commands.Edit
{
    public class EditPerformedWorkCommandValidator : AbstractValidator<EditPerformedWorkCommand>
    {
        public EditPerformedWorkCommandValidator()
        {
            this.RuleFor(x => x.WorkType)
                .IsInEnum()
                .WithMessage("Типът работа е невалиден");

            this.RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Датата е задължителна");
        }
    }
}
