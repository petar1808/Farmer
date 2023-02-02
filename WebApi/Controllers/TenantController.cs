using Application.Features.Identity.Commands.CreateAdmin;
using Application.Features.Tenants.Commands.Create;
using Application.Features.Tenants.Queries.ListSelectionTenants;
using Application.Features.Tenants.Queries.ListTenantWithUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/tenants")]
    [Authorize(Roles = IdentityRoles.SystemAdminRole)]
    public class TenantController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreateTenant(
            [FromBody] CreateTenantCommand tenantModel)
            => await this.Send(tenantModel);

        [HttpPost]
        [Route("createAdmin")]
        public async Task<ActionResult> CreateAdmin(
            [FromBody] CreateAdminCommand createAdmin)
            => await this.Send(createAdmin);

        [HttpGet]
        public async Task<ActionResult<List<TenantWithUsersOutputQueryModel>>> ListTenantsWithUsers(
            [FromHeader] TenantListQuery tenantListQuery)
            => await this.Send(tenantListQuery);

        [HttpGet]
        [Route("listTenants")]
        public async Task<ActionResult<List<SelectionTenantOutputQueryModel>>> ListSelectionTenants(
            [FromHeader] SelectionTenantListQuery selectionTenantListQuery)
            => await this.Send(selectionTenantListQuery);
    }
}
