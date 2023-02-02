using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Treatments.Queries.Details
{
    public class TreatmentDetailsQuery : IRequest<Result<TreatmentDetailsQueryOutputModel>>
    {
        public int TreatmentId { get; set; }

        public class TreatmentDetailsQueryHandler : IRequestHandler<TreatmentDetailsQuery, Result<TreatmentDetailsQueryOutputModel>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public TreatmentDetailsQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<TreatmentDetailsQueryOutputModel>> Handle(
                TreatmentDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var treatment = farmerDbContext
                    .Treatments
                    .AsQueryable();

                var result = await mapper
                    .ProjectTo<TreatmentDetailsQueryOutputModel>(treatment)
                    .FirstOrDefaultAsync(x => x.Id == request.TreatmentId, cancellationToken); ;

                if (result == null)
                {
                    return $"Третиране с Ид: {request.TreatmentId} не съществува!";
                }

                return result;
            }
        }
    }
}
