using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WorkingSeasons.Queries.List
{
    // TO DO: Rename and remove not needed properties, once the refactoring of the UI is done
    public class WorkingSeasonBalanceListQuery : IRequest<Result<List<WorkingSeasonBalanceListQueryOutputModel>>>
    {
        
        public class WorkingSeasonBalanceListQueryHandler : IRequestHandler<WorkingSeasonBalanceListQuery, Result<List<WorkingSeasonBalanceListQueryOutputModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public WorkingSeasonBalanceListQueryHandler(IMapper mapper, IFarmerDbContext farmerDbContext)
            {
                this.mapper = mapper;
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result<List<WorkingSeasonBalanceListQueryOutputModel>>> Handle(
                WorkingSeasonBalanceListQuery request,
                CancellationToken cancellationToken)
            {
                var workingSeasonBalance = await farmerDbContext
                .WorkingSeasons
                .Include(x => x.Seedings)
                    .ThenInclude(x => x.ArableLand)
                .Include(x => x.Seedings)
                    .ThenInclude(x => x.Article)
                .Include(x => x.Seedings)
                    .ThenInclude(x => x.PerformedWorks)
                .Include(x => x.Seedings)
                    .ThenInclude(x => x.Treatments)
                .OrderByDescending(x => x.StartDate)
                .ToListAsync(cancellationToken);

                var result = mapper.Map<List<WorkingSeasonBalanceListQueryOutputModel>>(workingSeasonBalance);

                return result;
            }
        }
    }
}
