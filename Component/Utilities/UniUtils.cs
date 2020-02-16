using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;

namespace DomConsult.GlobalShared.Utilities
{
    public static class TUniConstants
    {
        public const int _INT_NULL = -1;
        public const string _STR_NULL = "";
        public const int _DATE_NULL = -53689; // = 31-12-1752 (ta data jest zbyt odlegla dla SQL Servera)
        public const int rcOK = 0;
        public const int rcError = -1;
        public readonly static string SYSREG_PATH = System.IO.Path.Combine(Environment.SystemDirectory, "SysReg_{63AFD5B1-49BA-11D5-9AA5-00105A72C191}.ini");

        public const int csWMK = 2147480647;
        public const int csWMK_JobError = csWMK + 2;
        public const int csWMK_Error = csWMK_JobError;


        #region DBCOM package flags

        // --- stałe używane przy opisie elementów paczki DBCom-a
        public const int cpColVisible = 1; // Kolumna ma być widoczna dla klienta
        public const int cpObjectId = 2; // Kolumna przechowuje wartość klucza głównego
        // dla rekordu (tylko jeden kolumna w paczce)
        public const int cpObjectTypeId = 4; // Kolumna przechowuje wartość określająca
        // ObjectTypeId dla bieżącego rekordu
        // (tylko jedna kolumna w paczce)
        public const int cpColFirst = 32; // Kolumna widoczna która ma być pierwszą
        // kolumną w liście prezentowanej użytkownikowi
        // (opcjonalnie)

        public const int cpDate = 0x0100;
        public const int cpInteger = 0x0200;
        public const int cpFloat = 0x0400;
        public const int cpString = 0x0800;
        public const int cpDateTime = 0x1000;
        public const int cpBoolean = 0x2000;
        public const int cpCurrency = 0x4000;

        public const int cpvInteger = cpInteger + cpColVisible;
        public const int cpvFloat = cpFloat + cpColVisible;
        public const int cpvString = cpString + cpColVisible;
        public const int cpvDateTime = cpDateTime + cpColVisible;
        public const int cpvBoolean = cpBoolean + cpColVisible;
        public const int cpvCurrency = cpCurrency + cpColVisible;

        public const int cpColKey = cpObjectId;
        public const int cpColObjectTypeId = cpObjectTypeId;

        //[flags]
        // dodatkowe stale dla budowania eksploratorow
        public const uint cpExternalObjectTypeId = 0x40000000; // Kolumna z ???
        public const uint cpTreeExpandFlag = 0x80000000; // informacja czy wezel ma sie rozwijac
        public const uint cpIconId = 0x40000000; // kolumna z ikona

        #endregion

        #region dbcom errors
        public const int DBC_errOK = 0;
        public const int DBC_errOther = -6000;
        public const int DBC_errCreate = -6010;
        public const int DBC_errUpdateRecord = -6011;
        public const int DBC_errClose = -6012;
        public const int DBC_errOpen = -6013;
        public const int DBC_errEdit = -6014;
        public const int DBC_errNoMoreRecords = -6015;
        public const int DBC_errWrongFieldName = -6016;
        public const int DBC_errExecute = -6017;
        public const int DBC_errGetPacket = -6018;
        public const int DBC_errTransaction = -6019;
        public const int DBC_errGetNextRecord = -6020;
        public const int DBC_errCount = -6021;
        public const int DBC_errCancel = -6022;
        public const int DBC_errRecordNotFound = -6023;
        public const int DBC_errAddNew = -6024;
        public const int DBC_errCreateTransManager = -6025;
        public const int DBC_errOpenTransaction = -6026;
        public const int DBC_errTransPrepare = -6027;
        public const int DBC_errCommit = -6028;
        public const int DBC_errRollback = -6029;
        public const int DBC_errTransClose = -6030;
        public const int DBC_errDelete = -6031;
        public const int DBC_errDestroyTransaction = -6032;
        public const int DBC_errEof = -6033;
        public const int DBC_errWrongTransactionId = -6034;
        public const int DBC_errGetWMK = -6035;
        public const int DBC_errGetErrorDescription = -6036;
        public const int DBC_errAssignSQL = -6037;
        public const int DBC_errGetFieldsValues = -6038;
        public const int DBC_errWrongParams = -6039;
        public const int DBC_errWrongADOPacket = -6040;
        public const int DBC_errWrongFieldIndex = -6041;
        #endregion

    }

    public enum TCSWMK
    {
        csWMK = 2147480647,
        csWMK_JobConversation = 2147480648,
        csWMK_JobError = 2147480649,
        csWMK_Error = 2147480649,
        csWMK_WithDBChange = -2147480646,
        csWMK_SkipJob = 2147480646
    }

    public enum TConfirmResult
    {
        acrYes = 1,
        acrNo = 2,
        acrCancel = 3
    }

    public static class TuniGlobalCache
    {
        private static ConcurrentDictionary<string, object> Cache = new ConcurrentDictionary<string, object>();

        public static object GetSysRegParam(string acc, string key, string defaultValue = "", bool setDefault = false)
        {
            bool getOK = false;
            object value = null;

            string keyC = string.Concat("sysreg.ini/", key).ToUpper();
            bool exists = Cache.ContainsKey(keyC);

            if (exists)
            {
                getOK = Cache.TryGetValue(keyC, out value);

                if (getOK)
                    value = TUniVar.VarToStr(value, defaultValue);
            }

            if (!exists || !getOK)
            {
                string svalue = ComUtils.GetRegParam(key, "", acc);

                if (svalue.Length != 0)
                    value = svalue;
                else if (setDefault)
                {
                    value = defaultValue;
                    svalue = defaultValue;
                }
                else
                    svalue = defaultValue;

                if (!exists)
                    Cache.TryAdd(keyC, value);
            }

            return value;
        }

        public static bool GetSysRegParam(string acc, string key, ref object value, string defaultValue = "", bool setDefault = false)
        {
            bool getOK = false;
            value = null;

            string keyC = string.Concat("sysreg.ini/", key).ToUpper();
            bool exists = Cache.ContainsKey(keyC);

            if (exists)
            {
                getOK = Cache.TryGetValue(keyC, out value);

                if (getOK)
                    value = TUniVar.VarToStr(value, defaultValue);
            }

            if (!exists || !getOK)
            {
                string svalue = ComUtils.GetRegParam(key, "", acc);

                if (svalue.Length != 0)
                    value = svalue;
                else if (setDefault)
                {
                    value = defaultValue;
                    svalue = defaultValue;
                }
                else
                    svalue = defaultValue;

                if (!exists)
                    Cache.TryAdd(keyC, value);
            }

            return getOK;
        }

        public static object GetUniParam(string acc, string key, string defaultValue = "", bool setDefault = false)
        {
            bool getOK = false;
            object value = null;

            int dbID = TUserSession.GetCurrentDatabaseId(acc);
            if (dbID < 0)
                throw new Exception("AccessCode required!");

            string keyC = string.Concat("dbid=",dbID.ToString(),"/uniParam/", key).ToUpper();
            bool exists = Cache.ContainsKey(keyC);

            if (exists)
            {
                getOK = Cache.TryGetValue(keyC, out value);

                if (getOK)
                    value = TUniVar.VarToStr(value, defaultValue);
            }

            if (!exists || !getOK)
            {
                string svalue = ComUtils.GetUniParam(acc, key, "");

                if (svalue.Length != 0)
                    value = svalue;
                else if (setDefault)
                {
                    value = defaultValue;
                    svalue = defaultValue;
                }
                else
                    svalue = defaultValue;

                if (!exists)
                    Cache.TryAdd(keyC, value);
            }

            return value;
        }

        public static int GetSQLData(string acc, string sql, out object[,] data, int timeOut = -1)
        {
            int result = 0;
            bool getOK = false;
            object value = null;
            data = null;

            int dbID   = TUserSession.GetCurrentDatabaseId(acc);
            int langID = TUserSession.GetCurrentLanguageId(acc);

            if (acc.Equals("LOGON", StringComparison.CurrentCultureIgnoreCase))
            {
                dbID = 0;
                if (langID < 0)
                    langID = 1;
            }

            if (dbID < 0)
                throw new Exception("AccessCode required!");

            string keyC = string.Format("DBID={0}/LID={1}/SQL={2}", dbID, langID, sql.GetHashCode()).ToUpper();
            bool exists = Cache.ContainsKey(keyC);

            if (exists)
            {
                getOK = Cache.TryGetValue(keyC, out value);

                if (getOK)
                {
                    data = value as object[,];

                    if (TUniVar.VarIsArray(data))
                        result = data.GetUpperBound(0)-1;
                }
            }

            if (!exists || !getOK)
            {
                result = ComUtils.GetPacket(acc, sql, -1, timeOut, out data);

                if (!exists && (result >= 0))
                    Cache.TryAdd(keyC, data);
            }

            return result;
        }
    }

    public class TUserSession
    {
        public static int GetCurrentLanguageId(string accessCode)
        {
            return TStrParams.GetParamAsInteger(accessCode, "L=");
        }

        public static int GetCurrentUserId(string accessCode)
        {
            return TStrParams.GetParamAsInteger(accessCode, "UID=");
        }

        public static int GetCurrentSessionId(string accessCode)
        {
            return TStrParams.GetParamAsInteger(accessCode, "QID=");
        }

        public static int GetCurrentDatabaseId(string accessCode)
        {
            return TStrParams.GetParamAsInteger(accessCode, "DBID=");
        }
        
        public static string LanguageIdToCultureInfo(int langId)
        {
            langId %= 100;
            switch (langId)
            {
                case 1:
                    return "pl-PL";
                case 2:
                    return "de-DE";
                case 3:
                    return "en-US";
                default:
                    return "pl-PL";
            }
        }
    }

    public class TStrParams
    {
        public static int GetParam(ref string paramStr, string paramName, ref string paramValue,
            char[] delimiters, bool ignoreCase, bool removeFromParamStr)
        {
            try
            {
                string tmpParamStr = "";

                //przepisanie tablicy do slownika
                Dictionary<char, char> delims = new Dictionary<char, char>(10);
                for (int i = 0; i < delimiters.Length; i++)
                {
                    if (!delims.ContainsKey(delimiters[i]))
                    {
                        delims.Add(delimiters[i], delimiters[i]);
                    }
                }

                if (ignoreCase)
                {
                    tmpParamStr = paramStr.ToUpper();
                    paramName = paramName.ToUpper();
                }
                else
                    tmpParamStr = paramStr;

                if (paramName[paramName.Length - 1] != '=')
                    paramName = paramName + '=';

                bool beginWithDelim = delims.ContainsKey(paramName[0]);

                paramValue = TUniConstants._STR_NULL;
                int ls = paramStr.Length;
                int ln = paramName.Length;
                int l = 0;

                bool paramFound = false;
                int j0 = 0;
                int j = 0;
                do
                {
                    j0 = tmpParamStr.IndexOf(paramName, j, ls - j);
                    j = j + j0;
                    if (j == 0) //jesli znaleziono ciag na poczatku
                    {
                        paramFound = true;
                        break;
                    }
                    else if ((j0 == -1) || (j == -1)) //jesli ciagu nie znaleziono
                        break;
                    else if (beginWithDelim)
                    {
                        paramFound = true;
                        break;
                    }
                    else if (delims.ContainsKey(tmpParamStr[j - 1]))
                    {
                        paramFound = true;
                        break;
                    }
                }
                while (paramFound);

                if (paramFound)
                {
                    for (l = j + ln; l < ls; l++)
                    {
                        if (delims.ContainsKey(tmpParamStr[l]))
                        {
                            break;
                        }
                    }
                    paramValue = paramStr.Substring(j + ln, l - (j + ln));

                    if (removeFromParamStr)
                    {
                        if (beginWithDelim)
                        {
                            tmpParamStr = paramStr.Substring(0, j);
                        }
                        else if ((j > 0) && (delims.ContainsKey(tmpParamStr[j - 1])))
                        {
                            tmpParamStr = paramStr.Substring(0, j - (j > 0 ? 1 : 0));
                        }
                        paramStr = tmpParamStr + paramStr.Substring(l, ls - l);
                    }
                }
                if (paramFound)
                    return TUniConstants.rcOK;
                else
                    return TUniConstants.rcError;
            }
            catch
            {
                return TUniConstants.rcError;
            }
        }

        public static int GetParamAsInteger(string paramStr, string paramName)
        {
            return GetParamAsInteger(paramStr, paramName, new char[] { '/', ' ' }, true, TUniConstants._INT_NULL);
        }

        public static int GetParamAsInteger(string paramStr, string paramName, char[] delimiters, bool ignoreCase, int nullValue)
        {
            string lstr = "";
            if (GetParam(ref paramStr, paramName, ref lstr, delimiters, ignoreCase, true) == TUniConstants.rcOK)
            {
                int _result = nullValue;
                if (int.TryParse(lstr, out _result))
                    return _result;
                else
                    return nullValue;
            }
            else
                return nullValue;
        }

        public static string SafeSql(string inputSQL)
        {
            return inputSQL.Replace("'", "''");
        }

        public static string GetAppSetting(NameValueCollection settings, string key, string defaultValue = "")
        {
            if (settings == null)
                return defaultValue;
            else
            {
                string value = settings[key];
                return value ?? defaultValue;
            }
        }
    }

    public class TSysReg
    {
        /*
         * MS 12.12.2016
         * Ta metoda jest wątpliwej jakości - chodzi o to, że sysreg w 99% przypadków nazywa się
         * tak jak TUniConstants.SYSREG_PATH, natomiast w przypadku jeśli są partycje COMowskie
         * nazwa sysrega jest prefixowana nazwą partycji i w takim wypadku to nie zadziała
         * Najbezpieczniejszą metodą jest niestety odpytanie komponentu system.registry
         * Do tego celu jest funkcja w ComUtils GetRegParam
         */
        public static string GetSysRegValue(string key)
        {
            try
            {
                key = key + "=";
                string[] lines = System.IO.File.ReadAllLines(TUniConstants.SYSREG_PATH);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(key, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return lines[i].Substring(key.Length);
                    }
                }
                return TUniConstants._STR_NULL;
            }
            catch
            {
                return TUniConstants._STR_NULL;
            }
        }
    }

    public class TUniJob
    {
        public enum TJobEventCategory
        {
            jecService,
            jecStartJob,
            jecEndJob,
            jecInformation,
            jecWarning,
            jecError,
        }

        public static int SaveJobEvent(int jobInstanceId, string eventDescription, TJobEventCategory jobEventCategoryId)
        {
            ComWrapper comObj = null;
            try
            {
                //if(eventDescription.Length > 255)
                //{
                //    eventDescription = eventDescription.Substring(0,252) + "...";
                //}

                string sql = "SELECT * FROM jobZdarzenie WHERE jobZdarzenieId=-1";


                int result = ComUtils.OpenResultset("LOGON", sql, "jobZdarzenieId", -1, -1, out comObj);
                if (result < 0)
                    return result;

                return ComUtils.RecordNew(ref comObj,
                                          new object[] { "jobInstancjaId", "jobZdarzenieKategoriaId", "Opis" },
                                          new object[] { jobInstanceId, jobEventCategoryId, eventDescription },
                                          "jobZdarzenieId");
            }
            catch
            {
                return TUniConstants.DBC_errOther;
            }
            finally
            {
                if (comObj != null)
                {
                    comObj.Disconnect();
                }
                comObj = null;
            }
        }
    }

    public static class TWMK
    {
        public static int AddMessage(int MTSComId, int TextId, object[] Params, ref object Messages, bool Clear)
        {
            try
            {
                object[,] _msg = null;
                if (Messages is object[,])
                {
                    _msg = Messages as object[,];
                }
                else if (Messages != null) //przypadek jesli message nie jest macieza
                {
                    return -2;
                }

                int index = 0;
                if (Clear || (_msg == null))
                {
                    _msg = new object[3, 1];
                }
                else
                {
                    index = _msg.GetLength(1);
                    ResizeArray(ref _msg, index + 1, _msg.GetLength(0));
                }

                _msg[0, index] = MTSComId;
                _msg[1, index] = TextId;
                _msg[2, index] = Params;

                Messages = _msg;
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public static int GetMessagesCount(object Messages)
        {
            if (Messages as object[,] == null)
            {
                return 0;
            }
            else
            {
                object[,] _msg = Messages as object[,];
                if (_msg == null)
                {
                    return 0;
                }
                else
                {
                    return _msg.GetLength(1);
                }
            }
        }

        private static void ResizeArray<T>(ref T[,] original, int newCoNum, int newRoNum)
        {

            var newArray = new T[newRoNum, newCoNum];
            int columnCount = original.GetLength(1);
            int columns = original.GetUpperBound(0);
            for (int co = 0; co <= columns; co++)
            {
                Array.Copy(original, co * columnCount, newArray, co * newCoNum, columnCount);
            }
            original = newArray;
        }
    }

    public class TUniCert
    {
        public static void InstallCerificates(string certRootPath, bool silent = false, ComLogger log = null)
        {
            if (!Directory.Exists(certRootPath))
                return;

            bool flush = true;

            string message = string.Format("Installing certificates from path: [{0}]", certRootPath);
            if (log != null)
                log.Add(message, true, flush);
            else if (!silent)
                Console.WriteLine(message);

            foreach (string slDir in Directory.GetDirectories(certRootPath))
            {
                string storeLocationDir = Path.GetFileName(slDir);

                try
                {
                    StoreLocation storeLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), storeLocationDir);

                    foreach (string snDir in Directory.GetDirectories(slDir))
                    {
                        string storeNameDir = Path.GetFileName(snDir);

                        try
                        {
                            StoreName storeName = (StoreName)Enum.Parse(typeof(StoreName), storeNameDir);

                            if (log != null)
                                log.Add("X509Store store", true, flush);

                            X509Store store = new X509Store(storeName, storeLocation);

                            if (log != null)
                                log.Add("store.Open", true, flush);

                            store.Open(OpenFlags.ReadWrite);

                            foreach (string certPath in Directory.GetFiles(snDir))
                            {
                                try
                                {
                                    if (log != null)
                                        log.Add("X509Certificate2 cert", true, flush);

                                    //Błędne pliki bedą rzucać wyjątkami!!!
                                    X509Certificate2 cert = new X509Certificate2(certPath);

                                    if (log != null)
                                        log.Add("store.Certificates.Find", true, flush);

                                    //check if cert exists
                                    var certlist = store.Certificates.Find(X509FindType.FindByThumbprint, cert.Thumbprint, false);
                                    if (certlist.Count == 0)
                                    {
                                        message = string.Format("Certificate [{0}], [{1}] was not found. Try to install it automatically", cert.Thumbprint, cert.Subject);
                                        if (log != null)
                                            log.Add(message, true, flush);
                                        else if (!silent)
                                            Console.WriteLine(message);

                                        if (log != null)
                                            log.Add("store.Add", true, flush);

                                        store.Add(cert);
                                    }
                                    else
                                    {
                                        message = string.Format("Certificate [{0}], [{1}] found. Installation skipped", cert.Thumbprint, cert.Subject);

                                        if (log != null)
                                            log.Add(message, true, flush);
                                        else if (!silent)
                                            Console.WriteLine(message);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    message = string.Format("Unable to install certificate automatically: [{0}]", certPath);
                                    if (log != null)
                                    {
                                        log.Add(message, true, flush);
                                        log.Add(ex);
                                    }
                                    else if (!silent)
                                    {
                                        Console.WriteLine(message);
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                }
                            }

                            store.Close();
                        }
                        catch (Exception ex)
                        {
                            message = string.Format("SKIP! Unknown StoreName: {0}", storeNameDir);
                            if (log != null)
                            {
                                log.Add(message, true, flush);
                                log.Add(ex);
                            }
                            else if (!silent)
                            {
                                Console.WriteLine(message);
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = string.Format("SKIP! Unknown StoreLocation: {0}", storeLocationDir);
                    if (log != null)
                    {
                        log.Add(message, true, flush);
                        log.Add(ex);
                    }
                    else if (!silent)
                    {
                        Console.WriteLine(message);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
        }
    }

    public class TUniVar
    {
        public static bool VarIsNullOrEmpty(object value)
        {
            return ((value == null) || (value is DBNull));
        }

        public static int VarToInt(object value, int var_null = TUniConstants._INT_NULL, bool def = true)
        {
            int ivalue = var_null;

            if (VarIsNullOrEmpty(value))
                return ivalue;
            else if (int.TryParse(value.ToString(), out ivalue))
                return ivalue;
            else if (def)
                return var_null;
            else
            {
                ivalue = (int)value;
                return var_null;
            }
        }

        public static string VarToStr(object value, string var_null = TUniConstants._STR_NULL)
        {
            if (VarIsNullOrEmpty(value))
                return var_null;
            else
                return value.ToString();
        }

        public static string VarToSQLStr(object value, string dateFormat = "yyyyMMdd HH:mm:ss.fff")
        {
            string res = "null";
            NumberFormatInfo nfi = null;
            if (!VarIsNullOrEmpty(value))
            {
                switch (value.GetType().Name.ToUpper())
                {
                    case "DATETIME":
                        res = string.Concat("'", ((DateTime)value).ToString(dateFormat), "'");
                        break;
                    case "DECIMAL":
                        nfi = new NumberFormatInfo
                        {
                            NumberDecimalSeparator = ".",
                            NumberGroupSeparator = ""
                        };
                        res = ((string)value).ToString(nfi);
                        break;
                    case "FLOAT":
                        nfi = new NumberFormatInfo
                        {
                            NumberDecimalSeparator = ".",
                            NumberGroupSeparator = ""
                        };
                        res = ((float)value).ToString(nfi);
                        break;
                    case "BOOLEAN":
                        res = (bool)value ? "1" : "0";
                        break;
                    default:
                        res = string.Concat("'", ((string)value).Replace("'", "''"), "'"); ;
                        break;
                }
            }
            return res;
        }

        public static bool VarIsArray(object value, int dimCount = 0)
        {
            if (VarIsNullOrEmpty(value))
                return false;
            else if (dimCount == 0)
                return value.GetType().IsArray;
            else
            {
                Array arr = value as Array;

                if (arr == null)
                    return false;
                else
                    return (arr.Rank == dimCount);
            }
        }
    }
}