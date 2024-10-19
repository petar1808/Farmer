using Application.Features.Articles.Queries;
using Application.Features.Articles.Queries.ListArticleType;
using Application.Features.Articles.Queries.SearchArticleType;
using Application.Features.Expenses.Queries.ListExpenseType;
using Application.Features.PerformedWorks.Queries.ListWorkType;
using Application.Features.Treatments.Queries.ListTreatmentType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/assets")]
    [Authorize(Roles = $"{IdentityRoles.AdminRole},  {IdentityRoles.UserRole}")]
    public class AssetsController : BaseApiController
    {
        [HttpGet]
        [Route("articleTypes")]
        public async Task<ActionResult<List<CommonArticleTypeOutputQueryModel>>> ListArticleType(
             [FromRoute] ArticleTypeListQuery query)
            => await base.Send(query);


        [HttpGet]
        [Route("treatment/{type:int}")]
        public async Task<ActionResult<List<CommonArticleTypeOutputQueryModel>>> SearchArticleByType(
            [FromRoute] SearchArticleByTypeQuery searchArticleByTypeQuery)
            => await base.Send(searchArticleByTypeQuery);

        [HttpGet]
        [Route("workTypes")]
        public async Task<ActionResult<List<WorkTypeOutputQueryModel>>> GetWorkTypes(
            [FromRoute] WorkTypeListQuery query)
            => await base.Send(query);

        [HttpGet]
        [Route("treatmentType")]
        public async Task<ActionResult<List<TreatmentTypeOutputQueryModel>>> GetTreatmentTypes(
            [FromRoute] TreatmentTypeListQuery query)
            => await base.Send(query);

        [HttpGet]
        [Route("expenseType")]
        public async Task<ActionResult<List<ListExpenseTypeQueryOutputModel>>> GetExpenseTypes(
            [FromRoute] ListExpenseTypeQuery query)
            => await base.Send(query);
    }
}
