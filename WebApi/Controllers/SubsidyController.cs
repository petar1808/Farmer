using Application.Features.Subsidies.Queries.List;
using Application.Features.Subsidies.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/subsidy")]
    [Authorize(Roles = $"{IdentityRoles.AdminRole},  {IdentityRoles.UserRole}")]
    public class SubsidyController : BaseApiController
    {
        [HttpGet]
        [Route("list/{seasonId:int}")]
        public async Task<ActionResult<List<ListSubsidyOutputQueryModel>>> ListSubsidies(
            [FromRoute] SubsidyListQuery subsidyListQuery)
            => await this.Send(subsidyListQuery);
    }
}
