using DomConsult.GlobalShared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomConsult.Components.Extensions
{
    /// <summary>
    /// Klasa zawierająca funkcje rozszerzające
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Extension that allows get value from complex string parameter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <returns>System.Object.</returns>
        public static string GetParam(this string value, string paramName)
        {
            if (value != null)
            {
                return TStrParams.GetParamAsString(value, paramName);
            }
            else
            {
                return value;
            }
        }
    }
}
