using Domain.Models;
using Infrastructure.DbContect;
using Microsoft.AspNetCore.Mvc;
using Web.Models.ArableLands;

namespace Web.Controllers
{
    public class ArableLandsController : Controller
    {
        private readonly FarmerDbContext dbContext;

        public ArableLandsController(FarmerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddArableLandModel arableLand)
        {
            if (!ModelState.IsValid)
            {
                return View(arableLand);
            }

            var arableLandData = new ArableLand(arableLand.Name, arableLand.SizeInDecar);
            dbContext.Add(arableLandData);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult All()
        {
            var arableLands = dbContext
                .ArableLands
                .Select(a => new ArableLandListingViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    SizeInDecar = a.SizeInDecar
                })
                .ToList();

            return View(arableLands);
        }

        public bool Edit(
            int id,
            string name,
            int sizeInDecar)
        {
            var arableLand = this.dbContext.ArableLands.FirstOrDefault(x => x.Id == id);

            if (arableLand == null)
            {
                return false;
            }

            arableLand.UpdateName(name);
            arableLand.UpdateSizeInDecar(sizeInDecar);
   
            this.dbContext.Update(arableLand);
            this.dbContext.SaveChanges();

            return true;
        }

        [HttpPost]
        public IActionResult Edit(EditAreableLandModel arableLand, int id)
        {
            if (arableLand == null)
            {
                return BadRequest();
            }

            var arableLandDate = this.dbContext.ArableLands.FirstOrDefault(x => x.Id == id);
            
            if (arableLandDate == null)
            {
                return NotFound();
            }

            var arableLandEdit = Edit(id, arableLand.Name, arableLand.SizeInDecar);

            if (arableLandEdit == false)
            {
                return BadRequest();
            }
            
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var areableLand = dbContext
                .ArableLands
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (areableLand == null)
            {
                throw new Exception();
            }

            var editView = new EditAreableLandModel()
            {
                Id = areableLand.Id,
                Name = areableLand.Name,
                SizeInDecar = areableLand.SizeInDecar
            };

            return View(editView);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var arableLandData = dbContext.ArableLands.FirstOrDefault(x => x.Id == id);

            if (arableLandData == null)
            {
                return NotFound();
            }

            dbContext.Remove(arableLandData);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
