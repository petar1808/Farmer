using Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Application.Features.PerformedWorks.Commands.Create
{
    public class CreatePerformedWorkCommandValidator : AbstractValidator<CreatePerformedWorkCommand>
    {
        public CreatePerformedWorkCommandValidator(IFarmerDbContext farmerDbContext)
        {
            this.RuleFor(x => x.WorkType)
                .IsInEnum()
                .WithMessage("Типът работа е невалиден");

            this.RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Датата е задължителна");

            this.RuleFor(x => x.AmountOfFuel)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Количеството гориво трябва да е положително число");

            this.RuleFor(x => x.FuelPrice)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Цената на горивото трябва да е положително число");

            this.RuleFor(x => x.SeedingId)
                .Must((nameValue) =>
                {
                    var seeding = farmerDbContext
                        .Seedings
                        .Any(x => x.Id == nameValue);

                    return seeding;
                })
                .WithMessage(x => $"Сеитба с Ид: {x.SeedingId} не съществува!");
        }
    }
}
