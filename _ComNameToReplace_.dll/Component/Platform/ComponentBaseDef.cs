using System;

namespace DomConsult.Platform
{
    public static class ManagerBaseDef
    {
        //sp_devGenerateMTSCOMDIC @MTSCOMID=2000000, @DICPREFIX='P', @TEXTIDIN='7,8,9,10,11,201', @LANG='C#'

        /// <summary>
        /// Platformowy s�ownik z uniwersalnymi wieloj�zycznymi komunikatami i tekstami.
        /// </summary>
        public static int DIC_P2000000_ID = 2000000;
        /// <summary>
        /// Text: Wykonanie operacji nie jest mo�liwe. Funkcjonalno�� jest wy��czona.
        /// Type: Informacja, Buttons:OK, Result: Informacja
        /// </summary>
        public static int MSG_P00007_ID = 7;
        public static string MSG_P00007 = "Wykonanie operacji nie jest mo�liwe. Funkcjonalno�� jest wy��czona.";
        /// <summary>
        /// Text: Dane nie mog� zosta� zapisane. Wyst�li�y b��dy walidacji:%s
        /// Type: B��d, Buttons:OK, Result: B��d
        /// </summary>
        public static int MSG_P00008_ID = 8;
        public static string MSG_P00008 = "Dane nie mog� zosta� zapisane. Wyst�li�y b��dy walidacji:" + Environment.NewLine +
                     "%s";
        /// <summary>
        /// Text: Czy zapisa� wprowadzone zmiany?
        /// Type: Potwierdzenie, Buttons:OKCANCEL, Result: Powt�rzenie
        /// </summary>
        public static int MSG_P00009_ID = 9;
        public static string MSG_P00009 = "Czy zapisa� wprowadzone zmiany?";
        /// <summary>
        /// Text: Nie mo�na usun�� rekordu. Wyst�pi�y b��dy walidacji:%s
        /// Type: B��d, Buttons:OK, Result: B��d
        /// </summary>
        public static int MSG_P00010_ID = 10;
        public static string MSG_P00010 = "Nie mo�na usun�� rekordu. Wyst�pi�y b��dy walidacji:" + Environment.NewLine +
                     "%s";
        /// <summary>
        /// Text: Czy na pewno usun�� rekord?
        /// Type: Potwierdzenie, Buttons:OKCANCEL, Result: Powt�rzenie
        /// </summary>
        public static int MSG_P00011_ID = 11;
        public static string MSG_P00011 = "Czy na pewno usun�� rekord?";

        /// <summary>
        /// %sB��dny wynik zapytania. Brak danych.%s
        /// </summary>
        /// <remarks>
        /// Type:B��d, Buttons:OK, Result: B��d
        /// </remarks>
        public static int MSG_P00201_ID = 201;
        public static string MSG_P00201 = "%sB��dny wynik zapytania. Brak danych.%s";
    }
}
