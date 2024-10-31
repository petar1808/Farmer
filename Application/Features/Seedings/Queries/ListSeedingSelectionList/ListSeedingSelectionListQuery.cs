using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Seedings.Queries.ListSeeding
{
    public class ListSeedingSelectionListQuery : IRequest<Result<List<ListSeedingSelectionListOutputModel>>>
    {
        public class ListSeedingQueryHandler : IRequestHandler<ListSeedingSelectionListQuery, Result<List<ListSeedingSelectionListOutputModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public ListSeedingQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<ListSeedingSelectionListOutputModel>>> Handle(ListSeedingSelectionListQuery request, CancellationToken cancellationToken)
            {
                var query = farmerDbContext
                    .Seedings
                    .AsQueryable();

                var result = await mapper.ProjectTo<ListSeedingSelectionListOutputModel>(query)
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
