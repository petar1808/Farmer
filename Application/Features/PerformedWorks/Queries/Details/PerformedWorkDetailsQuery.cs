using Application.Models;
using Application.Models.PerformedWorks;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PerformedWorks.Queries.Details
{
    public class PerformedWorkDetailsQuery : IRequest<Result<PerformedWorkDetailsQueryOutputModel>>
    {
        public int PerformedWorkId { get; set; }

        public class PerformedWorkDetailsQueryHandler : IRequestHandler<PerformedWorkDetailsQuery, Result<PerformedWorkDetailsQueryOutputModel>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public PerformedWorkDetailsQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<PerformedWorkDetailsQueryOutputModel>> Handle(
                PerformedWorkDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var performedWork = farmerDbContext
                .PerformedWorks
                .AsQueryable();

                var result = await mapper
                    .ProjectTo<PerformedWorkDetailsQueryOutputModel>(performedWork)
                    .FirstOrDefaultAsync(x => x.Id == request.PerformedWorkId, cancellationToken);

                if (result == null)
                {
                    return $"Обработка с Ид: {request.PerformedWorkId} не съществува!";
                }

                return result;
            }
        }
    }
}
