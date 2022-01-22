using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DomConsult.GlobalShared.Utilities
{
    /// <summary>
    /// Class TUniConstants.
    /// </summary>
    public static class TUniConstants
    {
        /// <summary>
        /// The int null
        /// </summary>
        public const int _INT_NULL = -1;
        /// <summary>
        /// The string null
        /// </summary>
        public const string _STR_NULL = "";
        /// <summary>
        /// The date null
        /// </summary>
        public const int _DATE_NULL = -53689; // = 31-12-1752 (ta data jest zbyt odlegla dla SQL Servera)
        /// <summary>
        /// The rc ok
        /// </summary>
        public const int rcOK = 0;
        /// <summary>
        /// The rc error
        /// </summary>
        public const int rcError = -1;
        /// <summary>
        /// The sysreg path
        /// </summary>
        public readonly static string SYSREG_PATH = System.IO.Path.Combine(Environment.SystemDirectory, "SysReg_{63AFD5B1-49BA-11D5-9AA5-00105A72C191}.ini");

        /// <summary>
        /// The cs WMK
        /// </summary>
        public const int csWMK = 2147480647;
        /// <summary>
        /// The cs WMK job error
        /// </summary>
        public const int csWMK_JobError = csWMK + 2;
        /// <summary>
        /// The cs WMK error
        /// </summary>
        public const int csWMK_Error = csWMK_JobError;

        #region DBCOM package flags

        // --- stałe używane przy opisie elementów paczki DBCom-a
        /// <summary>
        /// The cp col visible
        /// </summary>
        public const int cpColVisible = 1; // Kolumna ma być widoczna dla klienta
        /// <summary>
        /// The cp object identifier
        /// </summary>
        public const int cpObjectId = 2; // Kolumna przechowuje wartość klucza głównego
        // dla rekordu (tylko jeden kolumna w paczce)
        /// <summary>
        /// The cp object type identifier
        /// </summary>
        public const int cpObjectTypeId = 4; // Kolumna przechowuje wartość określająca
        // ObjectTypeId dla bieżącego rekordu
        // (tylko jedna kolumna w paczce)
        /// <summary>
        /// The cp col first
        /// </summary>
        public const int cpColFirst = 32; // Kolumna widoczna która ma być pierwszą
        // kolumną w liście prezentowanej użytkownikowi
        // (opcjonalnie)

        /// <summary>
        /// The cp date
        /// </summary>
        public const int cpDate = 0x0100;
        /// <summary>
        /// The cp integer
        /// </summary>
        public const int cpInteger = 0x0200;
        /// <summary>
        /// The cp float
        /// </summary>
        public const int cpFloat = 0x0400;
        /// <summary>
        /// The cp string
        /// </summary>
        public const int cpString = 0x0800;
        /// <summary>
        /// The cp date time
        /// </summary>
        public const int cpDateTime = 0x1000;
        /// <summary>
        /// The cp boolean
        /// </summary>
        public const int cpBoolean = 0x2000;
        /// <summary>
        /// The cp currency
        /// </summary>
        public const int cpCurrency = 0x4000;

        /// <summary>
        /// The CPV integer
        /// </summary>
        public const int cpvInteger = cpInteger + cpColVisible;
        /// <summary>
        /// The CPV float
        /// </summary>
        public const int cpvFloat = cpFloat + cpColVisible;
        /// <summary>
        /// The CPV string
        /// </summary>
        public const int cpvString = cpString + cpColVisible;
        /// <summary>
        /// The CPV date time
        /// </summary>
        public const int cpvDateTime = cpDateTime + cpColVisible;
        /// <summary>
        /// The CPV boolean
        /// </summary>
        public const int cpvBoolean = cpBoolean + cpColVisible;
        /// <summary>
        /// The CPV currency
        /// </summary>
        public const int cpvCurrency = cpCurrency + cpColVisible;

        /// <summary>
        /// The cp col key
        /// </summary>
        public const int cpColKey = cpObjectId;
        /// <summary>
        /// The cp col object type identifier
        /// </summary>
        public const int cpColObjectTypeId = cpObjectTypeId;

        //[flags]
        // dodatkowe stale dla budowania eksploratorow
        /// <summary>
        /// The cp external object type identifier
        /// </summary>
        public const uint cpExternalObjectTypeId = 0x40000000; // Kolumna z ???
        /// <summary>
        /// The cp tree expand flag
        /// </summary>
        public const uint cpTreeExpandFlag = 0x80000000; // informacja czy wezel ma sie rozwijac
        /// <summary>
        /// The cp icon identifier
        /// </summary>
        public const uint cpIconId = 0x40000000; // kolumna z ikona

        #endregion

        #region dbcom errors
        /// <summary>
        /// The DBC error ok
        /// </summary>
        public const int DBC_errOK = 0;
        /// <summary>
        /// The DBC error other
        /// </summary>
        public const int DBC_errOther = -6000;
        /// <summary>
        /// The DBC error create
        /// </summary>
        public const int DBC_errCreate = -6010;
        /// <summary>
        /// The DBC error update record
        /// </summary>
        public const int DBC_errUpdateRecord = -6011;
        /// <summary>
        /// The DBC error close
        /// </summary>
        public const int DBC_errClose = -6012;
        /// <summary>
        /// The DBC error open
        /// </summary>
        public const int DBC_errOpen = -6013;
        /// <summary>
        /// The DBC error edit
        /// </summary>
        public const int DBC_errEdit = -6014;
        /// <summary>
        /// The DBC error no more records
        /// </summary>
        public const int DBC_errNoMoreRecords = -6015;
        /// <summary>
        /// The DBC error wrong field name
        /// </summary>
        public const int DBC_errWrongFieldName = -6016;
        /// <summary>
        /// The DBC error execute
        /// </summary>
        public const int DBC_errExecute = -6017;
        /// <summary>
        /// The DBC error get packet
        /// </summary>
        public const int DBC_errGetPacket = -6018;
        /// <summary>
        /// The DBC error transaction
        /// </summary>
        public const int DBC_errTransaction = -6019;
        /// <summary>
        /// The DBC error get next record
        /// </summary>
        public const int DBC_errGetNextRecord = -6020;
        /// <summary>
        /// The DBC error count
        /// </summary>
        public const int DBC_errCount = -6021;
        /// <summary>
        /// The DBC error cancel
        /// </summary>
        public const int DBC_errCancel = -6022;
        /// <summary>
        /// The DBC error record not found
        /// </summary>
        public const int DBC_errRecordNotFound = -6023;
        /// <summary>
        /// The DBC error add new
        /// </summary>
        public const int DBC_errAddNew = -6024;
        /// <summary>
        /// The DBC error create trans manager
        /// </summary>
        public const int DBC_errCreateTransManager = -6025;
        /// <summary>
        /// The DBC error open transaction
        /// </summary>
        public const int DBC_errOpenTransaction = -6026;
        /// <summary>
        /// The DBC error trans prepare
        /// </summary>
        public const int DBC_errTransPrepare = -6027;
        /// <summary>
        /// The DBC error commit
        /// </summary>
        public const int DBC_errCommit = -6028;
        /// <summary>
        /// The DBC error rollback
        /// </summary>
        public const int DBC_errRollback = -6029;
        /// <summary>
        /// The DBC error trans close
        /// </summary>
        public const int DBC_errTransClose = -6030;
        /// <summary>
        /// The DBC error delete
        /// </summary>
        public const int DBC_errDelete = -6031;
        /// <summary>
        /// The DBC error destroy transaction
        /// </summary>
        public const int DBC_errDestroyTransaction = -6032;
        /// <summary>
        /// The DBC error EOF
        /// </summary>
        public const int DBC_errEof = -6033;
        /// <summary>
        /// The DBC error wrong transaction identifier
        /// </summary>
        public const int DBC_errWrongTransactionId = -6034;
        /// <summary>
        /// The DBC error get WMK
        /// </summary>
        public const int DBC_errGetWMK = -6035;
        /// <summary>
        /// The DBC error get error description
        /// </summary>
        public const int DBC_errGetErrorDescription = -6036;
        /// <summary>
        /// The DBC error assign SQL
        /// </summary>
        public const int DBC_errAssignSQL = -6037;
        /// <summary>
        /// The DBC error get fields values
        /// </summary>
        public const int DBC_errGetFieldsValues = -6038;
        /// <summary>
        /// The DBC error wrong parameters
        /// </summary>
        public const int DBC_errWrongParams = -6039;
        /// <summary>
        /// The DBC error wrong ADO packet
        /// </summary>
        public const int DBC_errWrongADOPacket = -6040;
        /// <summary>
        /// The DBC error wrong field index
        /// </summary>
        public const int DBC_errWrongFieldIndex = -6041;
        #endregion

    }

    /// <summary>
    /// Enum TCSWMK
    /// </summary>
    public enum TCSWMK
    {
        /// <summary>
        /// The cs WMK
        /// </summary>
        csWMK = 2147480647,
        /// <summary>
        /// The cs WMK job conversation
        /// </summary>
        csWMK_JobConversation = 2147480648,
        /// <summary>
        /// The cs WMK job error
        /// </summary>
        csWMK_JobError = 2147480649,
        /// <summary>
        /// The cs WMK error
        /// </summary>
        csWMK_Error = 2147480649,
        /// <summary>
        /// The cs WMK with database change
        /// </summary>
        csWMK_WithDBChange = -2147480646,
        /// <summary>
        /// The cs WMK skip job
        /// </summary>
        csWMK_SkipJob = 2147480646
    }

    /// <summary>
    /// Enum TConfirmResult
    /// </summary>
    public enum TConfirmResult
    {
        /// <summary>
        /// The acr yes
        /// </summary>
        acrYes = 1,
        /// <summary>
        /// The acr no
        /// </summary>
        acrNo = 2,
        /// <summary>
        /// The acr cancel
        /// </summary>
        acrCancel = 3
    }

    /// <summary>
    /// Class TuniGlobalCache.
    /// </summary>
    public static class TuniGlobalCache
    {
        /// <summary>
        /// The cache
        /// </summary>
        private static ConcurrentDictionary<string, object> Cache = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Gets the system reg parameter.
        /// </summary>
        /// <param name="acc">The acc.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="setDefault">if set to <c>true</c> [set default].</param>
        /// <returns>System.Object.</returns>
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
                {
                    value = svalue;
                }
                else if (setDefault)
                {
                    value = defaultValue;
                    svalue = defaultValue;
                }
                else
                {
                    svalue = defaultValue;
                }

                if (!exists)
                    Cache.TryAdd(keyC, value);
            }

            return value;
        }

        /// <summary>
        /// Gets the system reg parameter.
        /// </summary>
        /// <param name="acc">The acc.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="setDefault">if set to <c>true</c> [set default].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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
                {
                    value = svalue;
                }
                else if (setDefault)
                {
                    value = defaultValue;
                    svalue = defaultValue;
                }
                else
                {
                    svalue = defaultValue;
                }

                if (!exists)
                    Cache.TryAdd(keyC, value);
            }

            return getOK;
        }

        /// <summary>
        /// Gets the uni parameter.
        /// </summary>
        /// <param name="acc">The acc.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="setDefault">if set to <c>true</c> [set default].</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="Exception">AccessCode required!</exception>
        public static object GetUniParam(string acc, string key, string defaultValue = "")
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
                else
                    value = defaultValue;

                if (!exists)
                    Cache.TryAdd(keyC, value);
            }

            return value;
        }

        /// <summary>
        /// Gets the SQL data.
        /// </summary>
        /// <param name="acc">The acc.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="data">The data.</param>
        /// <param name="timeOut">The time out.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="Exception">AccessCode required!</exception>
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

        public static string GetMLText(int comId, int langId, int textId, string defaultValue = "")
        {
            string keyC = string.Concat("comtext/M={1}/L={0}/T={2}", comId, langId, textId).ToUpper();
            bool getOK = Cache.TryGetValue(keyC, out object value);

            if (!getOK)
            {
                value = ComUtils.GetText(comId, langId, textId);

                if (value != null)
                    Cache.TryAdd(keyC, value);
            }

            return TUniVar.VarToStr(value, defaultValue);
        }
    }

    /// <summary>
    /// Class TUserSession.
    /// </summary>
    public static class TUserSession
    {
        /// <summary>
        /// Gets the current language identifier.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.Int32.</returns>
        public static int GetCurrentLanguageId(string accessCode)
        {
            return TStrParams.GetParamAsInteger(accessCode, "L=");
        }

        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.Int32.</returns>
        public static int GetCurrentUserId(string accessCode)
        {
            return TStrParams.GetParamAsInteger(accessCode, "UID=");
        }

        /// <summary>
        /// Gets the current session identifier.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.Int32.</returns>
        public static int GetCurrentSessionId(string accessCode)
        {
            return TStrParams.GetParamAsInteger(accessCode, "QID=");
        }

        /// <summary>
        /// Gets the current database identifier.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.Int32.</returns>
        public static int GetCurrentDatabaseId(string accessCode)
        {
            return TStrParams.GetParamAsInteger(accessCode, "DBID=");
        }

        /// <summary>
        /// Languages the identifier to culture information.
        /// </summary>
        /// <param name="langId">The language identifier.</param>
        /// <returns>System.String.</returns>
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

    /// <summary>
    /// Class TStrParams.
    /// </summary>
    public static class TStrParams
    {
        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="paramStr">The parameter string.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <param name="delimiters">The delimiters.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <param name="removeFromParamStr">if set to <c>true</c> [remove from parameter string].</param>
        /// <returns>System.Int32.</returns>
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
                {
                    tmpParamStr = paramStr;
                }

                if (paramName[paramName.Length - 1] != '=')
                    paramName += '=';

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
                    j += j0;
                    if (j == 0) //jesli znaleziono ciag na poczatku
                    {
                        paramFound = true;
                        break;
                    }
                    else if ((j0 == -1) || (j == -1)) //jesli ciagu nie znaleziono
                    {
                        break;
                    }
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

        /// <summary>
        /// Gets the parameter as integer.
        /// </summary>
        /// <param name="paramStr">The parameter string.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>System.Int32.</returns>
        public static int GetParamAsInteger(string paramStr, string paramName)
        {
            return GetParamAsInteger(paramStr, paramName, new char[] { '/', ' ' }, true, TUniConstants._INT_NULL);
        }

        /// <summary>
        /// Gets the parameter as integer.
        /// </summary>
        /// <param name="paramStr">The parameter string.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="delimiters">The delimiters.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <param name="nullValue">The null value.</param>
        /// <returns>System.Int32.</returns>
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
            {
                return nullValue;
            }
        }

        public static int GetParamsAsDic(ref string paramStr, out Dictionary<string, object> paramsDic)
        {
            paramsDic = new Dictionary<string, object>();
            string tmpStr = paramStr;

            //Założenie jest takie, że znaki "/=" nie występują w wartościach
            paramStr = "";
            while (tmpStr.Contains("/"))
            {
                var start = tmpStr.IndexOf("/");
                if (start > 0)
                {
                    paramStr += tmpStr.Substring(0, start);
                    tmpStr = tmpStr.Substring(start);
                    start = 0;
                }

                var len = tmpStr.IndexOf("=");
                if (len < 0)
                {
                    paramStr += tmpStr;
                    break;
                }

                var paramName = tmpStr.Substring(start, len);
                var paramValue = string.Empty;
                TStrParams.GetParam(ref tmpStr, paramName, ref paramValue, new char[] { '/', '=' }, true, true);

                paramsDic.Add(paramName, paramValue);
            }

            return paramsDic.Count;
        }

        public static int GetParamsAsArr(ref string paramStr, out object[,] paramsArr)
        {
            paramsArr = null;
            TStrParams.GetParamsAsDic(ref paramStr, out Dictionary<string, object> paramsDic);

            int result = 0;
            if ((paramsDic?.Count ?? 0) > 0)
            {
                paramsArr = new object[paramsDic.Count, 2];
                foreach (var elem in paramsDic)
                {
                    paramsArr[result, 0] = elem.Key;
                    paramsArr[result, 1] = elem.Value;
                    result++;
                }
            }

            return result;
        }

        public static int GetParamsArrAsDic(object[,] paramsArr, out Dictionary<string, object> paramsDic)
        {
            paramsDic = new Dictionary<string, object>();
            const int result = 0;

            for (int i = 0; i < paramsArr.GetLength(0); i++)
            {
                if (paramsArr[i, 0] != null)
                {
                    paramsDic.Add((string)paramsArr[i, 0], paramsArr[i, 1]);
                }
            }

            return result;
        }

        /// <summary>
        /// Safes the SQL.
        /// </summary>
        /// <param name="inputSQL">The input SQL.</param>
        /// <returns>System.String.</returns>
        public static string SafeSql(string inputSQL)
        {
            return inputSQL.Replace("'", "''");
        }

        /// <summary>
        /// Gets the application setting.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string GetAppSetting(NameValueCollection settings, string key, string defaultValue = "")
        {
            if (settings == null)
            {
                return defaultValue;
            }
            else
            {
                string value = settings[key];
                return value ?? defaultValue;
            }
        }
    }

    /// <summary>
    /// Class TSysReg.
    /// </summary>
    public static class TSysReg
    {
        /*
         * MS 12.12.2016
         * Ta metoda jest wątpliwej jakości - chodzi o to, że sysreg w 99% przypadków nazywa się
         * tak jak TUniConstants.SYSREG_PATH, natomiast w przypadku jeśli są partycje COMowskie
         * nazwa sysrega jest prefixowana nazwą partycji i w takim wypadku to nie zadziała
         * Najbezpieczniejszą metodą jest niestety odpytanie komponentu system.registry
         * Do tego celu jest funkcja w ComUtils GetRegParam
         */
        /// <summary>
        /// Gets the system reg value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public static string GetSysRegValue(string key)
        {
            try
            {
                key += "=";
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

    /// <summary>
    /// Class TUniJob.
    /// </summary>
    public static class TUniJob
    {
        /// <summary>
        /// Enum TJobEventCategory
        /// </summary>
        public enum TJobEventCategory
        {
            /// <summary>
            /// The jec service
            /// </summary>
            jecService,
            /// <summary>
            /// The jec start job
            /// </summary>
            jecStartJob,
            /// <summary>
            /// The jec end job
            /// </summary>
            jecEndJob,
            /// <summary>
            /// The jec information
            /// </summary>
            jecInformation,
            /// <summary>
            /// The jec warning
            /// </summary>
            jecWarning,
            /// <summary>
            /// The jec error
            /// </summary>
            jecError,
        }

        /// <summary>
        /// Saves the job event.
        /// </summary>
        /// <param name="jobInstanceId">The job instance identifier.</param>
        /// <param name="eventDescription">The event description.</param>
        /// <param name="jobEventCategoryId">The job event category identifier.</param>
        /// <returns>System.Int32.</returns>
        public static int SaveJobEvent(int jobInstanceId, string eventDescription, TJobEventCategory jobEventCategoryId)
        {
            ComWrapper comObj = null;
            try
            {
                //if(eventDescription.Length > 255)
                //{
                //    eventDescription = eventDescription.Substring(0,252) + "...";
                //}

                const string sql = "SELECT * FROM jobZdarzenie WHERE jobZdarzenieId=-1";

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
                comObj?.Disconnect();
            }
        }
    }

    /// <summary>
    /// Class TWMK.
    /// </summary>
    public static class TWMK
    {
        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="MTSComId">The MTS COM identifier.</param>
        /// <param name="TextId">The text identifier.</param>
        /// <param name="Params">The parameters.</param>
        /// <param name="Messages">The messages.</param>
        /// <param name="Clear">if set to <c>true</c> [clear].</param>
        /// <returns>System.Int32.</returns>
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

        public static int AddMessage(ref object Messages, int MTSComId, int TextId, params object[] Params)
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
                if (_msg == null)
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

        /// <summary>
        /// Gets the messages count.
        /// </summary>
        /// <param name="Messages">The messages.</param>
        /// <returns>System.Int32.</returns>
        public static int GetMessagesCount(object Messages)
        {
            if ((Messages as object[,]) == null)
            {
                return 0;
            }
            else
            {
                if (!(Messages is object[,] _msg))
                {
                    return 0;
                }
                else
                {
                    return _msg.GetLength(1);
                }
            }
        }

        /// <summary>
        /// Resizes the array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original">The original.</param>
        /// <param name="newCoNum">The new co number.</param>
        /// <param name="newRoNum">The new ro number.</param>
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

    /// <summary>
    /// Class TUniCert.
    /// </summary>
    public static class TUniCert
    {
        /// <summary>
        /// Installs the cerificates.
        /// </summary>
        /// <param name="certRootPath">The cert root path.</param>
        /// <param name="silent">if set to <c>true</c> [silent].</param>
        /// <param name="log">The log.</param>
        public static void InstallCerificates(string certRootPath, bool silent = false, ComLogger log = null)
        {
            if (!Directory.Exists(certRootPath))
                return;

            const bool flush = true;

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

                            log?.Add("X509Store store", true, flush);

                            X509Store store = new X509Store(storeName, storeLocation);

                            log?.Add("store.Open", true, flush);

                            store.Open(OpenFlags.ReadWrite);

                            foreach (string certPath in Directory.GetFiles(snDir))
                            {
                                try
                                {
                                    log?.Add("X509Certificate2 cert", true, flush);

                                    //Błędne pliki bedą rzucać wyjątkami!!!
                                    X509Certificate2 cert = new X509Certificate2(certPath);

                                    log?.Add("store.Certificates.Find", true, flush);

                                    //check if cert exists
                                    var certlist = store.Certificates.Find(X509FindType.FindByThumbprint, cert.Thumbprint, false);
                                    if (certlist.Count == 0)
                                    {
                                        message = string.Format("Certificate [{0}], [{1}] was not found. Try to install it automatically", cert.Thumbprint, cert.Subject);
                                        if (log != null)
                                            log.Add(message, true, flush);
                                        else if (!silent)
                                            Console.WriteLine(message);

                                        log?.Add("store.Add", true, flush);

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

    /// <summary>
    /// Class TUniVar.
    /// </summary>
    public static class TUniVar
    {
        /// <summary>
        /// Variables the is null or empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool VarIsNullOrEmpty(object value)
        {
            return (value == null) || (value is DBNull);
        }

        public static bool VarIsNullOrEmptyOrDefault(object value, object defValue)
        {
            return (value == null) || (value is DBNull) || value.Equals(defValue);
        }

        /// <summary>
        /// Variables to int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="var_null">The variable null.</param>
        /// <param name="def">if set to <c>true</c> [definition].</param>
        /// <returns>System.Int32.</returns>
        public static int VarToInt(object value, int var_null = TUniConstants._INT_NULL, bool def = true)
        {
            int ivalue = var_null;

            if (VarIsNullOrEmpty(value))
            {
                return ivalue;
            }
            else if (int.TryParse(value.ToString(), out ivalue))
            {
                return ivalue;
            }
            else if (def)
            {
                return var_null;
            }
            else
            {
                ivalue = (int)value;
                return var_null;
            }
        }

        /// <summary>
        /// Variables to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="var_null">The variable null.</param>
        /// <returns>System.String.</returns>
        public static string VarToStr(object value, string var_null = TUniConstants._STR_NULL, int len = TUniConstants._INT_NULL)
        {
            string res;

            if (VarIsNullOrEmpty(value))
                return var_null;
            else
                res = value.ToString();

            if ((len > 0) && (len < res.Length))
            {
                res = res.Substring(0, len);
            }

            return res;
        }

        /// <summary>
        /// Variables to SQL string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <returns>System.String.</returns>
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
                        res = string.Concat("'", (value.ToString()).Replace("'", "''"), "'");
                        break;
                }
            }
            return res;
        }

        public static DateTime VarToDateTime(object value, DateTime? var_null = null)
        {
            if (VarIsNullOrEmpty(value))
            {
                return var_null ?? DateTime.MinValue;
            }
            else
            {
                return (DateTime)value;
            }
        }

        public static DateTime? VarToDateTimeN(object value)
        {
            if (VarIsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return (DateTime)value;
            }
        }

        /// <summary>
        /// Variables the is array.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="dimCount">The dim count.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool VarIsArray(object value, int dimCount = 0)
        {
            if (VarIsNullOrEmpty(value))
            {
                return false;
            }
            else if (dimCount == 0)
            {
                return value.GetType().IsArray;
            }
            else
            {
                if (!(value is Array arr))
                    return false;
                else
                    return arr.Rank == dimCount;
            }
        }
    }

    public static class TUniTools
    {
        public static string ResolvePath(string acc, string path)
        {
            var regExp = new Regex("%(.)+%");
            var matches = regExp.Matches(path);
            foreach (var match in matches)
            {
                string key = match.ToString().Replace("%","");
                string value = (string)TuniGlobalCache.GetUniParam(acc, key);
                path = path.Replace(string.Concat("%", key, "%"), value);
            }

            if (path.Length > 2)
            {
                string start = "";
                if (path.StartsWith(@"\\"))
                {
                    start = @"\\";
                    path = path.Substring(2);
                }
                else if(path.StartsWith("//"))
                {
                    start = "//";
                    path = path.Substring(2);
                }

                path = path.Replace(@"\\",@"\");
                path = path.Replace("//", "/");
                path = string.Concat(start, path);
            }

            return path;
        }

        public static bool ErrorDetected(int result_, bool include_csWMK = false)
        {
            return (result_ < 0) || (result_ == (int)TCSWMK.csWMK_Error) || ((result_ == (int)TCSWMK.csWMK) && include_csWMK);
        }
    }
}