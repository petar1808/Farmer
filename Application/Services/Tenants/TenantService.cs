using Application.Models;
using Application.Models.Common;
using Application.Models.Tenants;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Web;
using static Application.IdentityConstants;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services.Tenants
{
    public class TenantService : ITenantService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;

        public TenantService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task<Result> Add(AddTenantModel tenant)
        {
            var tenantUnique = await farmerDbContext
                .Tenants
                .AnyAsync(x => x.Name == tenant.Name);

            if (tenantUnique)
            {
                return "Има създадена организация със същото име";
            }

            await this.farmerDbContext.AddAsync(new Tenant(tenant.Name));
            await this.farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result<List<SelectionListModel>>> ListSelectionTenants()
        {
             var tenants =  await farmerDbContext
                .Tenants
                //.Where(x => !x.Users.Any())
                .Select(x => new SelectionListModel(x.Id, x.Name))
                .ToListAsync();

            return tenants;
        }

        public async Task<Result<List<ListTenantsWithUsersModel>>> ListTenantsWithUsers()
        {
            //var test = await farmerDbContext
            //    .Tenants
            //    .Where(x => x.Users.First().UserRoles.First().Name != IdentityRoles.SystemAdminRole)
            //    .Include(x => x.Users)
            //        .ThenInclude(x => x.UserRoles)
            //     .Select(x => new ListTenantsWithUsersModel
            //     {
            //         TanantId = x.Id,
            //         TanantName = x.Name,
            //         UserTenants = x.Users.Select(c => new UserTenant
            //         {
            //             UserEmail = c.UserName,
            //             UserName= c.FirstName,
            //             UserRole = c.UserRoles.First().Name
            //         }).ToList()
            //     })
            //    .ToListAsync();

            //var test2 = await farmerDbContext
            //     .Tenants

            //         .ToListAsync();


            var result =  await this.mapper.ProjectTo<ListTenantsWithUsersModel>(
                    farmerDbContext.Tenants.AsQueryable()
                )
                .ToListAsync();

            return result;
            //var tenantWithUsers = await farmerDbContext
            //    .Users
            //    .Where(x => x.UserRoles.Any(x => x.Name != IdentityRoles.SystemAdminRole))
            //    .Include(x => x.UserRoles)
            //    .Include(x => x.Tenant)
            //    .ToListAsync();

            //var result = mapper.Map<List<ListTenantsWithUsersModel>>(tenantWithUsers);

        }
    }
}
