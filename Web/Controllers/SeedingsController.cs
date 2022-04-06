using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.Seedings;

namespace Web.Controllers
{
    public class SeedingsController : Controller
    {
        private readonly DbContext dbContext;

        public SeedingsController(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddSeedingsModel addSeedings)
        {
            if (!ModelState.IsValid)
            {
                return View(addSeedings);
            }

            var articleDate = new Seeding(addSeedings.ArableLandId, addSeedings.WorkingSeasonId, addSeedings.ArticleId);

            dbContext.Add(articleDate);
            dbContext.SaveChanges();

            return View();
        }
    }
}
