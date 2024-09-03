using Application.Mappings;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Seedings.Queries.DetailsSeedingSummary
{
    public class SeedingSummaryDetailsQuery : IRequest<Result<SeedingSummaryDetailsQueryOutputModel>>, IMapFrom<Seeding>
    {
        public int SeedingId { get; set; }

        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<Seeding, SeedingSummaryDetailsQuery>()
               .ForMember(x => x.SeedingId, cfg => cfg.MapFrom(c => c.Id));

        public class SeedingSummaryDetailsQueryHandler : IRequestHandler<SeedingSummaryDetailsQuery, Result<SeedingSummaryDetailsQueryOutputModel>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public SeedingSummaryDetailsQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<SeedingSummaryDetailsQueryOutputModel>> Handle(
                SeedingSummaryDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var seeding = farmerDbContext
                .Seedings
                .AsQueryable();

                var result = await mapper.ProjectTo<SeedingSummaryDetailsQueryOutputModel>(seeding).FirstOrDefaultAsync(x => x.Id == request.SeedingId);

                if (result == null)
                {
                    return $"Сеитба с Ид: {request.SeedingId} не съществува!";
                }

                return result;
            }
        }
    }
}
