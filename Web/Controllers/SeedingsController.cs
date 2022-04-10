using Domain.Models;
using Infrastructure.DbContect;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Seedings;

namespace Web.Controllers
{
    public class SeedingsController : Controller
    {
        private readonly FarmerDbContext dbContext;

        public SeedingsController(FarmerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult List(string workingSeason)
        {
            var seedingQuery = this.dbContext.Seedings.AsQueryable();

            if (!string.IsNullOrWhiteSpace(workingSeason))
            {
                seedingQuery = seedingQuery.Where(x => x.WorkingSeason.Name == workingSeason);
            }

            var seedings = seedingQuery
                .Select(x => new SeedingsListingViewModel
                {
                    Id = x.Id,
                    AreableLandName = x.ArableLand.Name,
                    ArticleName = x.Article.Name,
                    SizeInDecar = x.ArableLand.SizeInDecar,
                    SeasonName = x.WorkingSeason.Name,
                    StartDate = x.WorkingSeason.StartDate,
                    EndDate = x.WorkingSeason.EndDate
                })
                .ToList();

            var seedingWorkingSeason = dbContext
                .Seedings
                .Select(x => x.WorkingSeason.Name)
                .Distinct()
                .ToList();

            return View(new SeedingSearchViewModel
            {
                WorkingSeasons = seedingWorkingSeason,
                Seedings = seedings,
            });
        }

        [HttpGet]
        public IActionResult Add() => View(new AddSeedingViewModel
        {
            ArableLands = this.GetArableLand(),
            Articles = this.GetArticle(),
            WorkingSeaosons = this.GetWorkingSeason()
        });

        [HttpPost]
        public IActionResult Add(AddSeedingViewModel add)
        {
            var a = new Seeding(add.ArableLandId, add.WorkingSeasonId, add.ArticleId);

            dbContext.Add(a);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(List));
        }

        private IEnumerable<SelectedListArableLand> GetArableLand()
            => this.dbContext
            .ArableLands
            .Select(x => new SelectedListArableLand
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();


        private IEnumerable<SelectedListArticle> GetArticle()
           => this.dbContext
           .Articles
           .Select(x => new SelectedListArticle
           {
               Id = x.Id,
               Name = x.Name,
           })
           .ToList();

        private IEnumerable<SelectedListWorkingSeaoson> GetWorkingSeason()
            => this.dbContext
              .WorkingSeasons
              .Where(x => x.Id != 1)
              .Select(x => new SelectedListWorkingSeaoson
              {
                  Id = x.Id,
                  Name = x.Name,
              })
              .ToList();

    }
}
