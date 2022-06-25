using System;
using DomConsult.GlobalShared.Utilities;

namespace DomConsult.Platform.Extensions
{
    /// <summary>
    /// Klasa platformowa zawieraj¹ca funkcje rozszerzaj¹ce
    /// </summary>
    public static class PlatformExtenstions
    {
        /// <summary>
        /// Extension that allows direct conversion of an object type to a dbnullable object
        /// </summary>
        /// <param name="value"></param>
        /// <param name="var_null"></param>
        /// <returns>System.Object.</returns>
        public static object ToDBNull(this object value, object var_null)
        {
            if (value != null)
            {
                return value.Equals(var_null) ? DBNull.Value : value;
            }
            else
            {
                return value;
            }
        }
    }
}
