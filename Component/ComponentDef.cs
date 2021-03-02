using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomConsult.Components
{
    public static class ComponentDef
    {
        public static string MtsComName = "Component.Manager"; //STEP: put ProgId here
        public static int MtsComID = 2000412; //STEP: Register COM and put valid registration ID here

        public static int FRM_EXAMPLE1_ID = 2002083;

        //Default COM dictionary ID
        public static int MtsComDicA_ID = MtsComID; //STEP: put different default COM dictionary ID if differ from MtsComID

        //Generate constants by executing stored procedure:
        //all     : sp_devGenerateMTSCOMDIC @MTSCOMID=x, @DICPREFIX='A', @LANG='C#'
        //selected: sp_devGenerateMTSCOMDIC @MTSCOMID=x, @DICPREFIX='A', @TEXTIDIN='1,100', @LANG='C#'

        /// <summary>
        /// Text: Przykład
        /// </summary>
        public static int TXT_A00100_ID = 100;
        public static string TXT_A00100 = "Przykład";

        /// <summary>
        /// Nie można usunąć rekordu jeżeli wartość pola [Integer] jest mniejsza od {0}.
        /// </summary>
        public static int TXT_A00101_ID = 101;
        public static string TXT_A00101 = "Nie można usunąć rekordu jeżeli wartość pola [Integer] jest mniejsza od {0}.";
    }
}
