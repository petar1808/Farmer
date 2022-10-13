using Application.Extensions;
using Application.Models.Articles;
using Application.Models.Common;
using Application.Services.Articles;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/articles")]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService articleService;
        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddArticleModel articleModel)
        {  
            return await articleService
                .Add(articleModel)
                .ToActionResult();
        } 

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<GetArticleModel>> Get(int id)
        {
            return await articleService
                .Get(id)
                .ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<List<ListArticleModel>>> List()
        {
            return await articleService
                .List()
                .ToActionResult();
        }

        [HttpPut]
        public async Task<ActionResult> Edit(EditArticleModel articleModel)
        {
            return await articleService
                .Edit(articleModel)
                .ToActionResult();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await articleService
                .Delete(id)
                .ToActionResult();
        }
    }
}
