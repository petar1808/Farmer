using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ArableLands.Queries.Details
{
    public class ArableLandDetailsQuery : IRequest<Result<CommonArableLandOutputQueryModel>>
    {
        public int Id { get; set; }

        public class ArableLandDetailsQueryHandler : IRequestHandler<ArableLandDetailsQuery, Result<CommonArableLandOutputQueryModel>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public ArableLandDetailsQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<CommonArableLandOutputQueryModel>> Handle(
                ArableLandDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var arableLand = farmerDbContext
                .ArableLands
                .AsQueryable();

                var result = await mapper.ProjectTo<CommonArableLandOutputQueryModel>(arableLand).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (result == null)
                {
                    return $"Земя с Ид: {request} не съществува!";
                }

                return result;
            }
        }
    }
}
