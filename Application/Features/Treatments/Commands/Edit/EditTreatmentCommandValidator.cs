using FluentValidation;

namespace Application.Features.Treatments.Commands.Edit
{
    public class EditTreatmentCommandValidator : AbstractValidator<EditTreatmentCommand>
    {
        public EditTreatmentCommandValidator()
        {
            this.RuleFor(x => x.ArticleId)
               .NotEmpty()
               .WithMessage("Артикулът е задължителен");

            this.RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Датата е задължителна");

            this.RuleFor(x => x.TreatmentType)
                .IsInEnum()
                .WithMessage("Типът третиране е невалиден");

            this.RuleFor(x => x.AmountOfFuel)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Количеството гориво трябва да е положително число");

            this.RuleFor(x => x.FuelPrice)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Цената на горивото трябва да е положително число");

            this.RuleFor(x => x.ArticleQuantity)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Количеството на артикула трябва да е положително число");

            this.RuleFor(x => x.ArticlePrice)
               .ExclusiveBetween(0m, int.MaxValue)
               .WithMessage("Цената на артикула трябва да е положително число");
        }
    }
}
