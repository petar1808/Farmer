using Application.Services;
using FluentValidation;

namespace Application.Features.Treatments.Commands.Create
{
    public class CreateTreatmentCommandValidator : AbstractValidator<CreateTreatmentCommand>
    {
        public CreateTreatmentCommandValidator(IFarmerDbContext farmerDbContext)
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

            this.RuleFor(x => x.SeedingId)
                .Must((nameValue) =>
                {
                    var seeding = farmerDbContext
                        .Seedings
                        .Any(x => x.Id == nameValue);

                    return seeding;
                })
                .WithMessage(x => $"Сеитба с Ид: {x.SeedingId} не съществува!");

            this.RuleFor(x => x.ArticleId)
                .Must((nameValue) =>
                {
                    var article = farmerDbContext
                        .Articles
                        .Any(x => x.Id == nameValue);

                    return article;
                })
                .WithMessage(x => $"Артикул с Ид: {x.ArticleId} не съществува!");
        }
    }
}
