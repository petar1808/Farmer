using Application.Services.Articles;
using Domain.Models;
using Infrastructure.DbContect;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.PerformedWork;

namespace Web.ControllersOld
{
    public class PerformedWorkController : Controller
    {
        private readonly FarmerDbContext farmerDbContext;
        private readonly IArticleService articleService;

        public PerformedWorkController(FarmerDbContext farmerDbContext, 
            IArticleService articleService)
        {
            this.farmerDbContext = farmerDbContext;
            this.articleService = articleService;
        }

        public IActionResult List(int seedingId)
        {
            var processing = farmerDbContext.PerformedWorks
                .Where(x => x.SeedingId == seedingId)
                .Select(x => new PerformedWorkListViewModel
                {
                    PerfomedWorkDate = x.PerforemedWorkDate,
                    PerfomedWorkType = x.WorkType.ToString(),
                    FuelUsed = x.FuelUsed,
                    FuelSum = x.FuelSum
                })
                .ToList();

            return View(new ProcessingSearchViewModel(seedingId,processing));
        }


        [HttpGet]
        public IActionResult AddProcessing(int seedingId) => View(new AddProcessingViewModel
        {
            SeedingId = seedingId
        });

        [HttpPost]
        public IActionResult AddProcessing(AddProcessingViewModel processing)
        {
            if (!ModelState.IsValid)
            {
                return View(processing);
            }

            var processingData = new PerformedWork(processing.SeedingId,
                processing.Type,
                processing.PerformedWorkDate,
                processing.FuelSum,
                processing.FuelUsed);

            farmerDbContext.Add(processingData);
            farmerDbContext.SaveChanges();
            
            return RedirectToAction(nameof(List), new { seedingId = processing.SeedingId });
        }

        [HttpGet]
        public async Task<IActionResult> AddTreatment(int seedingId) => View(new AddTreatmentViewModel
        {
            Articles = await this.articleService.TreatmentArticlesSelectionList(),
            SeedingId = seedingId
        });


        [HttpPost]
        public IActionResult AddTreatment(AddTreatmentViewModel treatment)
        {

            var treatmentData = new PerformedWork(treatment.SeedingId,
                treatment.Type,
                treatment.ArticleId,
                treatment.PerformedWorkDate,
                treatment.FuelSum,
                treatment.FuelUsed);

            farmerDbContext.Add(treatmentData);
            farmerDbContext.SaveChanges();

            return RedirectToAction(nameof(List), new { seedingId = treatment.SeedingId });
        }

    }
}
