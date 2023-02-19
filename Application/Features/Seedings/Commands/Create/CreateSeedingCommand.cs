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
                var seeding = new Seeding(request.ArableLandId, request.WorkingSeasonId);

                await farmerDbContext.AddAsync(seeding, cancellationToken);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
