using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Subsidies.Commands.Edit
{
    public class EditSubsidyCommand : CommonSubsidyInputCommandModel, IRequest<Result>
    {
        public int Id { get; set; }

        public class EditSubsidyCommandHandler : IRequestHandler<EditSubsidyCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public EditSubsidyCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                EditSubsidyCommand request,
                CancellationToken cancellationToken)
            {
                var subsidy = await farmerDbContext
                    .Subsidies
                    .Include(x => x.SubsidyByArableLands)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (subsidy == null)
                {
                    return $"Субсидия с Ид: {request.Id} не съществува";
                }

                subsidy
                    .UpdateIncome(request.Income)
                    .UpdateDate(request.Date)
                    .UpdateComment(request.Comment);

                var arableLands = await farmerDbContext.ArableLands
                    .Where(x => request.SelectedArableLands.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                // Remove
                var forRemove = subsidy.SubsidyByArableLands
                    .Where(x => !request.SelectedArableLands.Contains(x.ArableLandId));

                foreach (var item in forRemove)
                {
                    farmerDbContext.Remove(item);
                }

                var totalArea = arableLands.Sum(x => x.SizeInDecar);

                foreach (var arableLand in arableLands)
                {
                    var arableLandIncome = ((decimal)arableLand.SizeInDecar / (decimal)totalArea) * request.Income;

                    var arableLandSubsidy = subsidy.SubsidyByArableLands
                        .Find(x => x.ArableLandId == arableLand.Id);

                    if (arableLandSubsidy == null)
                    {
                        subsidy.SubsidyByArableLands
                            .Add(new SubsidyByArableLand(arableLand.Id, arableLandIncome));
                    }
                    else
                    {
                        arableLandSubsidy.UpdateIncome(arableLandIncome);
                    }
                }


                farmerDbContext.Update(subsidy);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}