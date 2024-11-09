using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace WebUI.Components.Validators
{
    public class CompareDateTimeByYearValidator : ValidatorBase
    {
        [Parameter]
        public override string Text { get; set; } = "Invalid date";

        [Parameter]
        public DateTime FromYear { get; set; }

        [Parameter]
        public int MustBeBiggerWithNumberOfYears { get; set; }

        protected override bool Validate(IRadzenFormComponent component)
        {
            object value = component.GetValue();

            if (value == null)
            {
                return false;
            }

            if (DateTime.TryParse(value.ToString(), out DateTime toDate))
            {
                if (toDate.Year - FromYear.Year == MustBeBiggerWithNumberOfYears)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Text = "Invalid date format";
                return false;
            }
        }
    }
}
