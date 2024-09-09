using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Subsidies.Queries.List
{
    public class SubsidyListQuery : IRequest<Result<List<CommonSubsidyOutputQueryModel>>>
    {
        public int SeedingId { get; set; }

        public class SubsidyListQueryHandler : IRequestHandler<SubsidyListQuery, Result<List<CommonSubsidyOutputQueryModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public SubsidyListQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<CommonSubsidyOutputQueryModel>>> Handle(
                SubsidyListQuery request,
                CancellationToken cancellationToken)
            {
                var seeding = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.Id == request.SeedingId, cancellationToken);

                if (!seeding)
                {
                    return $"Сеитба с Ид: {request.SeedingId} не съществува!";
                }

                var subsidy = farmerDbContext
                    .Subsidies
                    .Where(x => x.SeedingId == request.SeedingId)
                    .AsQueryable();

                var result = await mapper
                    .ProjectTo<CommonSubsidyOutputQueryModel>(subsidy)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
