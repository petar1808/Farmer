using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Subsidies.Queries.List
{
    public class SubsidyListQuery : IRequest<Result<List<ListSubsidyOutputQueryModel>>>
    {
        public int SeasonId { get; set; }

        public class SubsidyListQueryHandler : IRequestHandler<SubsidyListQuery, Result<List<ListSubsidyOutputQueryModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public SubsidyListQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<ListSubsidyOutputQueryModel>>> Handle(
                SubsidyListQuery request,
                CancellationToken cancellationToken)
            {
                var subsidy = await farmerDbContext
                    .Subsidies
                    .Include(x => x.Seeding)
                        .ThenInclude(x => x.ArableLand)
                    .Where(x => x.Seeding.WorkingSeasonId == request.SeasonId)
                    .ToListAsync();

                var result = subsidy
                    .GroupBy(x => x.Date.ToString("MM-dd-yy"))
                    .Select((x, index) => new ListSubsidyOutputQueryModel
                    {
                        Id = index,
                        Date = DateTime.Parse(x.Key),
                        Income = x.Sum(x => x.Income),
                        ArableLands = x
                            .Select(c => new SubsidySlitByArableLand 
                            { 
                                ArableLandName = c.Seeding.ArableLand.Name, 
                                Income = c.Income
                            })
                            .ToList()
                    })
                    .OrderByDescending(x => x.Date)
                    .ToList();

                //var result = await mapper
                //    .ProjectTo<ListSubsidyOutputQueryModel>(subsidy)
                //    .OrderByDescending(x => x.Date)
                //    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
