using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Treatments.Queries.List
{
    public class TreatmentListQuery : IRequest<Result<List<TreatmentListQueryOutputModel>>>
    {
        public int SeedingId { get; set; }

        public class TreatmentListQueryHandler : IRequestHandler<TreatmentListQuery, Result<List<TreatmentListQueryOutputModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public TreatmentListQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<TreatmentListQueryOutputModel>>> Handle(
                TreatmentListQuery request,
                CancellationToken cancellationToken)
            {
                var seeding = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.Id == request.SeedingId, cancellationToken);

                if (!seeding)
                {
                    return $"Сеитба с Ид: {request.SeedingId} не съществува!";
                }

                var treatment = farmerDbContext
                    .Treatments
                    .Where(x => x.SeedingId == request.SeedingId)
                    .AsQueryable();

                var result = await mapper
                    .ProjectTo<TreatmentListQueryOutputModel>(treatment)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
