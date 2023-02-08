using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tenants.Queries.ListSelectionTenants
{
    public class SelectionTenantListQuery : IRequest<Result<List<SelectionTenantOutputQueryModel>>>
    {
        public class SelectionTenantListQueryHandler : IRequestHandler<SelectionTenantListQuery, Result<List<SelectionTenantOutputQueryModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public SelectionTenantListQueryHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result<List<SelectionTenantOutputQueryModel>>> Handle(
                SelectionTenantListQuery request,
                CancellationToken cancellationToken)
            {
                var tenants = await farmerDbContext
                .Tenants
                .Select(x => new SelectionTenantOutputQueryModel(x.Id, x.Name))
                .ToListAsync(cancellationToken);

                return tenants;
            }
        }
    }
}
