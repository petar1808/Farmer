using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace WebUI.Components.Validators
{
    public class NumericRangeValidator : ValidatorBase
    {
        [Parameter]
        public override string Text { get; set; } = "Not in the valid range";

        [Parameter]
        public decimal Min { get; set; }

        [Parameter]
        public decimal Max { get; set; } = decimal.MaxValue;

        [Parameter]
        public bool CanBeZero { get; set; } = false;

        protected override bool Validate(IRadzenFormComponent component)
        {
            object valueAsObj = component.GetValue();


            if (valueAsObj == null)
            {
                return false;
            }

            if (!decimal.TryParse(valueAsObj.ToString(), out decimal value))
            {
                return false;
            }

            if (CanBeZero && value == 0)
            {
                return true;
            }

            if (Min >= value && value <= Max)
            {
                return false;
            }

            return true;
        }
    }
}
