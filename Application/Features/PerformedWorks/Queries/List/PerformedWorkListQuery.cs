using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PerformedWorks.Queries.List
{
    public class PerformedWorkListQuery : IRequest<Result<List<PerformedWorkListQueryOutputModel>>>
    {
        public int SeedingId { get; set; }

        public class PerformedWorkListQueryHandler : IRequestHandler<PerformedWorkListQuery, Result<List<PerformedWorkListQueryOutputModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public PerformedWorkListQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<PerformedWorkListQueryOutputModel>>> Handle(
                PerformedWorkListQuery request,
                CancellationToken cancellationToken)
            {
                var seeding = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.Id == request.SeedingId, cancellationToken);

                if (!seeding)
                {
                    return $"Сеитба с Ид: {request.SeedingId} не съществува!";
                }

                var performedWork = farmerDbContext
                    .PerformedWorks
                    .Where(x => x.SeedingId == request.SeedingId)
                    .AsQueryable();

                var result = await mapper
                    .ProjectTo<PerformedWorkListQueryOutputModel>(performedWork)
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
