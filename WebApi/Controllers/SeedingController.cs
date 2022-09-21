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
    [Route("api/seeding")]
    public class SeedingController : ControllerBase
    {
        private readonly ISeedingService seedingService;
        private readonly IArableLandService arableLandService;
        private readonly IТreatmentService treatmentService;
        private readonly IPerformedWorkService performedWorkService;

        public SeedingController(
            ISeedingService seedingService,
            IArableLandService arableLandService,
            IТreatmentService treatmentService,
            IPerformedWorkService performedWorkService)
        {
            this.seedingService = seedingService;
            this.arableLandService = arableLandService;
            this.treatmentService = treatmentService;
            this.performedWorkService = performedWorkService;
        }

        #region Seeding
        [HttpGet]
        [Route("availableArableLands/{seasonId:int}")]
        public async Task<ActionResult<List<SelectionListModel>>> GetAvailableArableLands(int seasonId)
        {
            return await arableLandService.ArableLandsSelectionList(seasonId);
        }

        [HttpGet]
        [Route("sownArableLand/{seasonId:int}")]
        public async Task<ActionResult<List<SownArableLandModel>>> GetSownArableLand(int seasonId)
        {
            var seeding = await seedingService.SownArableLands(seasonId);

            return seeding;
        }

        [HttpPost]
        public async Task<ActionResult> AddSeading(AddSeedingModel seedingModel)
        {
            await seedingService.AddSeeding(seedingModel);

            return Ok();
        }

        [HttpGet]
        [Route("summary/{seedingId:int}")]
        public async Task<ActionResult<GetSeedingSummaryModel>> GetSeedingSummary(int seedingId)
        {
            var seeding = await seedingService.GetSeedingSummary(seedingId);

            return seeding;
        }

        [HttpPut]
        [Route("summary/{seedingId:int}")]
        public async Task<ActionResult> UpdateSeedingSummary(UpdateSeedingSummaryModel updateSeedingModel, int seedingId)
        {
            await seedingService.UpdateSeedingSummary(updateSeedingModel, seedingId);

            return Ok();
        }
        #endregion

        #region PergormedWork
        [HttpGet]
        [Route("{seedingId:int}/performedWork")]
        public async Task<ActionResult<List<ListPerformedWorkModel>>> ListPerformedWork(int seedingId)
        {
            var performedWork = await performedWorkService.List(seedingId);

            return performedWork;
        }

        [HttpGet]
        [Route("performedWork/{performedWorkId:int}")]
        public async Task<ActionResult<GetPerformedWorkModel>> GetPerformedWork(int performedWorkId)
        {
            var performedWork = await performedWorkService.Get(performedWorkId);

            return performedWork;
        }


        [HttpPost]
        [Route("performedWork")]
        public async Task<ActionResult> AddPerformedWork([FromBody] AddPerformedWorkModel performedWorkModel)
        {
            await performedWorkService.Add(performedWorkModel.SeedingId,
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
        [Route("performedWork/{performedWorkId:int}")]
        public async Task<ActionResult> DeletePerformedWork(int performedWorkId)
        {
            await performedWorkService.Delete(performedWorkId);

            return Ok();
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
            var treatment = await treatmentService.List(seedingId);

            return treatment;
        }

        [HttpGet]
        [Route("treatment/{treatmentId:int}")]
        public async Task<ActionResult<GetTreatmentModel>> GetТreatment(int treatmentId)
        {
            var treatment = await treatmentService.Get(treatmentId);

            return treatment;
        }

        [HttpPost]
        [Route("{seedingId:int}/treatment")]
        public async Task<ActionResult> AddТreatment([FromBody] AddТreatmentModel treatmentModel,int seedingId)
        {
            await treatmentService.Add(treatmentModel.Date,
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
            await treatmentService.Edit(тreatmentModel);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}/treatment")]
        public async Task<ActionResult> DeleteТreatment(int id)
        {
            await treatmentService.Delete(id);

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
