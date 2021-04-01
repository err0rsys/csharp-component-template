using System;

namespace DomConsult.Platform
{
    public static class ManagerBaseDef
    {
        //sp_devGenerateMTSCOMDIC @MTSCOMID=2000000, @DICPREFIX='P', @TEXTIDIN='7,8,9,10,11,201', @LANG='C#'

        /// <summary>
        /// Platformowy s³ownik z uniwersalnymi wielojêzycznymi komunikatami i tekstami.
        /// </summary>
        public static int DIC_P2000000_ID = 2000000;
        /// <summary>
        /// Text: Wykonanie operacji nie jest mo¿liwe. Funkcjonalnoœæ jest wy³¹czona.
        /// Type: Informacja, Buttons:OK, Result: Informacja
        /// </summary>
        public static int MSG_P00007_ID = 7;
        public static string MSG_P00007 = "Wykonanie operacji nie jest mo¿liwe. Funkcjonalnoœæ jest wy³¹czona.";
        /// <summary>
        /// Text: Dane nie mog¹ zostaæ zapisane. Wyst¹li³y b³êdy walidacji:%s
        /// Type: B³¹d, Buttons:OK, Result: B³¹d
        /// </summary>
        public static int MSG_P00008_ID = 8;
        public static string MSG_P00008 = "Dane nie mog¹ zostaæ zapisane. Wyst¹li³y b³êdy walidacji:" + Environment.NewLine +
                     "%s";
        /// <summary>
        /// Text: Czy zapisaæ wprowadzone zmiany?
        /// Type: Potwierdzenie, Buttons:OKCANCEL, Result: Powtórzenie
        /// </summary>
        public static int MSG_P00009_ID = 9;
        public static string MSG_P00009 = "Czy zapisaæ wprowadzone zmiany?";
        /// <summary>
        /// Text: Nie mo¿na usun¹æ rekordu. Wyst¹pi³y b³êdy walidacji:%s
        /// Type: B³¹d, Buttons:OK, Result: B³¹d
        /// </summary>
        public static int MSG_P00010_ID = 10;
        public static string MSG_P00010 = "Nie mo¿na usun¹æ rekordu. Wyst¹pi³y b³êdy walidacji:" + Environment.NewLine +
                     "%s";
        /// <summary>
        /// Text: Czy na pewno usun¹æ rekord?
        /// Type: Potwierdzenie, Buttons:OKCANCEL, Result: Powtórzenie
        /// </summary>
        public static int MSG_P00011_ID = 11;
        public static string MSG_P00011 = "Czy na pewno usun¹æ rekord?";

        /// <summary>
        /// %sB³êdny wynik zapytania. Brak danych.%s
        /// </summary>
        /// <remarks>
        /// Type:B³¹d, Buttons:OK, Result: B³¹d
        /// </remarks>
        public static int MSG_P00201_ID = 201;
        public static string MSG_P00201 = "%sB³êdny wynik zapytania. Brak danych.%s";
    }
}
