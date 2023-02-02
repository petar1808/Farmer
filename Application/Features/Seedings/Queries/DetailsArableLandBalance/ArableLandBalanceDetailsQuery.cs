using Application.Mappings;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Seedings.Queries.DetailsArableLandBalance
{
    public class ArableLandBalanceDetailsQuery : IRequest<Result<ArableLandBalanceDetailsQueryOutputModel>>, IMapFrom<Seeding>
    {
        public int SeedingId { get; set; }

        public class ArableLandBalanceDetailsQueryHandler : IRequestHandler<ArableLandBalanceDetailsQuery, Result<ArableLandBalanceDetailsQueryOutputModel>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public ArableLandBalanceDetailsQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<ArableLandBalanceDetailsQueryOutputModel>> Handle(
                ArableLandBalanceDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var seeding = await farmerDbContext
                .Seedings
                .Include(x => x.Article)
                .Include(x => x.ArableLand)
                .Include(x => x.Treatments)
                .Include(x => x.PerformedWorks)
                .Include(x => x.Subsidies)
                .FirstOrDefaultAsync(x => x.Id == request.SeedingId);

                if (seeding == null)
                {
                    return $"Сеитба с Ид: {request.SeedingId} не съществува!";
                }

                var result = mapper.Map<ArableLandBalanceDetailsQueryOutputModel>(seeding);

                return result;
            }
        }
    }
}
