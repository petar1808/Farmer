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
            var workingSeasons = await workingSeasonService.GetAll();

            return workingSeasons;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddWorkingSeasonModel workingSeason)
        {
            await workingSeasonService.Add(workingSeason.Name, workingSeason.StartDate, workingSeason.EndDate);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditWorkingSeasonModel workingSeason)
        {
            await workingSeasonService.Edit(
                workingSeason.Id,
                workingSeason.Name,
                workingSeason.StartDate,
                workingSeason.EndDate);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await workingSeasonService.Delete(id);

            return Ok();
        }

        [HttpGet]
        [Route("allSeasons")]
        public async Task<List<SelectionListModel>> GetAllSeasons()
        {
            return await workingSeasonService.SeasonsSelectionList();
        }
    }
}
