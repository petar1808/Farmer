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
                var subsidyByArableLands = await CalculateSubsidyByArableLand(
                    request.SelectedArableLands, 
                    request.Income,
                    cancellationToken);

                var subsidy = new Subsidy(request.Income, request.SeasonId, request.Date, subsidyByArableLands, request.Comment);

                await farmerDbContext.AddAsync(subsidy, cancellationToken);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }

            private async Task<List<SubsidyByArableLand>> CalculateSubsidyByArableLand(
                IEnumerable<int> selectedArableLands,
                decimal income,
                CancellationToken cancellationToken)
            {
                var arableLands = await farmerDbContext.ArableLands
                    .Where(x => selectedArableLands.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                var totalArea = arableLands.Sum(x => x.SizeInDecar);

                return arableLands.Select(arableLand =>
                    new SubsidyByArableLand(arableLand.Id, ((decimal)arableLand.SizeInDecar / (decimal)totalArea) * income))
                    .ToList();
            }
        }
    }
}
