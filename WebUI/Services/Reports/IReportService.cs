using WebUI.ServicesModel.Reports;

namespace WebUI.Services.Reports
{
    public interface IReportService
    {
        Task<List<FinancialSummaryReportModel>> GetFinancialSummaryReport();

        Task<TimelineComparisonReportModel> GetTimelineComparisonReport(int SeedingId1, int SeedingId2);
    }
}
