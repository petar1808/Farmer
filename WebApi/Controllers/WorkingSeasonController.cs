using Application.Features.WorkingSeasons.Commands.Create;
using Application.Features.WorkingSeasons.Commands.Delete;
using Application.Features.WorkingSeasons.Commands.Edit;
using Application.Features.WorkingSeasons.Queries.Details;
using Application.Features.WorkingSeasons.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/workingSeasons")]
    [Authorize(Roles = $"{IdentityRoles.AdminRole},  {IdentityRoles.UserRole}")]
    public class WorkingSeasonController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreateWorkingSeason(
            [FromBody] CreateWorkingSeasonCommand workingSeasonModel)
            => await base.Send(workingSeasonModel);

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<WorkingSeasonDetailsQueryOutputModel>> WorkingSeasonDetails(
            [FromRoute] WorkingSeasonDetailsQuery workingSeasonDetailsQuery)
            => await base.Send(workingSeasonDetailsQuery);

        [HttpGet]
        public async Task<ActionResult<List<WorkingSeasonBalanceListQueryOutputModel>>> ListWorkingSeason(
            [FromHeader] WorkingSeasonBalanceListQuery workingSeasonBalanceListQuery)
            => await base.Send(workingSeasonBalanceListQuery);

        [HttpPut]
        public async Task<ActionResult> Edit(
            EditWorkingSeasonCommand workingSeasonModel)
            => await base.Send(workingSeasonModel);

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteWorkingSeasonCommand deleteWorkingSeason)
            => await base.Send(deleteWorkingSeason);
    }
}
