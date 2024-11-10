using Application.Features.Articles.Commands.Create;
using Application.Features.Articles.Commands.Delete;
using Application.Features.Articles.Commands.Edit;
using Application.Features.Articles.Queries.Details;
using Application.Features.Articles.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{

    [Route("api/articles")]
    [Authorize(Roles = $"{IdentityRoles.AdminRole},  {IdentityRoles.UserRole}")]
    public class ArticleController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult> CreateArticle(
            [FromBody] CreateArticleCommand articleModel)
            => await this.Send(articleModel);

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ArticleDetailsQueryOutputModel>> ArticleDetails(
            [FromRoute] ArticleDetailsQuery articleDetailsQuery)
            => await this.Send(articleDetailsQuery);

        [HttpGet]
        public async Task<ActionResult<List<ArticleListQueryOutputModel>>> ListArticles(
            [FromQuery] ArticleListQuery articleListQuery)
            => await this.Send(articleListQuery);

        [HttpPut]
        public async Task<ActionResult> EditArticle(
            EditArticleCommand articleModel)
            => await this.Send(articleModel);

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteArticle(
            [FromRoute] DeleteArticleCommand deleteArticle)
            => await this.Send(deleteArticle);
    }
}
