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

namespace Web.ControllersOld
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class WorkingSeasonsController : Controller
    {
        private readonly IWorkingSeasonService workingSeasonService;
        private readonly IMapper mapper;

        public WorkingSeasonsController(IWorkingSeasonService workingSeasonService, IMapper mapper)
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

            return RedirectToAction(actionName: nameof(All),
                controllerName: "WorkingSeasons");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var workingSeason = await workingSeasonService.GetAll();

            return View(mapper.Map<List<GetWorkingSeasonViewModel>>(workingSeason)); 
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
            if (!ModelState.IsValid)
            {
                return View(workingSeason);
            }

            if (workingSeason == null)
            {
                return BadRequest();
            }

            await workingSeasonService.Edit(
                workingSeason.Id,
                workingSeason.Name,
                workingSeason.StartDate,
                workingSeason.EndDate);

            return RedirectToAction(actionName: nameof(All),
                controllerName: "WorkingSeasons");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await workingSeasonService.Delete(id);

            return RedirectToAction(actionName: nameof(All),
                controllerName: "WorkingSeasons");
        }
    }
}
