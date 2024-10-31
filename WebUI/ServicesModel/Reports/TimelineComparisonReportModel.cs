using System.Globalization;

namespace WebUI.ServicesModel.Reports
{
    public class TimelineComparisonReportModel
    {
        public List<ArableLandTimeLine> ArableLand1 { get; set; } = new List<ArableLandTimeLine>();

        public string SeedName1 { get; set; } = string.Empty;

        public decimal SeedsQuantityPerDecare1 { get; set; }

        public decimal HarvestedQuantityPerDecare1 { get; set; }

        public List<ArableLandTimeLine> ArableLand2 { get; set; } = new List<ArableLandTimeLine>();

        public string SeedName2 { get; set; } = string.Empty;

        public decimal SeedsQuantityPerDecare2 { get; set; }

        public decimal HarvestedQuantityPerDecare2 { get; set; }
    }

    public class ArableLandTimeLine
    {
        public string Date { get; set; } = string.Empty;

        public string Icon { get; set; } = string.Empty;

        public string Value { get; set; }

        public string? AdditionalValue { get; set; }
    }
}
