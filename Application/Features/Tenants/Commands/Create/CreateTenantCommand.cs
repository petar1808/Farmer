using Application.Models;
using Application.Services;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tenants.Commands.Create
{
    public class CreateTenantCommand : CreateTenantInputCommandModel, IRequest<Result>
    {

        public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public CreateTenantCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                CreateTenantCommand request,
                CancellationToken cancellationToken)
            {
                var tenantUnique = await farmerDbContext
                .Tenants
                .AnyAsync(x => x.Name == request.Name, cancellationToken);

                if (tenantUnique)
                {
                    return "Има създадена организация със същото име";
                }

                await this.farmerDbContext.AddAsync(new Tenant(request.Name), cancellationToken);
                await this.farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
