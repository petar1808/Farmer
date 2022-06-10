using Application.Models.Seedings;
using Application.Services.ArableLands;
using Application.Services.Articles;
using Application.Services.Seedings;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/seedings")]
    public class SeedingController : ControllerBase
    {
        private readonly ISeedingService seedingService;
        private readonly IArableLandService arableLandService;
        private readonly IArticleService articleService;

        public SeedingController(
            ISeedingService seedingService,
            IArableLandService arableLandService,
            IArticleService articleService)
        {
            this.seedingService = seedingService;
            this.arableLandService = arableLandService;
            this.articleService = articleService;
        }

        [HttpGet]
        [Route("list/{seasonId:int}")]
        public async Task<ActionResult<List<GetSeedingModel>>> List(int seasonId)
        {
            var seeding = await seedingService.List(seasonId);

            return seeding;
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<GetSeedingModel>> Get(int id)
        {
            var seedings = await seedingService.Get(id);

            return seedings;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] AddSeedingModel add)
        {
            await seedingService.Add(add.ArableLandId, add.WorkingSeasonId, add.ArticleId);

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(GetSeedingModel seeding)
        {
            await seedingService.Edit(
                seeding.Id,
                seeding.ArableLandId,
                seeding.ArticleId);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await seedingService.Delete(id);

            return Ok();
        }
    }
}
