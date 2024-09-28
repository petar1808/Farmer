using Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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

            this.RuleFor(x => x.SeasonId)
                .Must(id=>
                {
                    var seeding = farmerDbContext
                        .WorkingSeasons
                        .Any(x => x.Id == id);

                    return seeding;
                })
                .WithMessage(x => $"Сеитба с Ид: {x.SeasonId} не съществува!");

            this.RuleFor(x => x.SelectedArableLands)
                .Must(x => x.Any())
                .WithMessage("Трябва да изберете поне една земя");
        }
    }
}
