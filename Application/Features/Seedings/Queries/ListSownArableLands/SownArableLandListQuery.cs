using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Seedings.Queries.ListSownArableLands
{
    public class SownArableLandListQuery : IRequest<Result<List<SownArableLandListQueryOutputModel>>>
    {
        public int SeasonId { get; set; }

        public class SownArableLandListQueryHandler : IRequestHandler<SownArableLandListQuery, Result<List<SownArableLandListQueryOutputModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public SownArableLandListQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<SownArableLandListQueryOutputModel>>> Handle(
                SownArableLandListQuery request,
                CancellationToken cancellationToken)
            {
                var workingSeason = await farmerDbContext
                .WorkingSeasons
                .AnyAsync(x => x.Id == request.SeasonId);

                if (!workingSeason)
                {
                    return $"Сезон с Ид: {request.SeasonId} не съществува!";
                }

                var arableLands = farmerDbContext
                    .Seedings
                    .Include(x => x.ArableLand)
                    .Where(x => x.WorkingSeasonId == request.SeasonId)
                    .AsQueryable();

                var result = await mapper
                    .ProjectTo<SownArableLandListQueryOutputModel>(arableLands)
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
