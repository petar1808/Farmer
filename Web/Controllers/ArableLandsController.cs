using Application.Models.ArableLands;
using Application.Services.ArableLands;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.ArableLands;

namespace Web.Controllers
{
    public class ArableLandsController : Controller
    {
        private readonly IArableLandService arableLandService;
        private readonly IMapper _mapper;

        public ArableLandsController(
            IArableLandService arableLandService, 
            IMapper mapper)
        {
            this.arableLandService = arableLandService;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(AddArableLandModel arableLand)
        {
            if (!ModelState.IsValid)
            {
                return View(arableLand);
            }

            await this.arableLandService.Add(arableLand.Name, arableLand.SizeInDecar);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var arableLands = await this.arableLandService.GetAll();

            return View(_mapper.Map<List<GetArableLandViewModel>>(arableLands));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GetAreableLandModel arableLand)
        {
            if (arableLand == null)
            {
                return BadRequest();
            }

            await this.arableLandService.Edit(
                arableLand.Id,
                arableLand.Name,
                arableLand.SizeInDecar);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await this.arableLandService.Get(id);

            return View(_mapper.Map<GetArableLandViewModel>(result));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.arableLandService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
