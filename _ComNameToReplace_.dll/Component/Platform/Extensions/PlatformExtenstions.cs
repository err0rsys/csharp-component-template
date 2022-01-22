using DomConsult.GlobalShared.Utilities;

namespace DomConsult.Platform.Extensions
{
    public static class PlatformExtenstions
    {
        public static int AsInt(this object value)
        {
            return TUniVar.VarToInt(value);
        }

        public static string AsString(this object value)
        {
            return TUniVar.VarToStr(value);
        }
    }
}
