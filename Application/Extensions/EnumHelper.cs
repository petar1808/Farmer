using System.ComponentModel;

namespace Application.Extensions
{
    public static class EnumHelper
    {
        public static Dictionary<int, string> GetAllNamesAndValues<TEnum>() 
            where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(t => (int)(object)t, t => t.ToString()!);
        }
    }
}
