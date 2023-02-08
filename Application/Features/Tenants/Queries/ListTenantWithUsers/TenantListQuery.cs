using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tenants.Queries.ListTenantWithUsers
{
    public class TenantListQuery : IRequest<Result<List<TenantWithUsersOutputQueryModel>>>
    {
        public class TenantListQueryHandler : IRequestHandler<TenantListQuery, Result<List<TenantWithUsersOutputQueryModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public TenantListQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<TenantWithUsersOutputQueryModel>>> Handle(
                TenantListQuery request,
                CancellationToken cancellationToken)
            {
                var result = await mapper
                    .ProjectTo<TenantWithUsersOutputQueryModel>(farmerDbContext.Tenants.AsQueryable())
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
