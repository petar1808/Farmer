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
            return await articleService.GetAll();
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddArticleModel articleModel)
        {
            await articleService.Add(articleModel.Name, (ArticleType)articleModel.ArticleType);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Edit(EditArticleModel article)
        {
            await articleService.Edit(
                article.Id,
                article.Name,
                (ArticleType)article.ArticleType);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await articleService.Delete(id);

            return Ok();
        }

        [HttpGet]
        [Route("articleTypes")]
        public ActionResult<List<SelectionListModel>> GetArticleTypes()
        {
           return EnumHelper.GetAllNamesAndValues<ArticleType>().Select(x => new SelectionListModel(x.Key,x.Value)).ToList();
        }

        [HttpGet]
        [Route("seeds")]
        public async Task<ActionResult<List<SelectionListModel>>> GetSeeds()
        {
            return await articleService.SeedsArticlesSelectionList();
        }
    }
}
