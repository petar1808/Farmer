using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PerformedWorks.Commands.Delete
{
    public class DeletePerformedWorkCommand : IRequest<Result>
    {
        public int PerformedWorkId { get; set; }

        public class DeletePerformedWorkCommandHandler : IRequestHandler<DeletePerformedWorkCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public DeletePerformedWorkCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                DeletePerformedWorkCommand request,
                CancellationToken cancellationToken)
            {
                var performedWork = await farmerDbContext
                .PerformedWorks
                .FirstOrDefaultAsync(x => x.Id == request.PerformedWorkId, cancellationToken);

                if (performedWork == null)
                {
                    return $"Обработка с Ид: {request.PerformedWorkId} не съществува!";
                }

                farmerDbContext.Remove(performedWork);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
