using Domain.Models;
using Infrastructure.DbContect;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.WorkingSeasons;

namespace Web.Controllers
{
    public class WorkingSeasonsController : Controller
    {
        private readonly FarmerDbContext dbContext;

        public WorkingSeasonsController(FarmerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddWorkingSeasonModel workingSeason)
        {
            if (!ModelState.IsValid)
            {
                return View(workingSeason);
            }

            var workingSeasonDate = new WorkingSeason(workingSeason.Name,
                workingSeason.StartDate,
                workingSeason.EndDate);

            dbContext.Add(workingSeasonDate);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult All()
        {
            var workingSeason = dbContext
                .WorkingSeasons
                .Select(a => new WorkingSeasonListingViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate
                })
                .ToList();

            return View(workingSeason); 
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var workingSeason = dbContext
                .WorkingSeasons
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (workingSeason == null)
            {
                throw new Exception();
            }

            var editView = new EditWorkingSeason()
            {
                Id = workingSeason.Id,
                Name = workingSeason.Name,
                StartDate = workingSeason.StartDate,
                EndDate = workingSeason.EndDate
            };

            return View(editView);
        }

        public bool Edit(
            int id,
            string name,
            DateTime? startdate,
            DateTime? endDate)
        {
            var workingSeasonData = this.dbContext.WorkingSeasons.FirstOrDefault(x => x.Id == id);

            if (workingSeasonData == null)
            {
                return false;
            }

            workingSeasonData.UpdateName(name);
            workingSeasonData.UpdateSratDate(startdate);
            workingSeasonData.UpdateEndDate(endDate);

            this.dbContext.Update(workingSeasonData);
            this.dbContext.SaveChanges();

            return true;
        }

        [HttpPost]
        public IActionResult Edit(EditWorkingSeason workingSeason, int id)
        {
            if (workingSeason == null)
            {
                return BadRequest();
            }

            var workingSeasonDate = this.dbContext.WorkingSeasons.FirstOrDefault(x => x.Id == id);

            if (workingSeason == null)
            {
                return NotFound();
            }

            var workingSeasonEdit = Edit(id, workingSeason.Name, workingSeason.StartDate, workingSeason.EndDate);

            if (workingSeasonEdit == false)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var workingSeasonData = dbContext.WorkingSeasons.FirstOrDefault(x => x.Id == id);

            if (workingSeasonData == null)
            {
                return NotFound();
            }

            dbContext.Remove(workingSeasonData);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
