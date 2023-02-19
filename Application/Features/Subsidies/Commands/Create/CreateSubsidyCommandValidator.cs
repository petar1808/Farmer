using Application.Services;
using FluentValidation;

namespace Application.Features.Subsidies.Commands.Create
{
    public class CreateSubsidyCommandValidator : AbstractValidator<CreateSubsidyCommand>
    {
        public CreateSubsidyCommandValidator(IFarmerDbContext farmerDbContext)
        {
            this.RuleFor(x => x.Income)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Прихода трябва да е положително число");

            this.RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Датата е задължителна");

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
