using Application.Services.ArableLands;
using Application.Services.Articles;
using Application.Services.Seedings;
using Application.Services.WorikingSeasons;
using AutoMapper;
using Domain.Models;
using Infrastructure.DbContect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Seedings;

namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class SeedingsController : Controller
    {
        private readonly FarmerDbContext dbContext;
        private readonly ISeedingService seedingService;
        private readonly IMapper mapper;
        private readonly IArableLandService arableLandService;
        private readonly IArticleService articleService;

        public SeedingsController(
            FarmerDbContext dbContext,
            ISeedingService seedingService,
            IMapper mapper,
            IArableLandService arableLandService,
            IArticleService articleService)
        {
            this.dbContext = dbContext;
            this.seedingService = seedingService;
            this.mapper = mapper;
            this.arableLandService = arableLandService;
            this.articleService = articleService;
        }

        [HttpGet]
        public IActionResult List(int seasonId)
        {
            var seeding = this.mapper.Map<List<SeedingsGetViewModel>>(seedingService.List(seasonId));

            return View(new SeedingSearchViewModel(seasonId, seeding));
        }

        [HttpGet]
        public async Task<IActionResult> Add(int seasonId) => View(new AddSeedingViewModel
        {
            ArableLands = await this.arableLandService.ArableLandsSelectionList(seasonId),
            Articles = await this.articleService.SeedsArticlesSelectionList(),
            WorkingSeasonId = seasonId
        });

        [HttpGet]
        public async Task<IActionResult> Edit(int seasonId, int currentArableLandId, int seedingId, int articleId)
        {
            var result = new EditSeedingViewModel
            {
                Id = seedingId,
                ArableLandId = currentArableLandId,
                ArticleId = articleId,
                ArableLands = await this.arableLandService
                                .ArableLandsSelectionList(seasonId, currentArableLandId),
                Articles = await this.articleService
                                .SeedsArticlesSelectionList(),
                WorkingSeasonId = seasonId,
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSeedingViewModel seeding)
        {
            if (seeding == null)
            {
                return BadRequest();
            }

            await seedingService.Edit(
                seeding.Id,
                seeding.ArableLandId, 
                seeding.ArticleId);

            return RedirectToAction(nameof(List), new { seasonId = seeding.WorkingSeasonId });
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddSeedingViewModel add)
        {
            if (add.ArableLandId == 0 || add.ArticleId == 0)
            {
                return BadRequest();
            }

            await seedingService.Add(add.ArableLandId, add.WorkingSeasonId, add.ArticleId);

            return RedirectToAction(nameof(List), new { seasonId = add.WorkingSeasonId });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int seedingId, int seasonId)
        {
            await seedingService.Delete(seedingId);

            return RedirectToAction(nameof(List), new { seasonId = seasonId });
        }


    }
}
