using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveControllerFromName(this string str)
        {
            return str.Replace("Controller", "");
        }
    }
}
