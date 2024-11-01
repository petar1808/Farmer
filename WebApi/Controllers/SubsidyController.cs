using Application.Features.Subsidies.Commands.Create;
using Application.Features.Subsidies.Commands.Delete;
using Application.Features.Subsidies.Commands.Edit;
using Application.Features.Subsidies.Queries.Details;
using Application.Features.Subsidies.Queries.List;
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

        [HttpGet]
        [Route("{subsidyId:int}")]
        public async Task<ActionResult<DetailsSubsidyOutputQueryModel>> SubsidyDetails(
            [FromRoute] SubsidyDetailsQuery subsidyDetailsQuery)
            => await this.Send(subsidyDetailsQuery);

        [HttpPost]
        public async Task<ActionResult> CreateSubsidy(
            [FromBody] CreateSubsidyCommand subsidyModel)
            => await this.Send(subsidyModel);

        [HttpPut]
        public async Task<ActionResult> EditSubsidy(
            [FromBody] EditSubsidyCommand subsidyModel)
            => await this.Send(subsidyModel);

        [HttpDelete]
        [Route("{subsidyId:int}")]
        public async Task<ActionResult> DeleteSubsidy(
            [FromRoute] DeleteSubsidyCommand deleteSubsidy)
            => await this.Send(deleteSubsidy);
    }
}
