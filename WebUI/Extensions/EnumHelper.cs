﻿using System.ComponentModel;
using System.Reflection;

namespace WebUI.Extensions
{
    public static class EnumHelper
    {
        public static string GetEnumDisplayName(this Enum value)
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
