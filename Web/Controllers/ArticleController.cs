using Application.Models.Articles;
using Application.Services.Articles;
using AutoMapper;
using CommonLibrary;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Articles;

namespace Web.Controllers
{

    [ApiController]
    [Route("api/articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<GetArticleModel>> Get(int id)
        {
            var result = await articleService.Get(id);

            return result;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetArticleModelCommon>>> List()
        {
            var articles = await articleService.GetAll();

            return articles.Select(x => new GetArticleModelCommon
            {
                Id = x.Id,
                ArticleType = (int)x.ArticleType,
                Name = x.Name
            }).ToList();
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddArticleApiModelTest articleModel)
        {

            await articleService.Add(articleModel.Name, ArticleType.Preparations);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Edit(EditArticleViewModel article)
        {
            await articleService.Edit(
                article.Id,
                article.Name,
                article.ArticleType);

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
