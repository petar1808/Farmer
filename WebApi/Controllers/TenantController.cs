using Application.Models.Common;
using Application.Models.Tenants;
using Application.Services.Identity;
using Application.Services.Tenants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/tenants")]
    [Authorize(Roles = IdentityRoles.SystemAdminRole)]
    public class TenantController
    {
        private readonly ITenantService tenantService;
        private readonly IIdentityService identityService;

        public TenantController(ITenantService tenantService, IIdentityService identityService)
        {
            this.tenantService = tenantService;
            this.identityService = identityService;
        }


        [HttpPost]
        public async Task<ActionResult> AddTenant(AddTenantModel tenantModel)
        {
            return await tenantService
                .Add(tenantModel)
                .ToActionResult();
        }

        [HttpPost]
        [Route("createAdmin")]
        public async Task<ActionResult> CreateAdmin(CreateAdminModel createAdmin)
        {
            return await identityService
                .CreateAdmin(createAdmin)
                .ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<List<ListTenantsWithUsersModel>>> ListTenantsWithUsers()
        {
            return await tenantService
                .ListTenantsWithUsers()
                .ToActionResult();
        }

        [HttpGet]
        [Route("listTenants")]
        public async Task<ActionResult<List<SelectionListModel>>> ListSelectionTenants()
        {
            return await tenantService
                .ListSelectionTenants()
                .ToActionResult();
        }
    }
}
