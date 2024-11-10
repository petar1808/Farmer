using Application.Features.Reporting.Queries.FinancialSummary;
using Application.Features.Reporting.Queries.TimelineComparison;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/report")]
    [Authorize(Roles = $"{IdentityRoles.AdminRole},  {IdentityRoles.UserRole}")]
    public class ReportController : BaseApiController
    {
        [HttpGet]
        [Route("FinancialSummary")]
        public async Task<ActionResult<List<FinancialSummaryReportOutputModel>>> ListExpenses(
            [FromRoute] FinancialSummaryReportQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route("TimelineComparison")]
        public async Task<ActionResult<TimelineComparisonReportOutputModel>> TimelineComparisonReport(
            [FromQuery] TimelineComparisonReportQuery query)
            => await this.Send(query);
    }
}
