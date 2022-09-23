using Application.Extensions;
using Application.Models.Articles;
using Application.Models.Common;
using Application.Services.Articles;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService articleService;

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddArticleModel articleModel)
        {
            await articleService.Add(articleModel);

            return Ok();
        }

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<GetArticleModel>> Get(int id)
        {
            var article = await articleService.Get(id);

            return article;
        }

        [HttpGet]
        public async Task<ActionResult<List<ListArticleModel>>> List()
        {
            return await articleService.List();
        }

        [HttpPut]
        public async Task<ActionResult> Edit(EditArticleModel articleModel)
        {
            await articleService.Edit(articleModel);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await articleService.Delete(id);

            return Ok();
        }
    }
}
