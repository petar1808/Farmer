﻿using Application.Features.ArableLands.Queries.SearchAvailableArableLand;
using Application.Features.PerformedWorks.Commands.Create;
using Application.Features.PerformedWorks.Commands.Delete;
using Application.Features.PerformedWorks.Commands.Edit;
using Application.Features.PerformedWorks.Queries.Details;
using Application.Features.PerformedWorks.Queries.List;
using Application.Features.Seedings.Commands.Create;
using Application.Features.Seedings.Commands.Edit;
using Application.Features.Seedings.Queries.DetailsSeedingSummary;
using Application.Features.Seedings.Queries.ListSeeding;
using Application.Features.Seedings.Queries.ListSownArableLands;
using Application.Features.Treatments.Commands.Create;
using Application.Features.Treatments.Commands.Delete;
using Application.Features.Treatments.Commands.Edit;
using Application.Features.Treatments.Queries.Details;
using Application.Features.Treatments.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/seeding")]
    [Authorize(Roles = $"{IdentityRoles.AdminRole},  {IdentityRoles.UserRole}")]
    public class SeedingController : BaseApiController
    {
        #region Seeding
        [HttpPost]
        public async Task<ActionResult> CreateSeading(
            [FromBody] CreateSeedingCommand seedingModel)
            => await base.Send(seedingModel);

        [HttpGet]
        public async Task<ActionResult<List<ListSeedingSelectionListOutputModel>>> ListSeadingSelectionList(
            [FromRoute] ListSeedingSelectionListQuery query)
            => await base.Send(query);

        [HttpGet]
        [Route("{seasonId:int}")]
        public async Task<ActionResult<List<ListSeedingQueryOutputModel>>> ListSeading(
            [FromRoute] ListSeedingQuery query)
            => await base.Send(query);

        [HttpGet]
        [Route("availableArableLands/{seasonId:int}")]
        public async Task<ActionResult<List<SearchAvailableArableLandOutputQueryModel>>> GetAvailableArableLands(
            [FromRoute] SearchAvailableArableLandQuery searchAvailableArableLandQuery)
            => await base.Send(searchAvailableArableLandQuery);

        [HttpGet]
        [Route("sownArableLand/{seasonId:int}")]
        public async Task<ActionResult<List<SownArableLandListQueryOutputModel>>> ListSownArableLand(
            [FromRoute] SownArableLandListQuery sownArableLandListQuery)
            => await base.Send(sownArableLandListQuery);

        [HttpGet]
        [Route("summary/{seedingId:int}")]
        public async Task<ActionResult<SeedingSummaryDetailsQueryOutputModel>> SeedingSummaryDetails(
            [FromRoute] SeedingSummaryDetailsQuery seedingSummaryDetailsQuery)
            => await this.Send(seedingSummaryDetailsQuery);

        [HttpPut]
        [Route("summary/{seedingId:int}")]
        public async Task<ActionResult> EditSeedingSummary(
            [FromBody] EditArableLandBalanceCommand updateSeedingModel,
            [FromRoute] int seedingId)
        {
            updateSeedingModel.SetSeedingId(seedingId);
            return await this.Send(updateSeedingModel);
        }

        #endregion

        #region PergormedWork
        [HttpPost]
        [Route("{seedingId:int}/performedWork")]
        public async Task<ActionResult> CreatePerformedWork(
            [FromBody] CreatePerformedWorkCommand performedWorkModel,
            [FromRoute] int seedingId)
        {
            performedWorkModel.SetSeedingId(seedingId);
            return await this.Send(performedWorkModel);
        }

        [HttpGet]
        [Route("{seedingId:int}/performedWork")]
        public async Task<ActionResult<List<PerformedWorkListQueryOutputModel>>> ListPerformedWork(
            [FromRoute] PerformedWorkListQuery performedWorkListQuery)
            => await this.Send(performedWorkListQuery);

        [HttpGet]
        [Route("performedWork/{performedWorkId:int}")]
        public async Task<ActionResult<PerformedWorkDetailsQueryOutputModel>> PerformedWorkDetails(
            [FromRoute] PerformedWorkDetailsQuery performedWorkDetailsQuery)
            => await this.Send(performedWorkDetailsQuery);

        [HttpPut]
        [Route("performedWork")]
        public async Task<ActionResult> EditPerformedWork(
            EditPerformedWorkCommand performedWorkModel)
            => await this.Send(performedWorkModel);

        [HttpDelete]
        [Route("performedWork/{performedWorkId:int}")]
        public async Task<ActionResult> DeletePerformedWork(
            [FromRoute] DeletePerformedWorkCommand deletePerformedWork)
            => await this.Send(deletePerformedWork);
        #endregion

        #region Treatment
        [HttpPost]
        [Route("{seedingId:int}/treatment")]
        public async Task<ActionResult> CreateТreatment(
            [FromBody] CreateTreatmentCommand treatmentModel,
            [FromRoute] int seedingId)
        {
            treatmentModel.SetSeedingId(seedingId);
            return await this.Send(treatmentModel);
        }

        [HttpGet]
        [Route("{seedingId:int}/treatment")]
        public async Task<ActionResult<List<TreatmentListQueryOutputModel>>> ListТreatments(
            [FromRoute] TreatmentListQuery treatmentListQuery)
            => await this.Send(treatmentListQuery);

        [HttpGet]
        [Route("treatment/{treatmentId:int}")]
        public async Task<ActionResult<TreatmentDetailsQueryOutputModel>> ТreatmentDetails(
            [FromRoute] TreatmentDetailsQuery treatmentListQuery)
            => await this.Send(treatmentListQuery);

        [HttpPut]
        [Route("treatment")]
        public async Task<ActionResult> EditТreatment(
            EditTreatmentCommand treatmentModel)
            => await this.Send(treatmentModel);

        [HttpDelete]
        [Route("treatment/{id:int}")]
        public async Task<ActionResult> DeleteТreatment(
            [FromRoute] DeleteTreatmentCommand deleteTreatment)
            => await this.Send(deleteTreatment);
        #endregion
    }
}
