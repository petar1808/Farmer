using Application.Extensions;
using Application.Models.Common;
using Application.Models.PerformedWorks;
using Application.Models.Seedings;
using Application.Models.Subsidies;
using Application.Models.Тreatments;
using Application.Services.ArableLands;
using Application.Services.Articles;
using Application.Services.PerformedWorks;
using Application.Services.Seedings;
using Application.Services.Subsidies;
using Application.Services.Treatments;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/seeding")]
    [Authorize]
    public class SeedingController : ControllerBase
    {
        private readonly ISeedingService seedingService;
        private readonly IArableLandService arableLandService;
        private readonly IТreatmentService treatmentService;
        private readonly IPerformedWorkService performedWorkService;
        private readonly ISubsidyService subsidyService;

        public SeedingController(
            ISeedingService seedingService,
            IArableLandService arableLandService,
            IТreatmentService treatmentService,
            IPerformedWorkService performedWorkService,
            ISubsidyService subsidyService)
        {
            this.seedingService = seedingService;
            this.arableLandService = arableLandService;
            this.treatmentService = treatmentService;
            this.performedWorkService = performedWorkService;
            this.subsidyService = subsidyService;
        }

        #region Seeding
        [HttpPost]
        public async Task<ActionResult> AddSeading(AddSeedingModel seedingModel)
        {
            return await seedingService
                .AddSeeding(seedingModel)
                .ToActionResult();
        }

        [HttpGet]
        [Route("availableArableLands/{seasonId:int}")]
        public async Task<ActionResult<List<SelectionListModel>>> GetAvailableArableLands(int seasonId)
        {
            return await arableLandService
                .GetAvailableArableLands(seasonId)
                .ToActionResult();
        }

        [HttpGet]
        [Route("sownArableLand/{seasonId:int}")]
        public async Task<ActionResult<List<SownArableLandModel>>> GetSownArableLand(int seasonId)
        {
            return await seedingService
                .SownArableLands(seasonId)
                .ToActionResult();
        }

        [HttpGet]
        [Route("sownArableLand/balance/{seedingId:int}")]
        public async Task<ActionResult<GetArableLandBalance>> GetArableLandBalance(int seedingId)
        {
            return await seedingService
                .GetArableLandBalance(seedingId)
                .ToActionResult();
        }

        [HttpGet]
        [Route("summary/{seedingId:int}")]
        public async Task<ActionResult<GetSeedingSummaryModel>> GetSeedingSummary(int seedingId)
        {
            return await seedingService
                .GetSeedingSummary(seedingId)
                .ToActionResult();
        }

        [HttpPut]
        [Route("summary/{seedingId:int}")]
        public async Task<ActionResult> UpdateSeedingSummary(UpdateSeedingSummaryModel updateSeedingModel, int seedingId)
        {
            return await seedingService
                .UpdateSeedingSummary(updateSeedingModel, seedingId)
                .ToActionResult();
        }
        #endregion

        #region PergormedWork
        [HttpPost]
        [Route("{seedingId:int}/performedWork")]
        public async Task<ActionResult> AddPerformedWork([FromBody] AddPerformedWorkModel performedWorkModel, int seedingId)
        {
            return await performedWorkService
                .Add(performedWorkModel, seedingId)
                .ToActionResult();
        }

        [HttpGet]
        [Route("{seedingId:int}/performedWork")]
        public async Task<ActionResult<List<ListPerformedWorkModel>>> ListPerformedWork(int seedingId)
        {
            return await performedWorkService
                .List(seedingId)
                .ToActionResult();
        }

        [HttpGet]
        [Route("performedWork/{performedWorkId:int}")]
        public async Task<ActionResult<GetPerformedWorkModel>> GetPerformedWork(int performedWorkId)
        {
            return await performedWorkService
                .Get(performedWorkId)
                .ToActionResult();
        }

        [HttpPut]
        [Route("performedWork")]
        public async Task<ActionResult> EditPerformedWork(EditPerformedWorkModel performedWorkModel)
        {
            return await performedWorkService
                .Edit(performedWorkModel)
                .ToActionResult();
        }

        [HttpDelete]
        [Route("performedWork/{performedWorkId:int}")]
        public async Task<ActionResult> DeletePerformedWork(int performedWorkId)
        {
            return await performedWorkService
                .Delete(performedWorkId)
                .ToActionResult();
        }
        #endregion

        #region Treatment
        [HttpPost]
        [Route("{seedingId:int}/treatment")]
        public async Task<ActionResult> AddТreatment([FromBody] AddТreatmentModel treatmentModel, int seedingId)
        {
            return await treatmentService
                .Add(treatmentModel, seedingId)
                .ToActionResult();
        }

        [HttpGet]
        [Route("{seedingId:int}/treatment")]
        public async Task<ActionResult<List<ListТreatmentModel>>> ListТreatment(int seedingId)
        {
            return await treatmentService
                .List(seedingId)
                .ToActionResult();
        }

        [HttpGet]
        [Route("treatment/{treatmentId:int}")]
        public async Task<ActionResult<GetTreatmentModel>> GetТreatment(int treatmentId)
        {
            return await treatmentService
                .Get(treatmentId)
                .ToActionResult();
        }

        [HttpPut]
        [Route("treatment")]
        public async Task<ActionResult> EditТreatment(EditТreatmentModel treatmentModel)
        {
            return await treatmentService
                .Edit(treatmentModel)
                .ToActionResult();
        }

        [HttpDelete]
        [Route("treatment/{id:int}")]
        public async Task<ActionResult> DeleteТreatment(int id)
        {
            return await treatmentService
                .Delete(id)
                .ToActionResult();
        }
        #endregion

        #region
        [HttpPost]
        [Route("{seedingId:int}/subsidy")]
        public async Task<ActionResult> AddSubsidy([FromBody] AddSubsidyModel subsidyModel, int seedingId)
        {
            return await subsidyService
                .Add(subsidyModel, seedingId)
                .ToActionResult();
        }

        [HttpGet]
        [Route("{seedingId:int}/subsidy")]
        public async Task<ActionResult<List<ListSubsidiesModel>>> ListSubsidies(int seedingId)
        {
            return await subsidyService
                .List(seedingId)
                .ToActionResult();
        }

        [HttpGet]
        [Route("subsidy/{subsidyId:int}")]
        public async Task<ActionResult<GetSubsidyModel>> GetSubsidy(int subsidyId)
        {
            return await subsidyService
                .Get(subsidyId)
                .ToActionResult();
        }

        [HttpPut]
        [Route("subsidy")]
        public async Task<ActionResult> EditSubsidy(EditSubsidyModel subsidyModel)
        {
            return await subsidyService
                .Edit(subsidyModel)
                .ToActionResult();
        }

        [HttpDelete]
        [Route("subsidy/{subsidyId:int}")]
        public async Task<ActionResult> DeleteSubsidy(int subsidyId)
        {
            return await subsidyService
                .Delete(subsidyId)
                .ToActionResult();
        }

        #endregion
    }
}
