using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Subsidies.Queries.List
{
    public class SubsidyListQuery : IRequest<Result<List<ListSubsidyOutputQueryModel>>>
    {
        public int SeasonId { get; set; }

        public class SubsidyListQueryHandler : IRequestHandler<SubsidyListQuery, Result<List<ListSubsidyOutputQueryModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public SubsidyListQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<ListSubsidyOutputQueryModel>>> Handle(
                SubsidyListQuery request,
                CancellationToken cancellationToken)
            {
                var subsidy = await farmerDbContext
                    .Subsidies
                        .Include(x => x.SubsidyByArableLands)
                            .ThenInclude(x => x.ArableLand)
                    .Where(x => x.WorkingSeasonId == request.SeasonId)
                    .ToListAsync(cancellationToken);

                var result = mapper
                    .Map<List<ListSubsidyOutputQueryModel>>(subsidy);

                return result;
            }
        }
    }
}
