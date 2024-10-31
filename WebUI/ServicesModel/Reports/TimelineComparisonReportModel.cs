using System.Globalization;

namespace WebUI.ServicesModel.Reports
{
    public class TimelineComparisonReportModel
    {
        public List<ArableLandTimeLine> ArableLand1 { get; set; } = new List<ArableLandTimeLine>();

        public List<ArableLandTimeLine> ArableLand2 { get; set; } = new List<ArableLandTimeLine>();
    }

    public class ArableLandTimeLine
    {
        public string Date { get; set; } = string.Empty;

        public string Icon { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string? Value { get; set; }

        public string? AdditionalValue { get; set; }
    }
}
