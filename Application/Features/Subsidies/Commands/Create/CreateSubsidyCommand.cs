using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Subsidies.Commands.Create
{
    public class CreateSubsidyCommand : CreateSubsidyInputModel, IRequest<Result>
    {
        public class CreateSubsidyCommandHandler : IRequestHandler<CreateSubsidyCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public CreateSubsidyCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                CreateSubsidyCommand request,
                CancellationToken cancellationToken)
            {
                var seedings = await farmerDbContext.Seedings
                    .Where(x => x.WorkingSeasonId == request.SeasonId && request.ArableLandIds.Contains(x.ArableLandId))
                    .Select(x => new
                    {
                        SeedingId = x.Id,
                        x.ArableLand.SizeInDecar,
                    })
                    .ToListAsync(cancellationToken);

                var totalArea = seedings.Sum(x => x.SizeInDecar);

                foreach (var seeding in seedings)
                {
                    var arableLandIncome = (seeding.SizeInDecar / totalArea) * request.Income;

                    var subsidy = new Subsidy(seeding.SeedingId,
                        arableLandIncome,
                        request.Date);

                    await farmerDbContext.AddAsync(subsidy, cancellationToken);
                }

                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
