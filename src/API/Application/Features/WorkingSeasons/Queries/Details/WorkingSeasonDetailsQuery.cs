using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WorkingSeasons.Queries.Details
{
    public class WorkingSeasonDetailsQuery : IRequest<Result<WorkingSeasonDetailsQueryOutputModel>>
    {
        public int Id { get; set; }

        public class WorkingSeasonDetailsQueryHandler : IRequestHandler<WorkingSeasonDetailsQuery, Result<WorkingSeasonDetailsQueryOutputModel>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public WorkingSeasonDetailsQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<WorkingSeasonDetailsQueryOutputModel>> Handle(
                WorkingSeasonDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var workingSeason = farmerDbContext
                        .WorkingSeasons
                        .AsQueryable();

                var result = await mapper
                    .ProjectTo<WorkingSeasonDetailsQueryOutputModel>(workingSeason)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (result == null)
                {
                    return $"Сезон с Ид: {request.Id} не съществува!";
                }

                return result;
            }
        }
    }
}
