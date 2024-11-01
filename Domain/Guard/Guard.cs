using Domain.Exceptions;

namespace Domain.Guard
{
    public static class Guard
    {
        const string value = "Value";
        public static void AgainstEmptyString(string? value, string name = value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            throw new DomainException($"{name} cannot be null or empty.");
        }

        public static void ForValidEnum<EType>(int value, string name = value) where EType : System.Enum
        {
            if (System.Enum.IsDefined(typeof(EType), value))
            {
                return;
            }

            throw new DomainException($"{name} cannot be 0 or empty.");
        }

        public static void ForStringMaxLength(string value, int maxLength, string name = value)
        {
            AgainstEmptyString(value, name);

            if (value.Length <= maxLength)
            {
                return;
            }

            throw new DomainException($"{name} must have maximum {maxLength} symbols.");
        }

        public static void ForPositiveNumber(int number, string name = value)
        {
            if (number > 0)
            {
                return;
            }

            throw new DomainException($"{name} must be positive number.");
        }

        public static void ForStringMaxLengtAndMinLength(string value, int maxLength, int minLenght, string name = value)
        {
            AgainstEmptyString(value, name);

            if (value.Length == maxLength || value.Length == minLenght)
            {
                return;
            }

            throw new DomainException($"{name} must be between {minLenght} and {maxLength}.");
        }
    }
}
