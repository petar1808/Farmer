using Application.Features.ArableLands.Commands.Create;
using Application.Features.ArableLands.Commands.Delete;
using Application.Features.ArableLands.Commands.Edit;
using Application.Features.ArableLands.Queries;
using Application.Features.ArableLands.Queries.Details;
using Application.Features.ArableLands.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/arableLands")]
    [Authorize(Roles = $"{IdentityRoles.AdminRole},  {IdentityRoles.UserRole}")]
    public class ArableLandController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreateArableLand(
            [FromBody] CreateArableLandCommand arableLandModel)
            => await base.Send(arableLandModel);

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CommonArableLandOutputQueryModel>> ArableLandDatails(
            [FromRoute] ArableLandDetailsQuery arableLandDetailsQuery)
            => await base.Send(arableLandDetailsQuery);

        [HttpGet]
        public async Task<ActionResult<List<CommonArableLandOutputQueryModel>>> ListArableLands(
            [FromHeader] ArableLandListQuery arableLandListQuery)
            => await base.Send(arableLandListQuery);

        [HttpPut]
        public async Task<ActionResult> EditArableLand(
            EditArableLandCommand arableLandModel)
            => await base.Send(arableLandModel);

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteArableLand(
            [FromRoute] DeleteArableLandCommand deleteArableLand)
            => await base.Send(deleteArableLand);
    }
}
