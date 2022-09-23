using Application.Models.Common;
using Application.Models.WorkingSeasons;
using Application.Services.WorikingSeasons;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/workingSeasons")]
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
            await workingSeasonService.Add(workingSeasonModel);

            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<GetWorkingSeasonModel>> Get(int id)
        {
            var workingSeason = await workingSeasonService.Get(id);

            return workingSeason;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetWorkingSeasonModel>>> List()
        {
            var workingSeasons = await workingSeasonService.List();

            return workingSeasons;
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditWorkingSeasonModel workingSeasonModel)
        {
            await workingSeasonService.Edit(workingSeasonModel);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await workingSeasonService.Delete(id);

            return Ok();
        }
    }
}
