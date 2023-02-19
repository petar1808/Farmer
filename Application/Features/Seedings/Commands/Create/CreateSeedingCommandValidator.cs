using Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Application.Features.Seedings.Commands.Create
{
    public class CreateSeedingCommandValidator : AbstractValidator<CreateSeedingCommand>
    {
        public CreateSeedingCommandValidator(IFarmerDbContext farmerDbContext)
        {
            this.RuleFor(x => x.ArableLandId).NotEmpty().WithMessage("Земята е задължителна");

            this.RuleFor(x => x.WorkingSeasonId).NotEmpty().WithMessage("Сезона е задължителен");

            this.RuleFor(x => x.ArableLandId)
               .Must((nameValue) =>
               {
                   var arableLand = farmerDbContext
                        .ArableLands
                        .Any(x => x.Id == nameValue);

                   return arableLand;
               })
               .WithMessage(x => $"Земя с Ид: {x.ArableLandId} не съществува!");

            this.RuleFor(x => x.WorkingSeasonId)
               .Must((nameValue) =>
               {
                   var workingSeason = farmerDbContext
                        .WorkingSeasons
                        .Any(x => x.Id == nameValue);

                   return workingSeason;
               })
               .WithMessage(x => $"Сезон с Ид: {x.WorkingSeasonId} не съществува!");
        }
    }
}
