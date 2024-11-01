using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WorkingSeasons.Queries.List
{
    public class WorkingSeasonListQuery : IRequest<Result<List<WorkingSeasonListQueryOutputModel>>>
    {

        public class WorkingSeasonBalanceListQueryHandler : IRequestHandler<WorkingSeasonListQuery, Result<List<WorkingSeasonListQueryOutputModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public WorkingSeasonBalanceListQueryHandler(IMapper mapper, IFarmerDbContext farmerDbContext)
            {
                this.mapper = mapper;
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result<List<WorkingSeasonListQueryOutputModel>>> Handle(
                WorkingSeasonListQuery request,
                CancellationToken cancellationToken)
            {
                var workingSeasonBalance = farmerDbContext.WorkingSeasons
                            .OrderByDescending(x => x.StartDate)
                            .AsQueryable();

                var result = await mapper
                    .ProjectTo<WorkingSeasonListQueryOutputModel>(workingSeasonBalance)
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
