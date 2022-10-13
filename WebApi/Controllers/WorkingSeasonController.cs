using Application.Models.Common;
using Application.Models.WorkingSeasons;
using Application.Services.WorikingSeasons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/workingSeasons")]
    [Authorize]
    public class WorkingSeasonController : ControllerBase
    {
        private readonly IWorkingSeasonService workingSeasonService;

        public WorkingSeasonController(IWorkingSeasonService workingSeasonService)
        {
            this.workingSeasonService = workingSeasonService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddWorkingSeasonModel workingSeasonModel)
        {
            return await workingSeasonService
                .Add(workingSeasonModel)
                .ToActionResult();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<GetWorkingSeasonModel>> Get(int id)
        {
            return await workingSeasonService
                .Get(id)
                .ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<List<GetWorkingSeasonModel>>> List()
        {
            return await workingSeasonService
                .List()
                .ToActionResult();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditWorkingSeasonModel workingSeasonModel)
        {
            return await workingSeasonService
                .Edit(workingSeasonModel)
                .ToActionResult();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await workingSeasonService
                .Delete(id)
                .ToActionResult();
        }
    }
}
