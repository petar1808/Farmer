using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Seedings.Queries.ListSeeding
{
    public class ListSeedingQuery : IRequest<Result<List<ListSeedingQueryOutputModel>>>
    {
        public int SeasonId { get; set; }

        public class ListSeedingQueryHandler : IRequestHandler<ListSeedingQuery, Result<List<ListSeedingQueryOutputModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public ListSeedingQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<ListSeedingQueryOutputModel>>> Handle(ListSeedingQuery request, CancellationToken cancellationToken)
            {
                var query = farmerDbContext
                    .Seedings
                    .Where(x => x.WorkingSeasonId == request.SeasonId)
                    .AsQueryable();

                var result = await mapper.ProjectTo<ListSeedingQueryOutputModel>(query)
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
