using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;

namespace Application.Features.PerformedWorks.Commands.Create
{
    public class CreatePerformedWorkCommand : CommonPerformedWorkInputCommandModel, IRequest<Result>
    {
        public int SeedingId { get; private set; }

        public void SetSeedingId(int seedingId)
        {
            SeedingId = seedingId;
        }

        public class CreatePerformedWorkCommandHandler : IRequestHandler<CreatePerformedWorkCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public CreatePerformedWorkCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                CreatePerformedWorkCommand request,
                CancellationToken cancellationToken)
            {
                var performedWork = new PerformedWork(request.SeedingId,
                    request.WorkType,
                    request.Date);

                await farmerDbContext.AddAsync(performedWork, cancellationToken);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
