using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Reflection;
using System.ComponentModel;

#if DEBUG
using System.Diagnostics;
#endif

namespace DomConsult.GlobalShared.Utilities
{
    /// <summary>
    /// Class ComUtils.
    /// </summary>
    public static class ComUtils
    {
        /// <summary>
        /// Delegate WMKHandler
        /// </summary>
        /// <param name="WMK">The WMK.</param>
        public delegate void WMKHandler(object WMK);

        /// <summary>
        /// The is single CPU machine
        /// </summary>
        private static readonly bool IsSingleCpuMachine = (Environment.ProcessorCount == 1);
        /// <summary>
        /// The DBC lock object
        /// </summary>
        private static readonly Object dbcLockObj = new Object();

        /// <summary>
        /// Switches to thread.
        /// </summary>
        [DllImport("kernel32", ExactSpelling = true)]
        private static extern void SwitchToThread();

        #region ClassID & ProgID constants and registry keys
        /// <summary>
        /// The cs dbcom unique identifier
        /// </summary>
        public const string CS_DBCOM_GUID = "{8EC71C07-3E75-11D3-AD64-005500E39587}";
        /// <summary>
        /// The cs trans unique identifier
        /// </summary>
        public const string CS_TRANS_GUID = "{6A2813F3-37B0-11D5-86E0-00105A72C161}";
        /// <summary>
        /// The cs locker unique identifier
        /// </summary>
        public const string CS_LOCKER_GUID = "{11D93C4F-67A8-4A65-9522-396A71393197}";

        /// <summary>
        /// The cs system unique identifier
        /// </summary>
        public const string CS_SYSTEM_GUID = "{FB56EAFA-743B-4E94-B711-7BB0462C2532}";
        /// <summary>
        /// The cs systemtrans unique identifier
        /// </summary>
        public const string CS_SYSTEMTRANS_GUID = "{124CF1F0-AB48-454E-91A8-61BE6EB84F4E}";
        /// <summary>
        /// The cs systemlocker unique identifier
        /// </summary>
        public const string CS_SYSTEMLOCKER_GUID = "{D02C49B3-95BF-46B7-9329-D49DE16182EF}";

        /// <summary>
        /// The cs reader unique identifier
        /// </summary>
        public const string CS_READER_GUID = "{6630571A-3668-42D0-9AFD-7711382CA53B}";

        /// <summary>
        /// The GateKeeperEx.Manager classID
        /// </summary>
        public const string CS_GKEX_GUID = "{57EBF9C7-7732-43EE-91A4-4D9D93CF5C7D}";
        /// <summary>
        /// The GateKeeperEx.Manager progID
        /// </summary>
        public const string CS_GKEX_ProgID = "GateKeeperEx.Manager";

        /// <summary>
        /// The cs sysreg unique identifier
        /// </summary>
        public const string CS_SYSREG_GUID = "{AB4079B8-75E3-11D5-9B70-00104B07B6CB}";
        /// <summary>
        /// The cs reg srvpath
        /// </summary>
        public const string CS_REG_SRVPATH = "{63AFD5B1-49BA-11D5-9AA5-00105A72C191}";
        /// <summary>
        /// The cs reg install
        /// </summary>
        public const string CS_REG_INSTALL = "{63AFD5B1-49BA-11D5-9AA5-00105A72C193}";
        /// <summary>
        /// The cs regkey root
        /// </summary>
        public const string CS_REGKEY_ROOT = "SOFTWARE";
        /// <summary>
        /// The cs regkey altr
        /// </summary>
        public const string CS_REGKEY_ALTR = "SOFTWARE\\DOMCONSULT\\GRANIT";
        /// <summary>
        /// The cs stdcom unique identifier
        /// </summary>
        public const string CS_STDCOM_GUID = "{78C06189-C9A3-11D3-86BA-00105A72C191}";
        /// <summary>
        /// The cs stdcomw unique identifier
        /// </summary>
        public const string CS_STDCOMW_GUID = "{15E97569-0BA4-4882-A79B-7B5D9B90F3B5}";
        #endregion

        /// <summary>
        /// The DBC class
        /// </summary>
        public static string DBC_CLASS = CS_DBCOM_GUID;
        /// <summary>
        /// The DBC trans class
        /// </summary>
        public static string DBC_TRANS_CLASS = CS_TRANS_GUID;
        /// <summary>
        /// The DBC locker class
        /// </summary>
        public static string DBC_LOCKER_CLASS = CS_LOCKER_GUID;

        /// <summary>
        /// The error message sqlcommand
        /// </summary>
        public static string ERR_MESSAGE_SQLCOMMAND = "Unexpected sql Command error.";

        /// <summary>
        /// Checks if locker object is properly initialized. If not, exception is rised.
        /// </summary>
        /// <param name="locker"></param>
        private static void CheckLocker(ComWrapper locker)
        {
            if (locker == null)
                throw new InvalidEnumArgumentException("Empty parameter value 'locker'!");
            else if (!locker.ClassID.Equals(DBC_LOCKER_CLASS))
                throw new InvalidEnumArgumentException("Wrong parameter value 'locker'!");
            else if (!locker.Connected)
                throw new InvalidEnumArgumentException("Wrong parameter value 'locker'! Not Connected.");
        }

        /// <summary>
        /// Enum PECDataTypeFlags
        /// </summary>
        public enum PECDataTypeFlags
        {
            /// <summary>
            /// The cp col visible
            /// </summary>
            cpColVisible = 1,   // Kolumna ma byæ widoczna dla klienta
            /// <summary>
            /// The cp object identifier
            /// </summary>
            cpObjectId = 2,     // Kolumna przechowuje wartoœæ klucza g³ównego dla rekordu (tylko jedna kolumna w paczce) np: '/Rok=2008/Miesiac=1/CreditId=123'
            /// <summary>
            /// The cp object type identifier
            /// </summary>
            cpObjectTypeId = 4, // Kolumna przechowuje wartoœæ okreœlaj¹ca ObjectTypeId dla bie¿¹cego rekordu (tylko jedna kolumna w paczce)
            /// <summary>
            /// The cp col first
            /// </summary>
            cpColFirst = 32,    // Kolumna widoczna która ma byæ pierwsz¹ kolumn¹ w liœcie prezentowanej u¿ytkownikowi (opcjonalnie)

            /// <summary>
            /// The cp date
            /// </summary>
            cpDate = 0x0100,
            /// <summary>
            /// The cp integer
            /// </summary>
            cpInteger = 0x0200,
            /// <summary>
            /// The cp float
            /// </summary>
            cpFloat = 0x0400,
            /// <summary>
            /// The cp string
            /// </summary>
            cpString = 0x0800,
            /// <summary>
            /// The cp date time
            /// </summary>
            cpDateTime = 0x1000,
            /// <summary>
            /// The cp boolean
            /// </summary>
            cpBoolean = 0x2000,
            /// <summary>
            /// The cp currency
            /// </summary>
            cpCurrency = 0x4000,

            /// <summary>
            /// The CPV integer
            /// </summary>
            cpvInteger = cpInteger + cpColVisible,
            /// <summary>
            /// The CPV float
            /// </summary>
            cpvFloat = cpFloat + cpColVisible,
            /// <summary>
            /// The CPV string
            /// </summary>
            cpvString = cpString + cpColVisible,
            /// <summary>
            /// The CPV date time
            /// </summary>
            cpvDateTime = cpDateTime + cpColVisible,
            /// <summary>
            /// The CPV date
            /// </summary>
            cpvDate = cpDate + cpColVisible,
            /// <summary>
            /// The CPV boolean
            /// </summary>
            cpvBoolean = cpBoolean + cpColVisible,
            /// <summary>
            /// The CPV currency
            /// </summary>
            cpvCurrency = cpCurrency + cpColVisible,

            /// <summary>
            /// The cp col key
            /// </summary>
            cpColKey = cpObjectId,
            /// <summary>
            /// The cp col object type identifier
            /// </summary>
            cpColObjectTypeId = cpObjectTypeId
        }

        /// <summary>
        /// Uses the DBC system.
        /// </summary>
        public static void UseDBCSystem()
        {
            //UWAGA: To ustawienie ma wp³yw na wszystkie instancje obiektów danego komponentu.
            //U¿ywaæ tylko je¿eli komponent danego typu ma ci¹gle u¿ywaæ DBCom-a SYSTEM!!!
            //Prze³¹czanie w locie miêdzy klasami DBC obecnie nie jest mo¿liwe bo C# nie pozwala dziedziczyæ po ststaycznych klasach.
            //Trzeba by wszystko przerobiæ na klasy zwyk³e co niesie dalsze konsekwencje :-(.
            //w delphi mogliœmy to zrobiæ poprzez klasê statyczn¹ TDBCSystem = class(TDBC), która nadpisywa³a metodê desyduj¹ca o tym,
            //jak¹ klasê DBCom-a u¿ywaæ.

            //Mam nadziejê, ¿e COM+ nie bêdzie wisia³ na tym Lock-u
            lock(dbcLockObj)
            {
                if (!ComUtils.DBC_CLASS.Equals(ComUtils.CS_SYSTEM_GUID, StringComparison.CurrentCultureIgnoreCase))
                {
                    ComUtils.DBC_CLASS = ComUtils.CS_SYSTEM_GUID;
                    ComUtils.DBC_TRANS_CLASS = ComUtils.CS_SYSTEMTRANS_GUID;
                    ComUtils.DBC_LOCKER_CLASS = ComUtils.CS_SYSTEMLOCKER_GUID;
                }
            }
        }

        /// <summary>
        /// Stalls the thread.
        /// </summary>
        public static void StallThread()
        {
            if (IsSingleCpuMachine)
                SwitchToThread();
            else
                Thread.SpinWait(1);
        }

        /// <summary>
        /// OLEs the check.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <exception cref="Exception">OleCheck error, result: " + result</exception>
        public static void OleCheck(int result)
        {
            if (result < 0)
            {
                throw new Exception("OleCheck error, result: " + result);
            }
        }

        /// <summary>
        /// Gets multilanguage text for given poarameters
        /// </summary>
        /// <param name="comId">Id of dictionary</param>
        /// <param name="langId">Id of language</param>
        /// <param name="textId">Id of text</param>
        /// <returns></returns>
        public static object GetText(int comId, int langId, int textId)
        {
            object paramValue=null;

            ComWrapper langManager = ComUtils.CreateComFromProgID("Language.Manager", "LOGON");

            using (langManager)
            {
                if (langManager.Connected)
                {
                    object[] method_params = new object[] { comId, langId, textId };
                    paramValue = langManager.InvokeMethod("GetText", method_params, new bool[] { false, false, false });
                }
            }

            return paramValue;
        }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="paramString">The parameter string.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string GetParam(string paramString, string paramName, string defaultValue)
        {
            Dictionary<string, string> ac = DecodeInputString(paramString, "/");

            if (ac.ContainsKey(paramName))
                return Convert.ToString(ac[paramName]);
            else
                return defaultValue;
        }
        /* MS 27.11.2015 Metody GetRegParam nie wolno wywo³ywaæ bez AccessCode-a !!
        public static string GetRegParam(string paramName, string defaultValue)
        {
            return GetRegParam(paramName, defaultValue, string.Empty);
        }
        */
        /// <summary>
        /// Gets the reg parameter.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.String.</returns>
        public static string GetRegParam(string paramName, string defaultValue, string accessCode)
        {
            string paramValue = defaultValue;
            ComWrapper sysreg;

            if (ComWrapper.ComRemoteMode)
            {
                sysreg = ComUtils.CreateComInStandardCom("Sysreg.Registry", accessCode, string.Empty);
            }
            else
            {
                //Kiedyœ mo¿na zrobiæ bezpoœrednio ale musia³aby siê rozpropagowaæ wersja z metod¹ interfejsow¹ RunMethodNet
                //ComWrapper sysreg = CreateRemoteCom(new Guid(CS_SYSREG_GUID), accessCode);
                sysreg = ComUtils.CreateComInStandardCom("Sysreg.Registry", accessCode, string.Empty);
            }

            using (sysreg)
            {
                if (sysreg.Connected)
                {
                    //wo³anie GetValue przez runmethode sysrega jest specyficzne dlatego trzeba by³o stworzyæ indywidualne wo³anie 
                    object[] rm_params = new object[] { "GETVALUE", paramName, null };
                    object res = sysreg.InvokeMethod("RunMethodNet", rm_params, new bool[] { false, false, true });

                    int result = CheckError(res, -1);
                    if (result >= 0)
                    {
                        paramValue = Convert.ToString(rm_params[2]);
                    }
                }
            }

            return paramValue;
        }

        /// <summary>
        /// Gets the uni parameter.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string GetUniParam(string accessCode, string paramName, string defaultValue)
        {
            int res = GetPacket(accessCode,
                                $"SELECT paramValue FROM uniParam WHERE paramName = '{paramName}'",
                                -1,
                                -1,
                                out object[,] packet);

            if (res > 0)
                return packet[1, 0].ToString();
            else
                return defaultValue;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.String.</returns>
        public static string GetConnectionString(string accessCode)
        {
            using (ComWrapper dbc = ComUtils.CreateRemoteCom(new Guid(DBC_CLASS), accessCode, string.Empty, true))
            {
                object[] _params = new object[2];
                _params[0] = -1;
                _params[1] = null;
                int res = (int)dbc.InvokeMethod("GetInfoNet", _params, new bool[] { false, true });
                OleCheck(res);
                return Convert.ToString(_params[1]);
            }
        }

        /// <summary>
        /// Assigneds the specified a object.
        /// </summary>
        /// <param name="anObject">a object.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Assigned(object anObject)
        {
            return anObject != null;
        }

        /// <summary>
        /// Formats the array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>System.String.</returns>
        public static string FormatArray(ArrayList array)
        {
            return FormatArray(array, ", ");
        }

        /// <summary>
        /// Formats the array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>System.String.</returns>
        public static string FormatArray(ArrayList array, string separator)
        {
            StringBuilder sb = new StringBuilder();

            foreach (object o in array)
            {
                if (sb.Length > 1)
                {
                    sb.Append(separator);
                }
                sb.Append(o.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Checks the error.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="resultIfNull">The result if null.</param>
        /// <returns>System.Int32.</returns>
        public static int CheckError(object result, int resultIfNull = 0)
        {
            return TUniVar.VarToInt(result, resultIfNull, false);
        }

        /// <summary>
        /// Checks the error with throw.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="Exception"></exception>
        public static void CheckErrorWithThrow(object result, string message)
        {
            if (TUniVar.VarToInt(result, 0) < 0)
                throw new Exception(message);
        }

        /// <summary>
        /// Gets the time out parameter value.
        /// </summary>
        /// <param name="timeOut">The time out.</param>
        /// <param name="addSeparator">if set to <c>true</c> [add separator].</param>
        /// <returns>System.String.</returns>
        public static string GetTimeOutParamValue(int timeOut, bool addSeparator)
        {
            return timeOut == -1 ? "" : ((addSeparator ? "&" : "") + $"parameters=/timeout={timeOut}");
        }

        /// <summary>
        /// Creates COM by progID
        /// </summary>
        /// <param name="progID"></param>
        /// <param name="accessCode"></param>
        /// <returns></returns>
        public static ComWrapper CreateComFromProgID(string progID, string accessCode)
        {
            return CreateComFromProgID(progID, accessCode, true);
        }

        /// <summary>
        /// Creates COM by classID.
        /// </summary>
        /// <param name="classID">The unique identifier.</param>
        /// <param name="accessCode">The access code.</param>
        /// <returns>ComWrapper.</returns>
        public static ComWrapper CreateCom(string classID, string accessCode)
        {
            return CreateCom(classID, accessCode, true);
        }

        /// <summary>
        /// Creates COM by classID
        /// </summary>
        /// <param name="classID">The unique identifier.</param>
        /// <param name="accessCode">The access code.</param>
        /// <param name="assignAccessCode">if set to <c>true</c> [assign access code].</param>
        /// <returns>ComWrapper.</returns>
        public static ComWrapper CreateCom(string classID, string accessCode, bool assignAccessCode)
        {
            ComWrapper ComWrapper = new ComWrapper
            {
                AccessCode = accessCode
            };

            /*
             * MS 03.12.2015 
             * Z jakiegoœ powodu wo³anie Connect(string className) zwraca b³¹d:
             * Invalid class string Exception from HRESULT: 0x800401F3 (CO_E_CLASSSTRING))
             * Ma to zwi¹zek z tworzeniem typu poprzez wywo³anie Type.GetTypeFromProgID(className, true);
             * Wyj¹tek nie jest zg³aszany jeœli typ jest tworzony przez Type.GetTypeFromCLSID st¹d te¿ ten fix z podaniem Guid-a.
             */
            if (ComWrapper.Connect(new Guid(classID)))
            {
                if (assignAccessCode)
                {
                    object[] arguments = new object[1];
                    arguments[0] = accessCode;
                    object res = ComWrapper.InvokeMethod("AssignAccessCode", arguments, new bool[] { false });
                    if (CheckError(res, -1) >= 0)
                        return ComWrapper;
                }
                else
                {
                    return ComWrapper;
                }
            }

            return null;
        }

        /// <summary>
        /// Creates COM by progID.
        /// </summary>
        /// <param name="progID"></param>
        /// <param name="accessCode"></param>
        /// <param name="assignAccessCode"></param>
        /// <returns></returns>
        public static ComWrapper CreateComFromProgID(string progID, string accessCode, bool assignAccessCode)
        {
            ComWrapper ComWrapper = new ComWrapper
            {
                AccessCode = accessCode
            };

            /*
             * MS 03.12.2015 
             * Z jakiegoœ powodu wo³anie Connect(string className) zwraca b³¹d:
             * Invalid class string Exception from HRESULT: 0x800401F3 (CO_E_CLASSSTRING))
             * Ma to zwi¹zek z tworzeniem typu poprzez wywo³anie Type.GetTypeFromProgID(className, true);
             * Wyj¹tek nie jest zg³aszany jeœli typ jest tworzony przez Type.GetTypeFromCLSID st¹d te¿ ten fix z podaniem Guid-a.
             */
            if (ComWrapper.Connect(progID))
            {
                if (assignAccessCode)
                {
                    object[] arguments = new object[1];
                    arguments[0] = accessCode;
                    object res = ComWrapper.InvokeMethod("AssignAccessCode", arguments, new bool[] { false });
                    if (CheckError(res, -1) >= 0)
                        return ComWrapper;
                }
                else
                {
                    return ComWrapper;
                }
            }
            return null;
        }

        /// <summary>
        /// Creates the COM in StandardCOM.
        /// </summary>
        /// <param name="comName">Name of the COM.</param>
        /// <param name="accessCode">The access code.</param>
        /// <returns>ComWrapper.</returns>
        public static ComWrapper CreateComInStandardCom(string comName, string accessCode)
        {
            if (ComWrapper.ComRemoteMode)
            {
                return CreateComInStandardCom(comName, accessCode, GetParam(accessCode, "CSN", string.Empty));
            }
            else
            {
                return CreateComInStandardCom(comName, accessCode, string.Empty);
            }
        }

        /// <summary>
        /// Creates the COM in StandardCOM.
        /// </summary>
        /// <param name="comName">Name of the COM.</param>
        /// <param name="accessCode">The access code.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <returns>ComWrapper.</returns>
        /// <exception cref="Exception">Error creating OLE Object \"StandardCom\"</exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="Exception"></exception>
        public static ComWrapper CreateComInStandardCom(string comName, string accessCode, string serverName)
        {
            /*
             * MS 07.12.2016
               W celu umo¿liwienia debugowania standardcom-a nale¿y prze³¹czyæ siê na standardComW
               Wiêcej informacji udziela Micha³ Piotrowski
             */
            ComWrapper stdCom;

            if (ComWrapper.ComRemoteMode)
            {
                stdCom = CreateRemoteCom(new Guid(CS_STDCOMW_GUID), accessCode, serverName, false);
            }
            else
            {
                stdCom = CreateRemoteCom(new Guid(CS_STDCOM_GUID), accessCode, serverName, false);
            }

            if (stdCom == null)
            {
                throw new Exception("Error creating OLE Object \"StandardCom\"");
            }
            object[] args = new object[] { comName };
            int _comRes = (int)stdCom.InvokeMethod("AssignComName", args, new bool[] { false });
            if (_comRes < 0)
                throw new Exception($"Error creating OLE Object \"{comName}\"");

            args[0] = accessCode;
            _comRes = (int)stdCom.InvokeMethod("AssignAccessCode", args, new bool[] { false });
            if (_comRes < 0)
                throw new Exception($"AccessCode assign error on OLE Object \"{comName}\"");

            return stdCom;
        }

        /// <summary>
        /// Funkcja tworzy zdalnie obiekt COM - adres serwera zostanie pobrany z AccessCode'a
        /// </summary>
        /// <param name="classId">CLSID tworzonego COMa</param>
        /// <param name="accessCode">AccessCode jaki zostanie do niego przypisany</param>
        /// <returns>Utworzony obiekt COM</returns>
        public static ComWrapper CreateRemoteCom(Guid classId, string accessCode)
        {
            return CreateRemoteCom(classId, accessCode, string.Empty, true);
        }

        /// <summary>
        /// Funkcja tworzy zdalnie obiekt COM
        /// </summary>
        /// <param name="classId">CLSID tworzonego COMa</param>
        /// <param name="accessCode">AccessCode jaki zostanie do niego przypisany</param>
        /// <param name="serverName">Nazwa serwera komponentow, gdzie jest zainstalowany komponent. Jesli zostanie podana pusta nazwa
        /// nazwa zostanie pobrana z AccessCode'a</param>
        /// <returns>Utworzony obiekt COM</returns>
        public static ComWrapper CreateRemoteCom(Guid classId, string accessCode, string serverName)
        {
            return CreateRemoteCom(classId, accessCode, serverName, true);
        }

        /// <summary>
        /// Funkcja tworzy zdalnie obiekt COM
        /// </summary>
        /// <param name="classID">The class identifier.</param>
        /// <param name="accessCode">AccessCode jaki zostanie do niego przypisany</param>
        /// <param name="serverName">Nazwa serwera komponentow, gdzie jest zainstalowany komponent. Jesli zostanie podana pusta nazwa to zostanie pobrana z AccessCode'a</param>
        /// <param name="assignAccessCode">Flaga okreslajaca czy ma byc przypisany AccessCode - przydatne podczas tunelowania wywolac przez inne komponenty</param>
        /// <returns>Utworzony obiekt COM</returns>
        public static ComWrapper CreateRemoteCom(Guid classID, string accessCode, string serverName, bool assignAccessCode)
        {
            ComWrapper ComWrapper = new ComWrapper();

            if (accessCode.ToUpper() != "LOGON")
                ComWrapper.AccessCode = accessCode;

            string _serverName = (string.IsNullOrEmpty(serverName)) ? ComWrapper.ServerName : serverName;

            if (ComWrapper.ConnectRemote(classID, _serverName))
            {
                if (assignAccessCode)
                {
                    object[] arguments = new object[1];
                    arguments[0] = accessCode;
                    object res = ComWrapper.InvokeMethod("AssignAccessCode", arguments, new bool[] { false });
                    if (CheckError(res, -1) >= 0)
                        return ComWrapper;
                }
                else
                {
                    return ComWrapper;
                }
            }
            return null;
        }

        /// <summary>
        /// Runs the method.
        /// </summary>
        /// <param name="comWrapper">The COM wrapper.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="params">The parameters.</param>
        /// <returns>System.Int32.</returns>
        public static int RunMethod(ComWrapper comWrapper, string methodName, ref object[] @params)
        {
            object[] rm_params = new object[] { methodName, @params };
            object res = comWrapper.InvokeMethod("RunMethod", rm_params, new bool[] { false, true });
            @params = (object[])rm_params[1];
            int result = CheckError(res, -1);
            return result;
        }

        /// <summary>
        /// Runs the method net.
        /// </summary>
        /// <param name="comWrapper">The COM wrapper.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="params">The parameters.</param>
        /// <returns>System.Int32.</returns>
        public static int RunMethodNet(ComWrapper comWrapper, string methodName, ref object[] @params)
        {
            object[] rm_params = new object[] { methodName, @params, null };
            object res = comWrapper.InvokeMethod("RunMethodNet", rm_params, new bool[] { false, false, true });
            @params = (object[])rm_params[2];
            int result = CheckError(res, -1);
            return result;
        }

        /// <summary>
        /// Gets the packet.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="timeOut">The timeout.</param>
        /// <param name="packet">The packet.</param>
        /// <param name="hWMK">The h WMK.</param>
        /// <returns>System.Int32.</returns>
        public static int GetPacket(string accessCode, string sql, int transId, int timeOut, out object[,] packet, WMKHandler hWMK = null)
        {
            if (ComWrapper.ComRemoteMode)
            {
                return GetPacket(accessCode, GetParam(accessCode, "CSN", string.Empty), sql, transId, timeOut, out packet, hWMK);
            }
            else
            {
                return GetPacket(accessCode, string.Empty, sql, transId, timeOut, out packet, hWMK);
            }
        }

        /// <summary>
        /// Gets the packet.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="timeOut">The timeout.</param>
        /// <param name="packet">The packet.</param>
        /// <param name="hWMK">The h WMK.</param>
        /// <returns>System.Int32.</returns>
        public static int GetPacket(string accessCode, string serverName, string sql, int transId, int timeOut, out object[,] packet, WMKHandler hWMK = null)
        {
            int res = -1;
            packet = null;

            using (ComWrapper comWrapper = CreateRemoteCom(new Guid(DBC_CLASS), accessCode, serverName))
            {
                if (Assigned(comWrapper))
                {
                    res = GetPacket(comWrapper, sql, transId, timeOut, out packet);

                    if ((hWMK != null) && (res < 0))
                    {
                        comWrapper.GetLastErrorDescription(out object wmk);
                        if (wmk != null)
                            hWMK(wmk);
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Gets the packet.
        /// </summary>
        /// <param name="dbc">The database COM.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="timeOut">The timeout.</param>
        /// <param name="packet">The packet.</param>
        /// <returns>System.Int32.</returns>
        public static int GetPacket(ComWrapper dbc, string sql, int timeOut, out object[,] packet)
        {
            return GetPacket(dbc, sql, dbc.TransactionObject.Id, timeOut, out packet);
        }

        /// <summary>
        /// Gets the packet.
        /// </summary>
        /// <param name="dbc">The database COM.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="timeOut">The timeout.</param>
        /// <param name="packet">The packet.</param>
        /// <returns>System.Int32.</returns>
        public static int GetPacket(ComWrapper dbc, string sql, int transId, int timeOut, out object[,] packet)
        {
            object res = null;
            packet = null;

            if (Assigned(dbc))
            {
                object[] arguments = new object[1];
                arguments[0] = sql;
                res = dbc.InvokeMethod("SQL", arguments, new bool[] { false });

                if (CheckError(res, -1) >= 0)
                {
                    arguments[0] = -1;
                    res = dbc.InvokeMethod("PacketSize", arguments, new bool[] { false });

                    if (CheckError(res, -1) >= 0)
                    {
                        if (transId == -1)
                            arguments[0] = "List" + GetTimeOutParamValue(timeOut, true);
                        else
                            arguments[0] = $"List&Transaction={transId}" + GetTimeOutParamValue(timeOut, true);

                        res = dbc.InvokeMethod("OpenA", arguments, new bool[] { false });

                        if (CheckError(res, -1) >= 0)
                        {
                            arguments = new object[2];
                            arguments[0] = -1;
                            res = dbc.InvokeMethod("GetPacketNumber", arguments, new bool[] { false, true });
                            if (CheckError(res, -1) >= 0)
                            {
                                packet = (object[,])arguments[1];
                            }
                        }
                    }
                }
            }

            return CheckError(res, -1);
        }
                
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="timeOut">The timeout.</param>
        /// <param name="hWMK">The h WMK.</param>
        /// <returns>System.Int32.</returns>
        public static int ExecuteCommand(string accessCode, string sql, int transId, int timeOut, WMKHandler hWMK = null)
        {
            return ExecuteCommand(accessCode, sql, transId, timeOut, false, hWMK);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="timeOut">The timeout.</param>
        /// <param name="noRigtsSQL">if set to <c>true</c> [no rigts SQL].</param>
        /// <param name="hWMK">The h WMK.</param>
        /// <returns>System.Int32.</returns>
        public static int ExecuteCommand(string accessCode, string sql, int transId, int timeOut, bool noRigtsSQL, WMKHandler hWMK = null)
        {
            int res = -1;

            using (ComWrapper comWrapper = CreateRemoteCom(new Guid(DBC_CLASS), accessCode, string.Empty))
            {
                if (Assigned(comWrapper))
                {
                    res = ExecuteCommand(comWrapper, sql, transId, timeOut, noRigtsSQL);

                    if ((hWMK != null) && (res < 0))
                    {
                        comWrapper.GetLastErrorDescription(out object wmk);
                        if (wmk != null)
                            hWMK(wmk);
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="dbc">The database COM instance.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="timeOut">The timeout.</param>
        /// <returns>System.Int32.</returns>
        public static int ExecuteCommand(ComWrapper dbc, string sql, int transId, int timeOut)
        {
            return ExecuteCommand(dbc, sql, transId, timeOut, false);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="dbc">The database COM instance.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>System.Int32.</returns>
        public static int ExecuteCommand(ComWrapper dbc, string sql, int timeout)
        {
            if (dbc.TransactionObject == null)
            {
                return ExecuteCommand(dbc, sql, -1, timeout);
            }
            else
            {
                return ExecuteCommand(dbc, sql, dbc.TransactionObject.Id, timeout);
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="dbc">The database COM instance.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="timeOut">The timeout.</param>
        /// <param name="noRigtsSQL">if set to <c>true</c> [no rigts SQL].</param>
        /// <returns>System.Int32.</returns>
        public static int ExecuteCommand(ComWrapper dbc, string sql, int transId, int timeOut, bool noRigtsSQL)
        {
            object res = -1;
            object[] arguments = new object[1];

            if (Assigned(dbc))
            {
                arguments[0] = sql;
                string _method = (noRigtsSQL) ? "NoRightsSQL" : "SQL";
                res = dbc.InvokeMethod(_method, arguments, new bool[] { false });

                if (CheckError(res, -1) >= 0)
                {
                    if (transId == -1)
                        arguments[0] = "Execute" + GetTimeOutParamValue(timeOut, true);
                    else
                        arguments[0] = $"Execute&Transaction={transId}" + GetTimeOutParamValue(timeOut, true);

                    res = dbc.InvokeMethod("OpenA", arguments, new bool[] { false });
                }

                RecordClose(dbc);
            }

            return CheckError(res, -1);
        }

        /// <summary>
        /// Locks records in given table
        /// </summary>
        /// <param name="locker"></param>
        /// <param name="lockInfo"></param>
        /// <param name="transId"></param>
        /// <returns></returns>
        public static int DBLock(ComWrapper locker, object[,] lockInfo, int transId = -1)
        {
            //ComWrapper comWrapper = new ComWrapper()
            //comWrapper.ConnectRemote(new Guid(DBC_LOCKER_CLASS), ComWrapper.ServerName)

            if (!TUniVar.VarIsArray(lockInfo))
                throw new InvalidEnumArgumentException("Wrong parameter value 'lockInfo'!");
            else 
                CheckLocker(locker);

            //LockA(TransactionId: Integer; var LockInfo, Options: OleVariant): Integer; safecall;
            object[] arguments = new object[3];
            arguments[0] = transId;
            arguments[1] = lockInfo;
            int res = CheckError(locker.InvokeMethod("LockA", arguments, new bool[] { false, true, true }), -1);

            return res;
        }

        /// <summary>
        /// Locks records in given table
        /// </summary>
        /// <param name="locker"></param>
        /// <param name="tableName"></param>
        /// <param name="RecordId"></param>
        /// <param name="transId"></param>
        /// <returns></returns>
        public static int DBLock(ComWrapper locker, string tableName, string RecordId, int transId = -1)
        {
            //ComWrapper comWrapper = new ComWrapper()
            //comWrapper.ConnectRemote(new Guid(DBC_LOCKER_CLASS), ComWrapper.ServerName)

            CheckLocker(locker);

            object[,] lockInfo = new object[,]
                {
                    { "TableName", "RecordId", "OwnerState" },
                    { tableName, RecordId, 0 },
                    { PECDataTypeFlags.cpString, PECDataTypeFlags.cpInteger, PECDataTypeFlags.cpInteger }
                };

            //LockA(TransactionId: Integer; var LockInfo, Options: OleVariant): Integer; safecall;
            object[] arguments = new object[3];
            arguments[0] = transId;
            arguments[1] = lockInfo;
            CheckError(locker.InvokeMethod("LockA", arguments, new bool[] { false, true, true }), -1);

            return TUniVar.VarToInt(arguments[2]);
        }

        /// <summary>
        /// Unlocks previously locked records.
        /// </summary>
        /// <param name="locker"></param>
        /// <param name="lockInfo"></param>
        /// <param name="transId"></param>
        /// <returns></returns>
        public static int DBUnlock(ComWrapper locker, object[,] lockInfo, int transId = -1)
        {
            //ComWrapper comWrapper = new ComWrapper()
            //comWrapper.ConnectRemote(new Guid(DBC_LOCKER_CLASS), ComWrapper.ServerName)

            if (!TUniVar.VarIsArray(lockInfo))
                throw new InvalidEnumArgumentException("Wrong parameter value 'lockInfo'!");
            else 
                CheckLocker(locker);

            //LockA(TransactionId: Integer; var LockInfo, Options: OleVariant): Integer; safecall;
            object[] arguments = new object[3];
            arguments[0] = transId;
            arguments[1] = lockInfo;
            int res = CheckError(locker.InvokeMethod("UnLockA", arguments, new bool[] { false, true, true }), -1);

            return res;
        }

        /// <summary>
        /// Unlocks previously locked records.
        /// </summary>
        /// <param name="locker"></param>
        /// <param name="tableName"></param>
        /// <param name="recordId"></param>
        /// <param name="transId"></param>
        /// <returns></returns>
        public static int DBUnlock(ComWrapper locker, string tableName, string recordId, int transId = -1)
        {
            //ComWrapper comWrapper = new ComWrapper()
            //comWrapper.ConnectRemote(new Guid(DBC_LOCKER_CLASS), ComWrapper.ServerName)

            CheckLocker(locker);

            object[,] lockInfo = new object[,]
                {
                    { "TableName", "RecordId", "OwnerState" },
                    { tableName, recordId, 0 },
                    { PECDataTypeFlags.cpString, PECDataTypeFlags.cpInteger, PECDataTypeFlags.cpInteger }
                };

            //LockA(TransactionId: Integer; var LockInfo, Options: OleVariant): Integer; safecall;
            object[] arguments = new object[3];
            arguments[0] = transId;
            arguments[1] = lockInfo;
            CheckError(locker.InvokeMethod("UnLockA", arguments, new bool[] { false, true, true }), -1);

            return TUniVar.VarToInt(arguments[2]);
        }

        /// <summary>
        /// Locks records in given table
        /// </summary>
        /// <param name="accessCode"></param>
        /// <param name="tableName"></param>
        /// <param name="recordId"></param>
        /// <param name="baseId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        [Obsolete("DBLock(string AccessCode, ...) is deprecated, please use DBLock(ComWrapper locker, ...) instead.")]
        public static int DBLock(string accessCode, string tableName, string recordId, out int baseId, out int tableId)
        {
            baseId = -1;
            tableId = -1;

            int res = -1;

            using (ComWrapper ComWrapper = new ComWrapper())
            {
                if (ComWrapper.ConnectRemote(new Guid(DBC_LOCKER_CLASS), ComWrapper.ServerName))
                {
                    object[] arguments = new object[6];
                    arguments[0] = ComWrapper.DBId;
                    arguments[1] = tableName;
                    arguments[2] = recordId;
                    arguments[3] = ComWrapper.AccessCode;
                    arguments[4] = -1; // BaseId
                    arguments[5] = -1; // TableId
                    res = CheckError(ComWrapper.InvokeMethod("Lock", arguments, new bool[] { false, false, false, false, true, true }), -1);
                    if (res >= 0)
                    {
                        baseId = Convert.ToInt32(arguments[4]);
                        tableId = Convert.ToInt32(arguments[5]);
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Unlocks previously locked records.
        /// </summary>
        /// <param name="accessCode"></param>
        /// <param name="tableName"></param>
        /// <param name="recordId"></param>
        /// <param name="baseId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        [Obsolete("DBUnlock(string AccessCode, ...) is deprecated, please use DBUnlock(ComWrapper locker, ...) instead or destroy 'locker' wrapper.")]
        public static int DBUnlock(string accessCode, string tableName, string recordId, int baseId, int tableId)
        {
            baseId = -1;
            tableId = -1;

            int res = -1;

            using (ComWrapper ComWrapper = new ComWrapper())
            {
                ComWrapper.AccessCode = accessCode;

                if (ComWrapper.ConnectRemote(new Guid(DBC_LOCKER_CLASS), ComWrapper.ServerName))
                {
                    object[] arguments = new object[6];
                    arguments[0] = ComWrapper.DBId;
                    arguments[1] = tableName;
                    arguments[2] = recordId;
                    arguments[3] = ComWrapper.AccessCode;
                    arguments[4] = baseId;
                    arguments[5] = tableId;
                    res = CheckError(ComWrapper.InvokeMethod("Unlock", arguments, new bool[] { false, false, false, false, false, false }), -1);
                    if (res >= 0)
                    {
                        baseId = Convert.ToInt32(arguments[4]);
                        tableId = Convert.ToInt32(arguments[5]);
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="comObj">The COM object.</param>
        /// <returns>System.Int32.</returns>
        public static int BeginTransaction(ref ComWrapper comObj)
        {
            int res = -1;
            ComWrapper ComWrapper;

            if (comObj.ClassID.Equals(CS_DBCOM_GUID, StringComparison.CurrentCultureIgnoreCase))
                ComWrapper = CreateRemoteCom(new Guid(CS_TRANS_GUID), comObj.AccessCode, string.Empty);
            else if (comObj.ClassID.Equals(CS_SYSTEM_GUID, StringComparison.CurrentCultureIgnoreCase))
                ComWrapper = CreateRemoteCom(new Guid(CS_SYSTEMTRANS_GUID), comObj.AccessCode, string.Empty);
            else
                ComWrapper = CreateRemoteCom(new Guid(DBC_TRANS_CLASS), comObj.AccessCode, string.Empty);

            if (Assigned(ComWrapper))
            {
                res = CheckError(ComWrapper.InvokeMethod("OpenTransaction", null, null), -1);

                if (res >= 0)
                {
                    comObj.TransactionObject = new ComWrapper.Transaction
                    {
                        Id = res,
                        ComWrapper = ComWrapper
                    };
                }
            }

            return res;
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <param name="comObj">The COM object.</param>
        /// <returns>System.Int32.</returns>
        public static int CommitTransaction(ref ComWrapper comObj)
        {
            int res = -1;

            if (Assigned(comObj.TransactionObject))
            {
                ComWrapper ComWrapper = comObj.TransactionObject.ComWrapper;
                res = CheckError(ComWrapper.InvokeMethod("CommitTransaction", null, null), -1);

                if (res >= 0)
                {
                    res = CheckError(ComWrapper.InvokeMethod("CloseTransaction", null, null), -1);
                    if (res >= 0)
                    {
                        ComWrapper.Disconnect();

                        comObj.TransactionObject.Id = -1;
                        comObj.TransactionObject.ComWrapper = null;
                        comObj.TransactionObject = null;
                    }
                }
            }

            return res;
        }

        //public static int RollbackTransaction(ref Transaction ATransaction)
        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        /// <param name="comObj">The COM object.</param>
        /// <returns>System.Int32.</returns>
        public static int RollbackTransaction(ref ComWrapper comObj)
        {
            int res = -1;

            if (Assigned(comObj.TransactionObject))
            {
                ComWrapper ComWrapper = comObj.TransactionObject.ComWrapper;
                res = CheckError(ComWrapper.InvokeMethod("RollbackTransaction", null, null), -1);

                if (res >= 0)
                {
                    res = CheckError(ComWrapper.InvokeMethod("CloseTransaction", null, null), -1);
                    if (res >= 0)
                    {
                        ComWrapper.Disconnect();

                        comObj.TransactionObject.Id = -1;
                        comObj.TransactionObject.ComWrapper = null;
                        comObj.TransactionObject = null;
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Opens the resultset.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="pkName">Name of the pk.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="timeOut">The timeout.</param>
        /// <param name="comWrapper">The COM wrapper.</param>
        /// <returns>System.Int32.</returns>
        public static int OpenResultset(string accessCode, string sql, string pkName, int transId, int timeOut, out ComWrapper comWrapper)
        {
            comWrapper = CreateRemoteCom(new Guid(DBC_CLASS), accessCode, string.Empty);
            if (Assigned(comWrapper))
            {
                object[] arguments = new object[1];
                arguments[0] = sql;
                object res = comWrapper.InvokeMethod("SQL", arguments, new bool[] { false });

                if (CheckError(res, -1) >= 0)
                {
                    if (transId == -1)
                    {
                        arguments[0] = "PrimaryKey=" + pkName + GetTimeOutParamValue(timeOut, true);
                    }
                    else
                    {
                        arguments[0] = String.Format("PrimaryKey=" + pkName + "&Transaction={0}", transId) +
                            GetTimeOutParamValue(timeOut, true);
                    }

                    res = comWrapper.InvokeMethod("OpenA", arguments, new bool[] { false });

                    return CheckError(res, -1);
                }
            }
            return 0;
        }

        /// <summary>
        /// Get Fields values of current record in DBCOm Packet format
        /// </summary>
        /// <param name="comWrapper"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static int RecordPacket(ref ComWrapper comWrapper, out object[,] packet)
        {
            packet = null;

            object[] arguments = new object[1];
            object res = comWrapper.InvokeMethod("FieldValuePacket", arguments, new bool[] { true });

            if (CheckError(res, -1) >= 0)
            {
                packet = arguments[0] as object[,];
            }

            return TUniVar.VarToInt(res);
        }

        /// <summary>
        /// Records the edit.
        /// </summary>
        /// <param name="comWrapper">The COM wrapper.</param>
        /// <param name="lockedByUserId">The locked by user identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int RecordEdit(ref ComWrapper comWrapper, out int lockedByUserId)
        {
            lockedByUserId = -1;

            object[] arguments = new object[1];
            object res = comWrapper.InvokeMethod("EditA", arguments, new bool[] { true });

            if (CheckError(res, -1) < 0)
            {
                lockedByUserId = Convert.ToInt32(arguments[0]);
            }
            return 0;
        }

        /// <summary>
        /// Records the update.
        /// </summary>
        /// <param name="comWrapper">The COM wrapper.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="values">The values.</param>
        /// <param name="pkName">Name of the pk.</param>
        /// <returns>System.Int32.</returns>
        public static int RecordUpdate(ref ComWrapper comWrapper, object fields, object values, string pkName)
        {
            object FieldValue = 0;

            object[] arguments = new object[2];
            arguments[0] = fields;
            arguments[1] = values;
            object res = comWrapper.InvokeMethod("Update", arguments, new bool[] { false, false });

            if (CheckError(res, -1) >= 0)
            {
                if (!string.IsNullOrEmpty(pkName))
                {
                    arguments = new object[1];
                    arguments[0] = pkName;
                    FieldValue = comWrapper.InvokeMethod("FieldValue", arguments, new bool[] { false });
                }
            }
            else
            {
                FieldValue = res;
            }

            return CheckError(FieldValue, -1);
        }

        /// <summary>
        /// Records the new.
        /// </summary>
        /// <param name="comWrapper">The COM wrapper.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="values">The values.</param>
        /// <param name="pkName">Name of the pk.</param>
        /// <returns>System.Int32.</returns>
        public static int RecordNew(ref ComWrapper comWrapper, object fields, object values, string pkName)
        {
            object FieldValue = 0;

            object[] arguments = new object[2];
            arguments[0] = fields;
            arguments[1] = values;
            object res = comWrapper.InvokeMethod("AddNew", arguments, new bool[] { false, false });

            if (CheckError(res, -1) >= 0)
            {
                if (!string.IsNullOrEmpty(pkName))
                {
                    arguments = new object[1];
                    arguments[0] = pkName;
                    FieldValue = comWrapper.InvokeMethod("FieldValue", arguments, new bool[] { false });
                }
            }
            else
            {
                FieldValue = res;
            }

            return CheckError(FieldValue, -1);
        }

        /// <summary>
        /// Records the new.
        /// </summary>
        /// <param name="comWrapper">The COM wrapper.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="values">The values.</param>
        /// <returns>System.Int32.</returns>
        public static int RecordNew(ref ComWrapper comWrapper, object fields, object values)
        {
            return RecordNew(ref comWrapper, fields, values, string.Empty);
        }

        /// <summary>
        /// Cancel editing of current record.
        /// </summary>
        /// <param name="comWrapper">The COM wrapper.</param>
        /// <returns>System.Int32.</returns>
        public static int RecordCancel(ref ComWrapper comWrapper)
        {
            object res = comWrapper.InvokeMethod("Cancel", null, null);
            return CheckError(res, -1);
        }

        /// <summary>
        /// Delete current record.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static int RecordDelete(ref ComWrapper comWrapper)
        {
            object res = comWrapper.InvokeMethod("Delete", null, null);
            return CheckError(res, -1);
        }

        /*MS 09.06.2016 zmienilem te funkcje bo to nie jest zdrowe zachowanie aby parametr przekazywany
         przez wskaŸnik by³ w niej w w¹tpliwy sposób niszczony. Nale¿y u¿ywaæ klauzuli using do tego!!*/
        /// <summary>
        /// Records the close.
        /// </summary>
        /// <param name="comWrapper">The COM wrapper.</param>
        /// <returns>System.Int32.</returns>
        public static int RecordClose(ComWrapper comWrapper)
        {
            comWrapper.InvokeMethod("Close", null, null);
            return 0;
        }

        /// <summary>
        /// Decodes the input string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public static Dictionary<string, string> DecodeInputString(string s, string delimiter)
        {
            return DecodeInputString(s, delimiter, false);
        }

        /// <summary>
        /// Decodes the input string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="listFromDuplicates">if set to <c>true</c> [list from duplicates].</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public static Dictionary<string, string> DecodeInputString(string s, string delimiter, bool listFromDuplicates)
        {
            if (String.IsNullOrEmpty(s))
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(0, StringComparer.OrdinalIgnoreCase);
                return dic;
            }
            else
            {
                if (s.StartsWith(delimiter, StringComparison.OrdinalIgnoreCase))
                    s = s.Substring(delimiter.Length);

                string[] collection = s.Split(delimiter[0]);
                int len = collection.Length;

                Dictionary<string, string> dic = new Dictionary<string, string>(len, StringComparer.OrdinalIgnoreCase);

                string[] keyvalue;
                string inputparam;
                string inputvalue;

                for (int i = 0; i < len; i++)
                {
                    keyvalue = collection[i].Split('=');
                    inputparam = keyvalue[0];

                    if (keyvalue.Length > 1)
                        inputvalue = keyvalue[1];
                    else
                        inputvalue = "";

                    if (dic.ContainsKey(inputparam))
                    {
                        if (listFromDuplicates)
                            dic[inputparam] += "," + inputvalue;
                        else
                            dic[inputparam] = inputvalue;
                    }
                    else
                    {
                        dic.Add(inputparam, inputvalue);
                }
                }
                return dic;
            }
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="path">The path.</param>
        /// <returns>System.Object.</returns>
        public static object GetPropertyValue(object instance, string path)
        {
            if (string.IsNullOrEmpty(path))
                return instance;

            // walk all way down the path
            foreach (var element in path.Split('.'))
            {
                var type = instance.GetType();
                var property = type.GetProperty(element, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                System.Diagnostics.Debug.Assert(property != null,
                                                $"Can't find property {element} in object of type {type.FullName}.");

                instance = property.GetValue(instance, null);
            }

            return instance;
        }

        /// <summary>
        /// Replaces the action parameters.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="actionParsed">The action parsed.</param>
        public static void ReplaceActionParams(ref string sql, Dictionary<string, string> actionParsed)
        {
            string paramName;
            string paramValue;

            foreach (KeyValuePair<string, string> item in actionParsed)
            {
                paramName = item.Key;
                paramValue = item.Value;

                if (!paramName.StartsWith("@"))
                    paramName = '@' + paramName;

                sql = sql.ReplaceWholeWords(paramName, paramValue, StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Databases the COM parse SQL.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parseData">The parse data.</param>
        /// <returns>System.Int32.</returns>
        public static int DBComParseSQL(string accessCode, ref string sql, ref object parseData)
        {
            int res = -1;

            using (ComWrapper ComWrapper = ComUtils.CreateRemoteCom(new Guid(ComUtils.CS_READER_GUID), accessCode, string.Empty))
            {
                if (ComUtils.Assigned(ComWrapper))
                {
                    object[] arguments = new object[2];
                    arguments[0] = sql;
                    arguments[1] = parseData;
                    res = ComUtils.RunMethod(ComWrapper, "ParseSQL", ref arguments);

                    if (res >= 0)
                    {
                        sql = arguments[0].ToString();
                        parseData = arguments[1];
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Hashes the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string HashString(string value)
        {
            StringBuilder hashedString = new StringBuilder();

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] data = md5.ComputeHash(Encoding.ASCII.GetBytes(value));

                for (int i = 0; i < data.Length; i++)
                    hashedString.Append(data[i].ToString("x2"));
            }

            return hashedString.ToString();
        }

        /// <summary>
        /// Determines whether [is valid URL] [the specified URL].
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns><c>true</c> if [is valid URL] [the specified URL]; otherwise, <c>false</c>.</returns>
        public static bool IsValidURL(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the name of the current database.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.String.</returns>
        public static string GetCurrentDatabaseName(string accessCode)
        {
            int res = GetPacket(
                accessCode,
                "SELECT ISNULL(Nazwa + ' (' + Opis + ')','???') " +
                "FROM LOGON_BazaDanychRzeczywista " +
                "WHERE BazaDanychRzeczywistaId = " + GetParam(accessCode, "DBID", "-1"),
                -1,
                -1,
                out object[,] packet);

            if (res > 0)
                return packet[1, 0].ToString();
            else
                return "???";
        }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.String.</returns>
        public static string GetCurrentUserName(string accessCode)
        {
            int res = GetPacket(
                accessCode,
                "SELECT ISNULL(Imie_Nazwisko,'???') " +
                "FROM OpisUzytkownika " +
                "WHERE uprUzytkownikId = " + GetParam(accessCode, "UID", "-1"),
                -1,
                -1,
                out object[,] packet);

            if (res > 0)
                return packet[1, 0].ToString();
            else
                return "???";
        }

        /// <summary>
        /// Gets the culture by language identifier.
        /// </summary>
        /// <param name="langId">The language identifier.</param>
        /// <returns>CultureInfo.</returns>
        public static CultureInfo GetCultureByLangId(int langId)
        {
            langId %= 100;
            switch (langId)
            {
                case 2:
                    return CultureInfo.CreateSpecificCulture("de"); // de-DE

                case 3:
                    return CultureInfo.CreateSpecificCulture("en"); // en-EN

                default:
                    return CultureInfo.CurrentCulture; // pl-PL
            }
        }

        /// <summary>
        /// Gets the system up time.
        /// </summary>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan GetSystemUpTime()
        {
            try
            {
#if !DEBUG
                return TimeSpan.Zero;
#else
                PerformanceCounter upTime = new PerformanceCounter("System", "System Up Time");
                upTime.NextValue();
                return TimeSpan.FromSeconds(upTime.NextValue());
#endif
            }
            catch
            {
                return TimeSpan.Zero;
            }
        }

        /// <summary>
        /// Nexts the available filename.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        public static string NextAvailableFilename(string path)
        {
            return NextAvailableFilename(path, " ({0})");
        }

        /// <summary>
        /// Nexts the available filename.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="numberPattern">The number pattern.</param>
        /// <returns>System.String.</returns>
        public static string NextAvailableFilename(string path, string numberPattern)
        {
            if (!File.Exists(path))
                return path;

            if (Path.HasExtension(path))
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), numberPattern));

            return GetNextFilename(path + numberPattern);
        }

        /// <summary>
        /// Gets the next filename.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentException">The pattern must include an index place-holder - pattern</exception>
        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern)
                throw new ArgumentException("The pattern must include an index place-holder", nameof(pattern));

            if (!File.Exists(tmp))
                return tmp;

            int min = 1, max = 2;

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }

        /// <summary>
        /// Gets the temporary file path with ext.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>System.String.</returns>
        public static string GetTempFilePathWithExt(string prefix, string extension)
        {
            var path = Path.GetTempPath();
            var fileName = String.Concat(prefix, "_", Guid.NewGuid().ToString("N"));
            return Path.Combine(path, Path.ChangeExtension(fileName, extension));
        }

        /// <summary>
        /// Creates the list.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>System.String.</returns>
        public static string CreateList(StringCollection items, String delimiter)
        {
            if (items == null)
            {
                return String.Empty;
            }

            String[] itemsArray = new String[items.Count];

            items.CopyTo(itemsArray, 0);

            return itemsArray.ToDelimitedString(x => Convert.ToString(x), delimiter);
        }

        /// <summary>
        /// Generates the passphrase.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GeneratePassphrase()
        {
            DateTime td = DateTime.Today;

            return "DD" +
                    td.DayOfWeek.ToString().Substring(0, 3).Reverse().ToUpper() +
                    Convert.ToString(100 - td.Day) +
                    td.ToString("MMMM", CultureInfo.InvariantCulture).Substring(0, 1).ToUpper();
        }

        /// <summary>
        /// Tries the login client.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <param name="dBId">The d b identifier.</param>
        /// <param name="langId">The language identifier.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="extractAC">if set to <c>true</c> [extract ac].</param>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.Int32.</returns>
        public static int TryLoginClient(string login, string password, int dBId, int langId,
            string applicationName, bool extractAC, string serverName, out string accessCode)
        {
            int res = -1;
            accessCode = String.Empty;

            object[,] Params = new object[19, 2];
            Params[0, 0] = 1; // accesscode
            Params[1, 0] = 2; // clientkey
            Params[2, 0] = 3; // enterprise
            Params[3, 0] = 4; // dbindex
            Params[4, 0] = 5; // languages
            Params[5, 0] = 6; // login
            Params[5, 1] = login;
            Params[6, 0] = 7; // password
            Params[6, 1] = ComUtils.HashString(password);
            Params[7, 0] = 8; // deploymentid
            Params[7, 1] = 0;
            Params[8, 0] = 9; // dbid
            Params[8, 1] = dBId;
            Params[9, 0] = 10; // language
            Params[9, 1] = langId;
            Params[10, 0] = 11; // lifetime
            Params[11, 0] = 12; // computername
            Params[11, 1] = System.Environment.MachineName;
            Params[12, 0] = 13; // computerusername
            Params[12, 1] = System.Environment.UserName;
            Params[13, 0] = 14; // messagepack
            Params[14, 0] = 15; // passchange
            Params[15, 0] = 16; // message
            Params[16, 0] = 17; // worktype
            Params[16, 1] = 0; // ACC_VALIDATION_IGNORE
            Params[17, 0] = 19; // appname
            Params[17, 1] = applicationName;
            Params[18, 0] = 20; // systemlogin
            Params[18, 1] = 1;

            using (ComWrapper ComWrapper = ComUtils.CreateRemoteCom(new Guid(ComUtils.CS_GKEX_GUID), "LOGON", serverName))
            {
                if (ComUtils.Assigned(ComWrapper))
                {
                    object[] arguments = new object[2];
                    arguments[0] = 2; // OperationType: GK_OPERATION_NEW_SESSION
                    arguments[1] = Params;
                    res = (int)ComWrapper.InvokeMethod("LoginClient", arguments, new bool[] { false, true });

                    if (res >= 0)
                    {
                        accessCode = Convert.ToString(((object[,])arguments[1])[0, 1]);

                        if (extractAC && (accessCode.Length > 4))
                            accessCode = accessCode.Remove(0, 4);
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Decodes the URL string.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>System.String.</returns>
        public static string DecodeUrlString(string url)
        {
            string newUrl = url;
            if (Assigned(url))
            {
                while ((newUrl = UrlDecode(url)) != url)
                    url = newUrl;
            }
            return newUrl;
        }

        /// <summary>
        /// UrlEncodes a string without the requirement for System.Web
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>System.String.</returns>
        // [Obsolete("Use System.Uri.EscapeDataString instead")]
        public static string UrlEncode(string text)
        {
            // Sytem.Uri provides reliable parsing
            return System.Uri.EscapeDataString(text);
        }

        /// <summary>
        /// UrlDecodes a string without requiring System.Web
        /// </summary>
        /// <param name="text">String to decode.</param>
        /// <returns>decoded string</returns>
        public static string UrlDecode(string text)
        {
            // pre-process for + sign space formatting since System.Uri doesn't handle it
            // plus literals are encoded as %2b normally so this should be safe
            text = text.Replace("+", " ");
            return System.Uri.UnescapeDataString(text);
        }

        /// <summary>
        /// Retrieves a value by key from a UrlEncoded string.
        /// </summary>
        /// <param name="urlEncoded">UrlEncoded String</param>
        /// <param name="key">Key to retrieve value for</param>
        /// <returns>returns the value or "" if the key is not found or the value is blank</returns>
        public static string GetUrlEncodedKey(string urlEncoded, string key)
        {
            urlEncoded = "&" + urlEncoded + "&";

            int Index = urlEncoded.IndexOf("&" + key + "=", StringComparison.OrdinalIgnoreCase);
            if (Index < 0)
                return "";

            int lnStart = Index + 2 + key.Length;

            int Index2 = urlEncoded.IndexOf("&", lnStart);
            if (Index2 < 0)
                return "";

            return UrlDecode(urlEncoded.Substring(lnStart, Index2 - lnStart));
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        public static double GetDouble(string value, double defaultValue)
        {
            string output;

            // check if last seperator == groupSeperator
            string groupSep = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            if (value.LastIndexOf(groupSep) + 4 == value.Length)
            {
                bool tryParse = double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out double result);
                return tryParse ? result : defaultValue;
            }
            else
            {
                // unify string (no spaces, only . )     
                output = value.Trim().Replace(" ", string.Empty).Replace(",", ".");

                // split it on points     
                string[] split = output.Split('.');

                if (split.Length > 1)
                {
                    // take all parts except last         
                    output = string.Concat(split.Take(split.Length - 1).ToArray());

                    // combine token parts with last part         
                    output = string.Format("{0}.{1}", output, split.Last());
                }
                // parse double invariant     
                return double.Parse(output, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the PKG headers.
        /// </summary>
        /// <param name="pkg">The PKG.</param>
        /// <param name="hdrNameCaseSensitive">if set to <c>true</c> [HDR name case sensitive].</param>
        /// <returns>Dictionary&lt;System.String, System.Int32&gt;.</returns>
        public static Dictionary<string, int> GetPkgHeaders(ref object[,] pkg, bool hdrNameCaseSensitive)
        {
            if (pkg == null) return null;

            Dictionary<string, int> _dict;
            if (hdrNameCaseSensitive)
                _dict = new Dictionary<string, int>();
            else
                _dict = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

            for (int i = 0; i < pkg.GetLength(1); i++)
            {
                _dict.Add(Convert.ToString(pkg[0, i]), i);
            }
            return _dict;
        }

        /// <summary>
        /// Strings to int array.
        /// </summary>
        /// <param name="myNumbers">My numbers.</param>
        /// <returns>System.Int32[].</returns>
        public static int[] StringToIntArray(string myNumbers)
        {
            List<int> myIntegers = new List<int>();
            Array.ForEach(myNumbers.Split(",".ToCharArray()), s =>
            {
                if (Int32.TryParse(s, out int currentInt))
                    myIntegers.Add(currentInt);
            });
            return myIntegers.ToArray();
        }

        /// <summary>
        /// A function that allows the value of a given field to be read from a standard DBCom package by name or index.
        /// return value &lt; 0: error (-1 row index greater than row count, -2 values package is null, -3 exception)
        /// return value = 0: .EOF or column missing
        /// return value &gt; 0: 1-based column index 
        /// </summary>
        /// <param name="FieldsValues">standard DBCom package or ADO package.</param>
        /// <param name="NameOrIdx">column name or column index.</param>
        /// <param name="FieldValue">(row,column) value.</param>
        /// <param name="Index">row index.</param>
        /// <returns>System.Int32.</returns>
        public static int GetPFieldValue(object FieldsValues, object NameOrIdx, ref object FieldValue, int Index = 1)
        {
            int result = 0;

            try
            {
                int rowCount = -1;

                object[,] NamesPacket = null;
                object[,] ValuesPacket = null;

                bool isADOpacket = TUniVar.VarIsArray(FieldsValues, 1);

                // extract values/names packet

                if (isADOpacket)
                {
                    if (FieldsValues is object[] ADOpacket)
                    {
                        NamesPacket = ADOpacket[4] as object[,];
                        ValuesPacket = ADOpacket[3] as object[,];

                        rowCount = TUniVar.VarToInt(ADOpacket[1]);
                    }
                }
                else
                {
                    ValuesPacket = FieldsValues as object[,];
                }

                if (ValuesPacket != null)
                {
                    // check row index correctness

                    if (!isADOpacket)
                    {
                        rowCount = ValuesPacket.GetUpperBound(0) - 1;
                    }

                    if (Index > rowCount)
                    {
                        return -1;
                    }

                    // check column name or column index usage

                    int ColIndex = -1;

                    string strNameOrIdx = TUniVar.VarToStr(NameOrIdx);

                    if (!String.IsNullOrEmpty(strNameOrIdx) && Char.IsDigit(strNameOrIdx[0]))
                    {
                        ColIndex = TUniVar.VarToInt(NameOrIdx);
                    }

                    // search for field value

                    FieldValue = null;

                    if (ColIndex < 0)
                    {
                        if (isADOpacket)
                        {
                            for (int i = NamesPacket.GetLowerBound(0); i < NamesPacket.GetUpperBound(0) + 1; i++)
                            {
                                if (strNameOrIdx.Equals(ValuesPacket[i, 0].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                {
                                    FieldValue = ValuesPacket[i, Index - 1];
                                    return i;
                                }
                            }
                        }
                        else
                        {
                            for (int i = ValuesPacket.GetLowerBound(1); i < ValuesPacket.GetUpperBound(1) + 1; i++)
                            {
                                if (strNameOrIdx.Equals(ValuesPacket[0, i].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                {
                                    FieldValue = ValuesPacket[Index, i];
                                    return i;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (isADOpacket)
                        {
                            FieldValue = ValuesPacket[ColIndex, Index - 1];
                        }
                        else
                        {
                            FieldValue = ValuesPacket[Index, ColIndex];
                        }

                        return ColIndex;
                    }
                }
                else
                    return -2;                    
            }
            catch
            {
                return -3;
            }

            return result;
        }

        /// <summary>Gets the p field value as variant.</summary>
        /// <param name="FieldsValues">The fields values.</param>
        /// <param name="NameOrIdx">Index of the name or.</param>
        /// <param name="Index">The index.</param>
        /// <returns>Field value as variant</returns>
        public static object GetPFieldValueAsVariant(object FieldsValues, object NameOrIdx, int Index = 1)
        {
            object result = null;
            OleCheck(GetPFieldValue(FieldsValues, NameOrIdx, ref result, Index));
            return result;
        }
        /// <summary>Gets the p field value as string.</summary>
        /// <param name="FieldsValues">The fields values.</param>
        /// <param name="NameOrIdx">Index of the name or.</param>
        /// <param name="Index">The index.</param>
        /// <param name="NullValue">The null value.</param>
        /// <returns>Field value as string</returns>
        public static string GetPFieldValueAsString(object FieldsValues, object NameOrIdx, int Index, string NullValue = TUniConstants._STR_NULL)
        {
            object result = null;
            OleCheck(GetPFieldValue(FieldsValues, NameOrIdx, ref result, Index));
            if (TUniVar.VarIsNullOrEmpty(result))
            {
                return NullValue;
            }
            else
            {
                return TUniVar.VarToStr(result);
            }
        }
        /// <summary>Gets the p field value as integer.</summary>
        /// <param name="FieldsValues">The fields values.</param>
        /// <param name="NameOrIdx">Index of the name or.</param>
        /// <param name="Index">The index.</param>
        /// <param name="NullValue">The null value.</param>
        /// <returns>Field value as integer</returns>
        public static int GetPFieldValueAsInteger(object FieldsValues, object NameOrIdx, int Index, int NullValue = TUniConstants._INT_NULL)
        {
            object result = null;
            OleCheck(GetPFieldValue(FieldsValues, NameOrIdx, ref result, Index));
            if (TUniVar.VarIsNullOrEmpty(result))
            {
                return NullValue;
            }
            else
            {
                return TUniVar.VarToInt(result);
            }
        }

        /// <summary>Gets the p field value as double.</summary>
        /// <param name="FieldsValues">The fields values.</param>
        /// <param name="NameOrIdx">Index of the name or.</param>
        /// <param name="Index">The index.</param>
        /// <param name="NullValue">The null value.</param>
        /// <returns>Field value as double (for float values)</returns>
        public static double GetPFieldValueAsDouble(object FieldsValues, object NameOrIdx, int Index, double NullValue = TUniConstants._DOUBLE_NULL)
        {
            object result = null;
            OleCheck(GetPFieldValue(FieldsValues, NameOrIdx, ref result, Index));
            if (TUniVar.VarIsNullOrEmpty(result))
            {
                return NullValue;
            }
            else
            {
                return TUniVar.VarToDouble(result);
            }
        }

        /// <summary>Gets the p field value as currency.</summary>
        /// <param name="FieldsValues">The fields values.</param>
        /// <param name="NameOrIdx">Index of the name or.</param>
        /// <param name="Index">The index.</param>
        /// <param name="NullValue">The null value.</param>
        /// <returns>Field value as decimal (for money)</returns>
        public static decimal GetPFieldValueAsCurrency(object FieldsValues, object NameOrIdx, int Index, decimal NullValue = TUniConstants._DECIMAL_NULL)
        {
            object result = null;
            OleCheck(GetPFieldValue(FieldsValues, NameOrIdx, ref result, Index));
            if (TUniVar.VarIsNullOrEmpty(result))
            {
                return NullValue;
            }
            else
            {
                return TUniVar.VarToDecimal(result);
            }
        }
        /// <summary>Gets the p field value as date time.</summary>
        /// <param name="FieldsValues">The fields values.</param>
        /// <param name="NameOrIdx">Index of the name or.</param>
        /// <param name="Index">The index.</param>
        /// <param name="NullValue">The null value.</param>
        /// <returns>Field value as datetime (internal as double)</returns>
        public static DateTime GetPFieldValueAsDateTime(object FieldsValues, object NameOrIdx, int Index, double NullValue = TUniConstants._DATE_NULL)
        {
            object result = null;
            OleCheck(GetPFieldValue(FieldsValues, NameOrIdx, ref result, Index));
            if (TUniVar.VarIsNullOrEmpty(result))
            {
                return DateTime.FromOADate(NullValue);
            }
            else
            {
                return TUniVar.VarToDateTime(result);
            }
        }
    }

    /// <summary>
    /// Class that offers helping methods for common purposes
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Determines whether [is word character] [the specified c].
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns><c>true</c> if [is word character] [the specified c]; otherwise, <c>false</c>.</returns>
        public static bool IsWordChar(this char c)
        {
            return Char.IsLetterOrDigit(c) || c == '_' || c == '@';
        }

#region String extension methods
        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="toCheck">To check.</param>
        /// <param name="comp">The comp.</param>
        /// <returns><c>true</c> if [contains] [the specified to check]; otherwise, <c>false</c>.</returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            if (string.IsNullOrEmpty(toCheck) || string.IsNullOrEmpty(source))
                return true;

            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Reverses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public static string Reverse(this string input)
        {
            char[] inputarray = input.ToCharArray();
            Array.Reverse(inputarray);
            return new string(inputarray);
        }

        /// <summary>
        /// Replaces the whole words.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="oldWord">The old word.</param>
        /// <param name="newWord">The new word.</param>
        /// <param name="comparisonOption">The comparison option.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceWholeWords(this string s, string oldWord, string newWord, StringComparison comparisonOption)
        {
            if (s == null)
            {
                return null;
            }
            int startIndex = 0; // Where we start to search in s.
            int copyPos = 0; // Where we start to copy from s to sb.
            var sb = new StringBuilder();
            while (true)
            {
                int position = s.IndexOf(oldWord, startIndex, comparisonOption);
                if (position == -1)
                {
                    if (copyPos == 0)
                    {
                        return s;
                    }
                    if (s.Length > copyPos)
                    { // Copy last chunk.
                        sb.Append(s, copyPos, s.Length - copyPos);
                    }
                    return sb.ToString();
                }
                int indexAfter = position + oldWord.Length;
                if ((position == 0 || !IsWordChar(s[position - 1])) && (indexAfter == s.Length || !IsWordChar(s[indexAfter])))
                {
                    sb.Append(s, copyPos, position - copyPos).Append(newWord);
                    copyPos = position + oldWord.Length;
                }
                startIndex = position + oldWord.Length;
            }
        }

        /// <summary>
        /// Replaces the string.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="pattern">The pattern.</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns>System.String.</returns>
        static public string ReplaceString(this string original, string pattern, string replacement, StringComparison comparisonType)
        {
            if (original == null)
                return null;

            if (String.IsNullOrEmpty(pattern))
                return original;

            int lenPattern = pattern.Length;
            int idxPattern = -1;
            int idxLast = 0;

            StringBuilder result = new StringBuilder();

            while (true)
            {
                idxPattern = original.IndexOf(pattern, idxPattern + 1, comparisonType);

                if (idxPattern < 0)
                {
                    result.Append(original, idxLast, original.Length - idxLast);
                    break;
                }

                result.Append(original, idxLast, idxPattern - idxLast);
                result.Append(replacement);

                idxLast = idxPattern + lenPattern;
            }

            return result.ToString();
        }
#endregion

#region ForEach extensions

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }

        /// <summary>
        /// Linqs for each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> LINQForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
                yield return element;
            }
        }

        /// <summary>
        /// Indexeds for each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void IndexedForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            int i = 0;
            foreach (T element in source)
            {
                action(element, i);
                i++;
            }
        }
#endregion

#region Helper functions
        /// <summary>
        /// Returns the last element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>T.</returns>
        public static T ReturnLastElement<T>(this T[] source)
        {
            return source[source.Length - 1];
        }

        /// <summary>
        /// Converts to delimitedstring.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="converter">The converter.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>System.String.</returns>
        public static string ToDelimitedString<T>(
           this IEnumerable<T> source, Func<T, string> converter, string separator)
        {
            // error checking removed for readability

            StringBuilder sb = new StringBuilder();
            foreach (T item in source)
            {
                sb.Append(converter(item));
                sb.Append(separator);
            }

            return (sb.Length > 0) ?
                sb.ToString(0, sb.Length - separator.Length) : string.Empty;
        }
#endregion
    }
}
