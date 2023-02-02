using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Seedings.Commands.Create
{
    public class CreateSeedingCommand : CreateSeedingInputCommandModel, IRequest<Result>
    {
        public class CreateSeedingCommandHandler : IRequestHandler<CreateSeedingCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public CreateSeedingCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                CreateSeedingCommand request,
                CancellationToken cancellationToken)
            {
                var arableLand = await farmerDbContext
                .ArableLands
                .AnyAsync(x => x.Id == request.ArableLandId, cancellationToken);

                if (!arableLand)
                {
                    return $"Земя с Ид: {request.ArableLandId} не съществува!";
                }

                var workingSeason = await farmerDbContext
                    .WorkingSeasons
                    .AnyAsync(x => x.Id == request.WorkingSeasonId, cancellationToken);

                if (!workingSeason)
                {
                    return $"Сезон с Ид: {request.WorkingSeasonId} не съществува!";
                }

                var seeding = new Seeding(request.ArableLandId, request.WorkingSeasonId);

                await farmerDbContext.AddAsync(seeding, cancellationToken);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
