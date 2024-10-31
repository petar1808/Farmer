using WebUI.ServicesModel.PerformedWork;
using WebUI.ServicesModel.Reports;

namespace WebUI.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly IHttpService _httpService;

        public ReportService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<FinancialSummaryReportModel>> GetFinancialSummaryReport()
        {
            return await _httpService
                    .GetAsync<List<FinancialSummaryReportModel>>($"api/report/FinancialSummary");
        }

        public async Task<TimelineComparisonReportModel> GetTimelineComparisonReport(
            int SeedingId1, 
            int SeedingId2
            )
        {
            var queryParams = new Dictionary<string, string>
                {
                    { "SeedingId1", SeedingId1.ToString() },
                    { "SeedingId2", SeedingId2.ToString() }
                };
            return await _httpService
                    .GetAsync<TimelineComparisonReportModel>($"api/report/TimelineComparison", queryParams);
        }
    }
}
