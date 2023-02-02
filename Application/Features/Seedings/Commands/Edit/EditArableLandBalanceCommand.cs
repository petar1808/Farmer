using Application.Mappings;
using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Seedings.Commands.Edit
{
    public class EditArableLandBalanceCommand : EditArableLandBalanceInputCommandModel, IRequest<Result>
    {
        public class EditArableLandBalanceCommandHandler : IRequestHandler<EditArableLandBalanceCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public EditArableLandBalanceCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                EditArableLandBalanceCommand request,
                CancellationToken cancellationToken)
            {
                var article = await farmerDbContext
                .Articles
                .AnyAsync(x => x.Id == request.ArticleId, cancellationToken);

                if (!article && request.ArticleId != null)
                {
                    return $"Артикул с Ид: {request.ArticleId} не съществува!";
                }

                var seeding = await farmerDbContext
                    .Seedings
                    .FirstOrDefaultAsync(x => x.Id == request.SeedingId, cancellationToken);

                if (seeding == null)
                {
                    return $"Сеитба с Ид: {request.SeedingId} не съществува!";
                }

                seeding.UpdateArticle(request.ArticleId)
                       .UpdateSeedsQuantityPerDecare(request.SeedsQuantityPerDecare)
                       .UpdateSeedsPricePerKilogram(request.SeedsPricePerKilogram)
                       .UpdateHarvestedQuantityPerDecare(request.HarvestedQuantityPerDecare)
                       .UpdateHarvestedGrainSellingPricePerKilogram(request.HarvestedGrainSellingPricePerKilogram)
                       .UpdateExpensesForHarvesting(request.ExpensesForHarvesting);

                farmerDbContext.Update(seeding);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
