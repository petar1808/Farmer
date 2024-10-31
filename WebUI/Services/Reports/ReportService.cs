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

        public async Task<TimelineComparisonReportModel> GetTimelineComparisonReport()
        {
            return await _httpService
                    .GetAsync<TimelineComparisonReportModel>($"api/report/TimelineComparison");
        }
    }
}
