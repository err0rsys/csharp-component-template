using System;

namespace DomConsult.Components
{
    /// <summary>
    /// Definicja sta�ych wykorzystywanych w komponencie bran�owym
    /// </summary>
    public static partial class ComponentDef
    {
        /// <summary>
        /// Pe�na nazwa komponentu bran�owego
        /// </summary>
        public static string ComPlusName = "_ComNameToReplace_.Manager";
        /// <summary>
        /// Identyfikator komponentu bran�owego
        /// </summary>
        public static int ComPlusID = 0; //TODO: Zarejestruj COM-a w SDC i wpisz identyfikator

        //Forms selection based on FormType parameter
        /// <summary>
        /// Opis formularza #1
        /// </summary>
        public static int FRM_EXAMPLE1_ID = -1; //TODO: wstaw g��wny identyfikator formatki
        /// <summary>
        /// Opis formularza #2
        /// </summary>
        public static int FRM_EXAMPLE2_ID = -1;
        /// <summary>
        /// Opis formularza #3
        /// </summary>
        public static int FRM_EXAMPLE3_ID = -1;

        //Params
        /// <summary>
        /// Opis parametru wej�ciowego PAR_XXXXX
        /// </summary>
        public const string PAR_XXXXX = "XXXXX";

        //RunMethods
        /// <summary>
        /// Kr�tki opis metody RM_XXXXX
        /// </summary>
        public const string RM_XXXXX = "XXXXX";

        //ComFunctions
        /// <summary>
        /// Opis COM-funkcji CF_XXXXX
        /// </summary>
        public const int CF_XXXXX = 0;

        //uniParam
        /// <summary>
        /// Opis parametru bazodanowego uniParam_XXXXX
        /// </summary>
        public const string uniParam_XXXXX = @"XXXXX";

        //dicDictionary
        /// <summary>
        /// Opis s�ownika DIC_XXXXX_ID
        /// </summary>
        public const int DIC_XXXXX_ID = 0;

        //ComText
        #region Main Dictionary A=0

        //Generate constants by executing stored procedure:
        //all     : sp_devGenerateMTSCOMDIC @MTSCOMID=xxxxx, @DICPREFIX='A', @LANG='C#'
        //selected: sp_devGenerateMTSCOMDIC @MTSCOMID=xxxxx, @DICPREFIX='A', @TEXTIDIN='1,2', @LANG='C#'

        /// <summary>
        /// Identyfikator domy�lnego s�ownika
        /// </summary>
        public static int MtsComDicA_ID = ComPlusID;

        #endregion

        #region Additional Dictionary B=2000000

        //selected: sp_devGenerateMTSCOMDIC @MTSCOMID=200000, @DICPREFIX='A', @TEXTIDIN='300,302', @LANG='C#'

        /// <summary>
        /// Identyfikator s�ownika dodatkowego
        /// </summary>
        public static int MtsComDicB_ID = 2000000;

        /// <summary>
        /// %sB��dne parametry wej�ciowe.%s
        /// </summary>
        /// <remarks>
        /// Type:B��d, Buttons:OK, Result: B��d
        /// </remarks>
        public static int MSG_B00300_ID = 300;
        /// <summary></summary>
        public static string MSG_B00300 = "%sB��dne parametry wej�ciowe.%s";

        /// <summary>
        /// Informacje techniczne: Modu�: %s Funkcja: %s Section: %s Parametry: %s Brakuj�ce parametry: %s Kod systemowy: %s Komunikat systemowy: %s Opis: %s
        /// </summary>
        /// <remarks>
        /// Type:Informacja, Buttons:OK, Result: Informacja
        /// </remarks>
        public static int MSG_B00302_ID = 302;
        /// <summary></summary>
        public static string MSG_B00302 = "Informacje techniczne:" + Environment.NewLine +
                     "Modu�: %s" + Environment.NewLine +
                     "Funkcja: %s" + Environment.NewLine +
                     "Section: %s" + Environment.NewLine +
                     "Parametry: %s" + Environment.NewLine +
                     "Brakuj�ce parametry: %s" + Environment.NewLine +
                     "Kod systemowy: %s" + Environment.NewLine +
                     "Komunikat systemowy: %s" + Environment.NewLine +
                     "Opis: %s";

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <remarks>
        /// Static constructor is executed only Once!!!
        /// </remarks>
        static ComponentDef()
        {
            _COM_NAME_LOG = ComPlusName;
        }
    }
}
