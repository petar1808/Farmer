using Application.Services.ArableLands;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.ArableLands;

namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ArableLandsController : Controller
    {
        private readonly IArableLandService arableLandService;
        private readonly IMapper mapper;

        public ArableLandsController(
            IArableLandService arableLandService, 
            IMapper mapper)
        {
            this.arableLandService = arableLandService;
            this.mapper = mapper;
        }



        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(AddArableLandViewModel arableLand)
        {          
            if (!ModelState.IsValid)
            {
                return View(arableLand);
            }

            await arableLandService.Add(arableLand.Name, arableLand.SizeInDecar);

            return RedirectToAction(actionName: nameof(All),
                controllerName: "ArableLands");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var arableLands = await arableLandService.GetAll();

            return View(mapper.Map<List<GetArableLandViewModel>>(arableLands));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditArableLandViewModel arableLand)
        {
            if (!ModelState.IsValid)
            {
                return View(arableLand);
            }

            if (arableLand == null)
            {
                return BadRequest();
            }

            await arableLandService.Edit(
                arableLand.Id,
                arableLand.Name,
                arableLand.SizeInDecar);

            return RedirectToAction(actionName: nameof(All),
                controllerName: "ArableLands");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await arableLandService.Get(id);

            return View(mapper.Map<EditArableLandViewModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await arableLandService.Delete(id);

            return RedirectToAction(actionName: nameof(All),
                controllerName: "ArableLands");
        }
    }
}
