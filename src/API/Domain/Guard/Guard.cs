using Domain.Exceptions;

namespace Domain.Guard
{
    public static class Guard
    {
        private const string DefaultName = "Value";

        public static void AgainstEmptyString(string? value, string name = DefaultName)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            throw new DomainException($"{name} cannot be null or empty.");
        }

        public static void ForValidEnum<EType>(int value, string name = DefaultName) where EType : System.Enum
        {
            if (System.Enum.IsDefined(typeof(EType), value))
            {
                return;
            }

            throw new DomainException($"{name} is not a valid value for {typeof(EType).Name}.");
        }

        public static void ForStringMaxLength(string value, int maxLength, string name = DefaultName)
        {
            AgainstEmptyString(value, name);

            if (value.Length <= maxLength)
            {
                return;
            }

            throw new DomainException($"{name} must have a maximum of {maxLength} characters.");
        }

        public static void ForPositiveNumber(int number, string name = DefaultName)
        {
            if (number > 0)
            {
                return;
            }

            throw new DomainException($"{name} must be a positive number.");
        }

        public static void ForStringMaxAndMinLength(string value, int maxLength, int minLength, string name = DefaultName)
        {
            AgainstEmptyString(value, name);

            if (value.Length >= minLength && value.Length <= maxLength)
            {
                return;
            }

            throw new DomainException($"{name} must be between {minLength} and {maxLength} characters.");
        }
    }
}
