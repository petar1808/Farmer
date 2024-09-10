using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ArableLands.Queries.List
{
    public class ArableLandListQuery : IRequest<Result<List<CommonArableLandOutputQueryModel>>>
    {
        public class ArableLandListQueryHandler : IRequestHandler<ArableLandListQuery, Result<List<CommonArableLandOutputQueryModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public ArableLandListQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<CommonArableLandOutputQueryModel>>> Handle(
                ArableLandListQuery request,
                CancellationToken cancellationToken)
            {
                var arableLands = farmerDbContext.ArableLands.AsQueryable();

                var result = await mapper.ProjectTo<CommonArableLandOutputQueryModel>(arableLands)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
