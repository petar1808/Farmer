using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reporting.Queries.TimelineComparison
{
    public class TimelineComparisonReportOutputModel
    {
        public List<ArableLandTimeLine> ArableLand1 { get; set; } = new ();

        public List<ArableLandTimeLine> ArableLand2 { get; set; } = new();
    }

    public class ArableLandTimeLine
    {
        public DateTime DateTime { get; set; }

        public string Date => DateTime.ToString("MMMM dd, yyyy", new CultureInfo("bg-BG"));

        public string Icon { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string? Value { get; set; }

        public string? AdditionalValue { get; set; }
    }
}
