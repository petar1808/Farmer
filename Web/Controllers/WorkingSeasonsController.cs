using Application.Models.WorkingSeasons;
using Application.Services.WorikingSeasons;
using AutoMapper;
using Domain.Models;
using Infrastructure.DbContect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.WorkingSeasons;
using static Infrastructure.IdentityConstants.IdentityRoles;

namespace Web.Controllers
{
    public class WorkingSeasonsController : Controller
    {
        private readonly IWorkingSeasonService workingSeasonService;
        private readonly IMapper mapper;

        public WorkingSeasonsController(IMapper mapper, IWorkingSeasonService workingSeasonService)
        {
            this.mapper = mapper;
            this.workingSeasonService = workingSeasonService;
        }


        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(AddWorkingSeasonModel workingSeason)
        {
            if (!ModelState.IsValid)
            {
                return View(workingSeason);
            }

            await workingSeasonService.Add(workingSeason.Name, workingSeason.StartDate, workingSeason.EndDate);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = AdminRole)]
        public async Task<IActionResult> All()
        {
            var workingSeason = await workingSeasonService.GetAll();

            var a = mapper.Map<List<GetWorkingSeasonViewModel>>(workingSeason);

            return View(a); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var workingSeason = await workingSeasonService.Get(id);

            return View(mapper.Map<EditWorkingSeasonModel>(workingSeason));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditWorkingSeasonModel workingSeason)
        {
            if (workingSeason == null)
            {
                return BadRequest();
            }

            await workingSeasonService.Edit(
                workingSeason.Id,
                workingSeason.Name,
                workingSeason.StartDate,
                workingSeason.EndDate);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await workingSeasonService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
