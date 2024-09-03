using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;

namespace Application.Features.Subsidies.Commands.Create
{
    public class CreateSubsidyCommand : CommonSubsidyInputCommandModel, IRequest<Result>
    {
        public int SeedingId { get; private set; }

        public void SetSeedingId(int seedingId)
        {
            SeedingId = seedingId;
        }

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
                var subsidy = new Subsidy(request.SeedingId,
                    request.Income,
                    request.Date);

                await farmerDbContext.AddAsync(subsidy, cancellationToken);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
