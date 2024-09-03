using System.ComponentModel;
using System.Reflection;

namespace Application.Extensions
{
    public static class EnumHelper
    {
        public static Dictionary<int, string> GetAllNamesAndValues<TEnum>()
            where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(t => (int)(object)t, t => t.DescriptionAttribute()!);
        }

        public static string DescriptionAttribute<T>(this T source)
        {
            if (source == null)
            {
                throw new ApplicationException();
            }
            FieldInfo fi = source.GetType().GetField(source.ToString()!)!;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return source.ToString()!;
            }
        }

        public static string GetEnumDisplayName(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString())!;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description!;
            else
                return value.ToString();
        }

        public static string GetEnumDisplayName2(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString())!;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description!;
            else
                return value.ToString();
        }
    }
}
