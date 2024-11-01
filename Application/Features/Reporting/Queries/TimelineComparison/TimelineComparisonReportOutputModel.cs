using System.Globalization;

namespace Application.Features.Reporting.Queries.TimelineComparison
{
    public class TimelineComparisonReportOutputModel
    {
        public List<ArableLandTimeLine> ArableLand1 { get; set; } = new();
        public string SeedName1 { get; set; } = string.Empty;
        public decimal SeedsQuantityPerDecare1 { get; set; }
        public decimal HarvestedQuantityPerDecare1 { get; set; }
        public List<ArableLandTimeLine> ArableLand2 { get; set; } = new();
        public string SeedName2 { get; set; } = string.Empty;
        public decimal SeedsQuantityPerDecare2 { get; set; }
        public decimal HarvestedQuantityPerDecare2 { get; set; }
    }

    public class ArableLandTimeLine
    {
        public DateTime DateTime { get; set; }

        public string Date => DateTime.ToString("MMMM dd, yyyy", new CultureInfo("bg-BG"));

        public string Icon { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public string? AdditionalValue { get; set; }
    }
}
