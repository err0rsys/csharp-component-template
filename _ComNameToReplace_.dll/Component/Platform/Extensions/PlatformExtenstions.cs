using System;
using DomConsult.GlobalShared.Utilities;

namespace DomConsult.Platform.Extensions
{
    /// <summary>
    /// Klasa platformowa zawierająca funkcje rozszerzające
    /// </summary>
    public static class PlatformExtenstions
    {
        /// <summary>
        /// Rozszerzenie typu object pozwalające na bezpośrednią konwersję typu object do int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int AsInt(this object value)
        {
            return TUniVar.VarToInt(value);
        }

        /// <summary>
        /// Rozszerzenie typu object pozwalające na bezpośrednią konwersję typu object do string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AsString(this object value)
        {
            return TUniVar.VarToStr(value);
        }
    }
}
