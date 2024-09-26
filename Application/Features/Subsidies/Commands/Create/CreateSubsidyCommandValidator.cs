using Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Subsidies.Commands.Create
{
    public class CreateSubsidyCommandValidator : AbstractValidator<CreateSubsidyInputModel>
    {
        public CreateSubsidyCommandValidator(IFarmerDbContext farmerDbContext)
        {
            this.RuleFor(x => x.Income)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Прихода трябва да е положително число");

            this.RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Датата е задължителна");

            this.RuleFor(x => x.SeasonId)
                .MustAsync(async (id, token) =>
                {
                    var seeding = await farmerDbContext
                        .WorkingSeasons
                        .AnyAsync(x => x.Id == id, token);

                    return seeding;
                })
                .WithMessage(x => $"Сеитба с Ид: {x.SeasonId} не съществува!");

            this.RuleFor(x => x.ArableLandIds)
                .NotEmpty()
                .WithMessage("Трябва да изберете поне една земя");
        }
    }
}
