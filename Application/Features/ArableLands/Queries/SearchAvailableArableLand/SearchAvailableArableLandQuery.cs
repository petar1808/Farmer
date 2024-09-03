using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ArableLands.Queries.SearchAvailableArableLand
{
    public class SearchAvailableArableLandQuery : IRequest<Result<List<SearchAvailableArableLandOutputQueryModel>>>
    {
        public int SeasonId { get; set; }

        public class SearchAvailableArableLandQueryHandler : IRequestHandler<SearchAvailableArableLandQuery, Result<List<SearchAvailableArableLandOutputQueryModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public SearchAvailableArableLandQueryHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result<List<SearchAvailableArableLandOutputQueryModel>>> Handle(
                SearchAvailableArableLandQuery request,
                CancellationToken cancellationToken)
            {
                var workingseason = await this.farmerDbContext
                        .WorkingSeasons
                        .AnyAsync(x => x.Id == request.SeasonId, cancellationToken);

                if (!workingseason)
                {
                    return $"Сезон с Ид: {request.SeasonId} не съществува!";
                }

                var arableLands = await this.farmerDbContext
                        .ArableLands
                        .Select(x => new SearchAvailableArableLandOutputQueryModel(x.Id, x.Name))
                        .ToListAsync(cancellationToken);

                var existingArableLands = await this.farmerDbContext
                        .Seedings
                        .Where(x => x.WorkingSeasonId == request.SeasonId)
                        .Select(x => x.ArableLandId)
                        .ToListAsync(cancellationToken);

                arableLands = arableLands
                        .Where(x => !existingArableLands.Contains(x.Value))
                        .ToList();

                return arableLands;
            }
        }
    }
}
