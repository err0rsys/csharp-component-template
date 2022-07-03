using System;
using System.Collections.Generic;
using System.Text;
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

        /// <summary>
        /// Extension that allows direct conversion of a dictionary to text: key1 = value1, key2 = value2, ...
        /// </summary>
        /// <param name="dict"></param>
        /// <returns>System.String.</returns>
        public static string ToPrettyString<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            var str = new StringBuilder();
            str.Append("{");
            foreach (var pair in dict)
            {
                str.Append(String.Format(" {0}={1} ", pair.Key, pair.Value));
            }
            str.Append("}");
            return str.ToString();
        }

    }
}
