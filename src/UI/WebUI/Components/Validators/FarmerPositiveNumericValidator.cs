using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Components.Validators
{
    public class FarmerPositiveNumericValidator : ValidatorBase
    {
        [Parameter]
        public override string Text { get; set; } = $"Стойността трябва да е по-голяма от 0";

        public IComparable Min { get; set; } = 0;

        public IComparable Max { get; set; } = int.MaxValue;

        [Parameter]
        public bool AllowNull { get; set; } = false;

        [Parameter]
        public bool AllowZero { get; set; } = false;

        protected override bool Validate(IRadzenFormComponent component)
        {
            if (Min == null && Max == null)
            {
                throw new ArgumentException("Min and Max cannot be both null");
            }

            object value = component.GetValue();

            if (value == null)
            {
                Text = "Стойност не може да е празна";
                return AllowNull;
            }

            if (!TryConvertToType(value, typeof(decimal), out var convertedValue) || !(convertedValue is decimal decimalValue))
            {
                return false;
            }

            if (!AllowZero && decimalValue == 0)
            {
                Text = "Стойност не може да е 0";
                return false;
            }

            if (Min != null && decimalValue.CompareTo(Convert.ChangeType(Min, typeof(decimal))) < 0)
            {
                return false;
            }

            if (Max != null && decimalValue.CompareTo(Convert.ChangeType(Max, typeof(decimal))) > 0)
            {
                return false;
            }

            return true;
        }

        private bool TryConvertToType(object value, Type type, out object convertedValue)
        {
            try
            {
                convertedValue = Convert.ChangeType(value, type);
                return true;
            }
            catch (Exception ex) when (ex is InvalidCastException || ex is OverflowException)
            {
                convertedValue = null;
                return false;
            }
        }
    }
}
