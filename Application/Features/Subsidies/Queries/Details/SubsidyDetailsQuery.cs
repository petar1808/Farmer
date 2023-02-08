using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Subsidies.Queries.Details
{
    public class SubsidyDetailsQuery : IRequest<Result<CommonSubsidyOutputQueryModel>>
    {
        public int SubsidyId { get; set; }

        public class SubsidyDetailsQueryHandler : IRequestHandler<SubsidyDetailsQuery, Result<CommonSubsidyOutputQueryModel>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public SubsidyDetailsQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<CommonSubsidyOutputQueryModel>> Handle(
                SubsidyDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var subsidy = farmerDbContext
                .Subsidies
                .AsQueryable();

                var result = await mapper
                    .ProjectTo<CommonSubsidyOutputQueryModel>(subsidy)
                    .FirstOrDefaultAsync(x => x.Id == request.SubsidyId);

                if (result == null)
                {
                    return $"Субсидия с Ид: {request.SubsidyId} не съществува";
                }

                return result;
            }
        }
    }
}
