using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Subsidies.Commands.Delete
{
    public class DeleteSubsidyCommand : IRequest<Result>
    {
        public int SubsidyId { get; set; }
        public class DeleteSubsidyCommandHandler : IRequestHandler<DeleteSubsidyCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public DeleteSubsidyCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                DeleteSubsidyCommand request,
                CancellationToken cancellationToken)
            {
                var subsidy = await farmerDbContext
                .Subsidies
                .FirstOrDefaultAsync(x => x.Id == request.SubsidyId, cancellationToken);

                if (subsidy == null)
                {
                    return $"Субсидия с Ид: {request.SubsidyId} не съществува";
                }

                farmerDbContext.Remove(subsidy);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
