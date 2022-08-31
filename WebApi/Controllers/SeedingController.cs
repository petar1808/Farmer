using Application.Extensions;
using Application.Models.Common;
using Application.Models.PerformedWorks;
using Application.Models.Seedings;
using Application.Models.Тreatments;
using Application.Services.ArableLands;
using Application.Services.Articles;
using Application.Services.PerformedWorks;
using Application.Services.Seedings;
using Application.Services.Treatments;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/seedings")]
    public class SeedingController : ControllerBase
    {
        private readonly ISeedingService seedingService;
        private readonly IArableLandService arableLandService;
        private readonly IТreatmentService тreatmentService;
        private readonly IPerformedWorkService performedWorkService;

        public SeedingController(
            ISeedingService seedingService,
            IArableLandService arableLandService,
            IТreatmentService тreatmentService,
            IPerformedWorkService performedWorkService)
        {
            this.seedingService = seedingService;
            this.arableLandService = arableLandService;
            this.тreatmentService = тreatmentService;
            this.performedWorkService = performedWorkService;
        }

        #region Seeding
        [HttpGet]
        [Route("{seasonId:int}/arableLand")]
        public async Task<ActionResult<List<SelectionListModel>>> GetAvailableArableLandSeeds(int seasonId)
        {
            return await arableLandService.ArableLandsSelectionList(seasonId);
        }

        [HttpGet]
        [Route("{seasonId:int}/{arableLandId:int}")]
        public async Task<ActionResult<GetSeedingModel>> GetSeeding(int seasonId, int arableLandId)
        {
            var seeding = await seedingService.GetSeeding(seasonId, arableLandId);

            return seeding;
        }

        [HttpPost]
        public async Task<ActionResult> AddSeading(AddSeedingModel seedingModel)
        {
            await seedingService.AddSeeding(seedingModel);

            return Ok();
        }
        #endregion

        #region PergormedWork
        [HttpPost]
        [Route("{seedingId:int}/performedWork")]
        public async Task<ActionResult> AddPerformedWork([FromBody] AddPerformedWorkModel performedWorkModel, int seedingId)
        {
            await performedWorkService.Add(seedingId,
                performedWorkModel.WorkType,
                performedWorkModel.Date,
                performedWorkModel.FuelPrice,
                performedWorkModel.AmountOfFuel);

            return Ok();
        }

        [HttpPut]
        [Route("performedWork")]
        public async Task<ActionResult> EditPerformedWork(EditPerformedWorkModel performedWorkModel)
        {
            await performedWorkService.Edit(performedWorkModel);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}/performedWork")]
        public async Task<ActionResult> DeletePerformedWork(int id)
        {
            await performedWorkService.Delete(id);

            return Ok();
        }

        [HttpGet]
        [Route("{seedingId:int}/performedWork")]
        public async Task<ActionResult<List<ListPerformedWorkModel>>> ListPerformedWork(int seedingId)
        {
            var performedWork = await performedWorkService.List(seedingId);

            return performedWork;
        }

        [HttpGet]
        [Route("{id:int}/getPerformedWork")]
        public async Task<ActionResult<GetPerformedWorkModel>> GetPerformedWork(int id)
        {
            var performedWork = await performedWorkService.Get(id);

            return performedWork;
        }

        [HttpGet]
        [Route("workTypes")]
        public ActionResult<List<SelectionListModel>> GetWorkTypes()
        {
            return EnumHelper.GetAllNamesAndValues<WorkType>().Select(x => new SelectionListModel(x.Key, x.Value)).ToList();
        }
        #endregion


        #region Treatment

        [HttpGet]
        [Route("{seedingId:int}/treatment")]
        public async Task<ActionResult<List<ListТreatmentModel>>> ListТreatment(int seedingId)
        {
            var treatment = await тreatmentService.List(seedingId);

            return treatment;
        }

        [HttpGet]
        [Route("{id:int}/getTreatment")]
        public async Task<ActionResult<GetTreatmentModel>> GetТreatment(int id)
        {
            var treatment = await тreatmentService.Get(id);

            return treatment;
        }

        [HttpPost]
        [Route("{seedingId:int}/treatment")]
        public async Task<ActionResult> AddТreatment([FromBody] AddТreatmentModel treatmentModel,int seedingId)
        {
            await тreatmentService.Add(treatmentModel.Date,
                treatmentModel.ТreatmentType,
                treatmentModel.AmountOfFuel,
                treatmentModel.FuelPrice,
                treatmentModel.ArticleId,
                treatmentModel.ArticleQuantity,
                seedingId,
                treatmentModel.ArticlePrice);

            return Ok();
        }

        [HttpPut]
        [Route("treatment")]
        public async Task<ActionResult> EditТreatment(EditТreatmentModel тreatmentModel)
        {
            await тreatmentService.Edit(тreatmentModel);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}/treatment")]
        public async Task<ActionResult> DeleteТreatment(int id)
        {
            await тreatmentService.Delete(id);

            return Ok();
        }

        [HttpGet]
        [Route("treatmentType")]
        public ActionResult<List<SelectionListModel>> GetTreatmentTypes()
        {
            return EnumHelper.GetAllNamesAndValues<ТreatmentType>().Select(x => new SelectionListModel(x.Key, x.Value)).ToList();
        }
        #endregion
    }
}
