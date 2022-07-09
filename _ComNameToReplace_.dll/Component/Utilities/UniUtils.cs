using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;

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
        /// Use DateTime.FromOADate(TUniConstants._DATE_NULL) to convert
        /// </summary>
        public const double _DATE_NULL = -53689; // = 31-12-1752
        /// <summary>
        /// The decimal null (Delphi: Currency)
        /// </summary>
        public const decimal _DECIMAL_NULL = 0.0m;
        /// <summary>
        /// The double null (Delphi: Float)
        /// </summary>
        public const double _DOUBLE_NULL = 0.0;
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
        /// Obligatory for executing SQL multipart script
        /// </summary>
        public const string CS_NO_COUNT = "SET NOCOUNT ON; SET XACT_ABORT ON; \r\n";

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

        // --- sta³e u¿ywane przy opisie elementów paczki DBCom-a
        /// <summary>
        /// The cp col visible
        /// </summary>
        public const int cpColVisible = 1; // Kolumna ma byæ widoczna dla klienta
        /// <summary>
        /// The cp object identifier
        /// </summary>
        public const int cpObjectId = 2; // Kolumna przechowuje wartoœæ klucza g³ównego
        // dla rekordu (tylko jeden kolumna w paczce)
        /// <summary>
        /// The cp object type identifier
        /// </summary>
        public const int cpObjectTypeId = 4; // Kolumna przechowuje wartoœæ okreœlaj¹ca
        // ObjectTypeId dla bie¿¹cego rekordu
        // (tylko jedna kolumna w paczce)
        /// <summary>
        /// The cp col first
        /// </summary>
        public const int cpColFirst = 32; // Kolumna widoczna która ma byæ pierwsz¹
        // kolumn¹ w liœcie prezentowanej u¿ytkownikowi
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
    /// Enum TDisabledFunction
    /// </summary>
    [Flags]
    public enum TDisabledFunction
    {
        /// <summary>
        /// Disable New
        /// </summary>
        dfNew = 0x0001,
        /// <summary>
        /// Disable Edit
        /// </summary>
        dfEdit = 0x0002,
        /// <summary>
        /// Disable Save
        /// </summary>
        dfSave = 0x0004,
        /// <summary>
        /// Disable Cancel
        /// </summary>
        dfCancel = 0x0008,
        /// <summary>
        /// Disable Refresh
        /// </summary>
        dfRefresh = 0x0010,
        /// <summary>
        /// Disable Switch
        /// </summary>
        dfSwitch = 0x0020,
        /// <summary>
        /// Disable Delete
        /// </summary>
        dfDelete = 0x0040,
        /// <summary>
        /// Disable Report Print
        /// </summary>
        dfReportPrint = 0x0080,
        /// <summary>
        /// Disable Report Save
        /// </summary>
        dfReportSave = 0x0100,
        /// <summary>
        /// Disable New Hidden
        /// </summary>
        dfNewHide = 0x0200,
        /// <summary>
        /// Disable Eddit Hidden
        /// </summary>
        dfEditHide = 0x0400,
        /// <summary>
        /// Disable Save Hidden
        /// </summary>
        dfSaveHide = 0x0800,
        /// <summary>
        /// Disable Cancel Hidden
        /// </summary>
        dfCancelHide = 0x1000,
        /// <summary>
        /// Disable Refresh Hidden
        /// </summary>
        dfRefreshHide = 0x2000,
        /// <summary>
        /// Disable Delete Hidden
        /// </summary>
        dfDeleteHide = 0x4000,
        /// <summary>
        /// Disable All
        /// </summary>
        dfAll = dfNew | dfEdit | dfSave | dfCancel | dfRefresh |
                dfSwitch | dfDelete |
                dfReportPrint | dfReportSave |
                dfNewHide | dfEditHide | dfSaveHide | dfCancelHide | dfRefreshHide | dfDeleteHide
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
    /// 
    /// </summary>
    public enum VarType
    {
        varEmpty = 0x0000,
        varNull     = 0x0001,
        varSmallint = 0x0002,
        varInteger = 0x0003,
        varSingle = 0x0004,
        varDouble = 0x0005,
        varCurrency = 0x0006,
        varDate = 0x0007,
        varOleStr = 0x0008,
        varDispatch = 0x0009,
        varError = 0x000A,
        varBoolean = 0x000B,
        varVariant = 0x000C,
        varUnknown = 0x000D,
        varDecimal  = 0x000E,
        varUndef0F  = 0x000F,
        varShortInt = 0x0010,
        varByte = 0x0011,
        varWord = 0x0012,
        varLongWord = 0x0013,
        varInt64 = 0x0014,
        varWord64   = 0x0015,

        varStrArg = 0x0048,
        varString = 0x0100,
        varAny = 0x0101,
        varTypeMask = 0x0FFF,
        varArray = 0x2000,
        varByRef = 0x4000
}

    /// <summary>
    /// Class TuniGlobalCache.
    /// </summary>
    public static class TuniGlobalCache
    {
        //KEYs' templates should be globally unique!!!
        //Be careful defining kes for GetCustomData. Check if your data depend on DBID, LID, UID ...

        /// <summary>
        /// {0}=ParamName
        /// </summary>
        public const string KEY_GetSysRegParam = "SYSREG.INI/{0}";
        /// <summary>
        /// {0}=DBID,{1}=ParamName
        /// </summary>
        public const string KEY_GetUniParam = "UNIPARAM/D={0}/{1}";
        /// <summary>
        /// {0}=DBID,{1}=LanguageId,{2}=SQL HASH not SQL!!!
        /// </summary>
        public const string KEY_GetSQLData = "SQL/D={0}/L={1}/S={2}";
        /// <summary>
        /// {0}=MtsComId,{1}=LanguageId,{2}=TextId
        /// </summary>
        public const string KEY_GetMLText = "COMTEXT/C={0}/L={1}/T={2}";
        /// <summary>
        /// {0}=DBID,{1}=UserId,{2}=ComFunctionId
        /// </summary>
        public const string KEY_CanRunComFunction = "COMFUNCTION/D={0}/U={1}/F={2}";
        /// <summary>
        /// {0}=DBID,{1}=Key
        /// </summary>
        public const string KEY_GetCustomData_DK = "DATA/D={0}/K={2}";
        /// <summary>
        /// {0}=DBID,{1}=UserId,{2}=Key
        /// </summary>
        public const string KEY_GetCustomData_DUK = "DATA/D={0}/U={1}/K={2}";
        /// <summary>
        /// {0}=DBID,{1}=LanguageId,{2}=UserId,{3}=Key
        /// </summary>
        public const string KEY_GetCustomData_DLUK = "DATA/D={0}/L={1}/U={2}/K={3}";

        /// <summary>
        /// The cache
        /// </summary>
        private static readonly ConcurrentDictionary<string, object> Cache = new ConcurrentDictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Gets the system reg parameter.
        /// </summary>
        /// <param name="acc">The acc.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="cacheIfDefault">if set to <c>true</c> value will be cached even if defaultValue is used</param>
        /// <returns>System.Object.</returns>
        public static object GetSysRegParam(string acc, string key, string defaultValue = "", bool cacheIfDefault = false)
        {
            string keyC = string.Format(KEY_GetSysRegParam, key).ToUpper();

            if (Cache.TryGetValue(keyC, out object value))
                value = TUniVar.VarToStr(value, defaultValue);
            else
            {
                const string EMPTY_TOKEN = "!@#$%^&*()";
                string svalue = ComUtils.GetRegParam(key, EMPTY_TOKEN, acc);

                if (svalue.Equals(EMPTY_TOKEN))
                {
                    value = defaultValue;
                    if (cacheIfDefault)
                        Cache.TryAdd(keyC, value);
                }
                else
                {
                    value = svalue;
                    Cache.TryAdd(keyC, value);
                }
            }

            return value;
        }

        /// <summary>
        /// Gets the uni parameter.
        /// </summary>
        /// <param name="acc">The acc.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="cacheIfDefault">if set to <c>true</c> value will be cached even if defaultValue is used</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="Exception">AccessCode required!</exception>
        public static object GetUniParam(string acc, string key, string defaultValue = "", bool cacheIfDefault = false)
        {
            int dbID = TUserSession.GetCurrentDatabaseId(acc);
            if (dbID < 0)
                throw new Exception("AccessCode required!");

            string keyC = string.Format(KEY_GetUniParam, dbID.ToString(), key).ToUpper();

            if (Cache.TryGetValue(keyC, out object value))
                value = TUniVar.VarToStr(value, defaultValue);
            else
            {
                const string EMPTY_TOKEN = "!@#$%^&*()";
                string svalue = ComUtils.GetUniParam(acc, key, EMPTY_TOKEN);

                if (svalue.Equals(EMPTY_TOKEN))
                {
                    value = defaultValue;
                    if (cacheIfDefault)
                        Cache.TryAdd(keyC, value);
                }
                else
                {
                    value = svalue;
                    Cache.TryAdd(keyC, value);
                }
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

            string keyC = string.Format(KEY_GetSQLData, dbID, langID, sql.GetHashCode()).ToUpper();

            if (Cache.TryGetValue(keyC, out object value))
            {
                data = value as object[,];

                if (TUniVar.VarIsArray(data))
                    result = data.GetUpperBound(0)-1;
            }
            else
            {
                result = ComUtils.GetPacket(acc, sql, -1, timeOut, out data);

                if (result >= 0)
                    Cache.TryAdd(keyC, data);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="comId"></param>
        /// <param name="langId"></param>
        /// <param name="textId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetMLText(int comId, int langId, int textId, string defaultValue = "")
        {
            string keyC = string.Format(KEY_GetMLText, comId, langId, textId).ToUpper();

            if (!Cache.TryGetValue(keyC, out object value))
            {
                value = ComUtils.GetText(comId, langId, textId);

                if (value != null)
                    Cache.TryAdd(keyC, value);
            }

            return TUniVar.VarToStr(value, defaultValue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="comFunctionId"></param>
        /// <returns></returns>
        public static bool CanRunComFunction(string acc, int comFunctionId)
        {
            bool result = TUserSession.GetCurrentUserIsAdmin(acc);

            if (!result)
            {
                int dbID = TUserSession.GetCurrentDatabaseId(acc);
                int uID = TUserSession.GetCurrentUserId(acc);
                if ((dbID < 0) || (uID < 0))
                    throw new Exception("AccessCode required!");

            string keyC = string.Format(KEY_CanRunComFunction, dbID, uID, comFunctionId).ToUpper();

                if (Cache.TryGetValue(keyC, out object value))
                    result = (bool)value;
                else
                {
                    object cf = comFunctionId;
                    result = TUserSession.CanRunComFunction(acc, ref cf);
                    Cache.TryAdd(keyC, result);
                }
            }

            return result;
        }

        /// <summary>
        /// Method used to get data from cache or get data from source and store in cache
        /// </summary>
        /// <param name="key">Key value connected with the searched data</param>
        /// <param name="getData">Method used to get data when not found in cache</param>
        /// <param name="cacheData">Determines if data should be cached when not found in cache</param>
        /// <returns></returns>
        public static object GetCustomData(string key, Func<object> getData, bool cacheData = true)
        {
            if (!Cache.TryGetValue(key, out object value))
            {
                value = getData();

                if (!TUniVar.VarIsNullOrEmpty(value) && cacheData)
                    Cache.TryAdd(key, value);
            }

            return value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteData(string key, out object data)
        {
            return Cache.TryRemove(key, out data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool CheckData(string key)
        {
            return Cache.ContainsKey(key);
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
        ///
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        public static string GetCurrentUserName(string acc)
        {
            string userName = "";

            string sql = "select Imie_Nazwisko from OpisUzytkownika where UprUzytkownikId = {0}";
            sql = string.Format(sql, GetCurrentUserId(acc));
            int res = ComUtils.GetPacket(acc, sql, -1, -1, out object[,] data);
            if (res > 0)
            {
                userName = TUniVar.VarToStr(data[1, 0]);
            }

            return userName;
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
        /// Checks if current user is admin.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.bool.</returns>
        public static bool GetCurrentUserIsAdmin(string accessCode)
        {
            return accessCode.Contains("/ADM/", StringComparison.CurrentCultureIgnoreCase);
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="comFunctions"> comFunctionId or vector with many comFunctionIds</param>
        /// <param name="mulMandantId"></param>
        /// <returns>
        /// For vector returns True if user has rights to any of given ids.
        /// Id in vector is OVERRITEN to 0 if user has not rights to this Id.
        /// For single id (not vetor with single id) returns true if user has rights to this comFunction.
        /// </returns>
        public static bool CanRunComFunction(string acc, ref object comFunctions, int mulMandantId = 0)
        {
            if (comFunctions == null)
                return false;

            int result = -1;
            bool isArray = comFunctions.GetType().IsArray;

            if (mulMandantId > 0)
            {
                acc = string.Concat(acc, "/MID=", mulMandantId.ToString());
                using (var rightsCom = new ComWrapper())
                {
                    if (rightsCom.Connect(ComUtils.CS_GKEX_ProgID))
                    {
                        var args = new object[] { acc, comFunctions };
                        result = (int)rightsCom.InvokeMethod("CheckPermission", args, new bool[] { false, true });
                    }
                }
            }

            return isArray ? result > 0 : result >= 0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        public static string GetCurrentUserWinLogin(string acc)
        {
            string winLogin = "";

            string sql = "select NazwaUzytkownikaMaszyny from LOGON_LogUzytkownik where LogUzytkownikId={0}";
            sql = string.Format(sql, GetCurrentSessionId(acc));
            int res = ComUtils.GetPacket(acc, sql, -1, -1, out object[,] data);
            if (res > 0)
            {
                winLogin = TUniVar.VarToStr(data[1,0]);
            }

            return winLogin;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="fullTempPath"></param>
        /// <param name="relativeTempPath"></param>
        /// <param name="winLogin"></param>
        public static void GetExportTempFolder(string acc, out string fullTempPath, out string relativeTempPath, string winLogin = "")
        {
            fullTempPath = (string)TuniGlobalCache.GetUniParam(acc, @"UNI\APP\PATH\EXPORTS", "");
            relativeTempPath = @"temp\";

            int.TryParse((string)TuniGlobalCache.GetUniParam(acc, @"UNI\APP\PATH\EXPORTS", "0"), out int priv);

            if (priv == 0)
            {
                fullTempPath = Path.Combine(fullTempPath, relativeTempPath);
                return;
            }

            if (winLogin.Length == 0)
                winLogin = GetCurrentUserWinLogin(acc);

            if (winLogin.Contains("\\"))
            {
                //winLogin: DOMENA\USERNAME, COMPUTER\USERNAME
                winLogin = winLogin.Split(new char[] { '\\' }).ReturnLastElement<string>();
            }

            relativeTempPath = string.Concat(relativeTempPath, winLogin, "\\");
            fullTempPath = Path.Combine(fullTempPath, relativeTempPath);
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
                    if ((j == 0) || //jesli znaleziono ciag na poczatku
                        beginWithDelim ||
                        delims.ContainsKey(tmpParamStr[j - 1]))
                    {
                        paramFound = true;
                        break;
                    }
                    else if ((j0 == -1) || (j == -1)) //jesli ciagu nie znaleziono
                    {
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
        public static int GetParamAsInteger(string paramStr, string paramName, char[] delimiters, bool ignoreCase, int nullValue = TUniConstants._INT_NULL)
        {
            string lstr = "";

            if (GetParam(ref paramStr, paramName, ref lstr, delimiters, ignoreCase, true) == TUniConstants.rcOK &&
                int.TryParse(lstr, out int _result))
            {
                return _result;
            }

            return nullValue;
        }

        /// <summary>
        /// Gets the parameter as integer.
        /// </summary>
        /// <param name="paramStr">The parameter string.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>System.Int32.</returns>
        public static string GetParamAsString(string paramStr, string paramName)
        {
            return GetParamAsString(paramStr, paramName, new char[] { '/', ' ' }, true, TUniConstants._STR_NULL);
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
        public static string GetParamAsString(string paramStr, string paramName, char[] delimiters, bool ignoreCase, string nullValue = TUniConstants._STR_NULL)
        {
            string lstr = "";

            if (GetParam(ref paramStr, paramName, ref lstr, delimiters, ignoreCase, true) == TUniConstants.rcOK)
            {
                return lstr;
            }

            return nullValue;
        }

        /// <summary>
        /// Converts string parameters into dictionary (string, object)
        /// </summary>
        /// <param name="paramStr">The parameter string.</param>
        /// <param name="paramsDic">Output parameters' dictionary</param>
        /// <returns></returns>
        public static int GetParamsAsDic(string paramStr, out Dictionary<string, object> paramsDic)
        {
            paramsDic = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
            string tmpStr = paramStr;

            //Za³o¿enie jest takie, ¿e znaki "/=" nie wystêpuj¹ w wartoœciach
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
                    //paramStr += tmpStr;
                    break;
                }

                var paramName = tmpStr.Substring(start, len+1); /* /xxxx= */
                var paramValue = string.Empty;
                TStrParams.GetParam(ref tmpStr, paramName, ref paramValue, new char[] { '/', '=' }, true, true);

                paramsDic.Add(paramName.Substring(1, paramName.Length-2), paramValue);
            }

            return paramsDic.Count;
        }

        /// <summary>
        /// Converts string parameters to two dim array (Name, Value)
        /// </summary>
        /// <param name="paramStr">String that represents input parameters</param>
        /// <param name="paramsArr">Parameters in the form of two dim array (Name, Value)</param>
        /// <returns></returns>
        public static int GetParamsAsArr(string paramStr, out object[,] paramsArr)
        {
            paramsArr = null;
            TStrParams.GetParamsAsDic(paramStr, out Dictionary<string, object> paramsDic);

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

        /// <summary>
        /// Converts two dim packet (Name, Value) into dictionary(string, object)
        /// </summary>
        /// <param name="paramsArr"></param>
        /// <param name="paramsDic"></param>
        /// <returns></returns>
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
         * Ta metoda jest w¹tpliwej jakoœci - chodzi o to, ¿e sysreg w 99% przypadków nazywa siê
         * tak jak TUniConstants.SYSREG_PATH, natomiast w przypadku jeœli s¹ partycje COMowskie
         * nazwa sysrega jest prefixowana nazw¹ partycji i w takim wypadku to nie zadzia³a
         * Najbezpieczniejsz¹ metod¹ jest niestety odpytanie komponentu system.registry
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
        /// <param name="Messages">The messages.</param>
        /// <param name="MTSComId">The MTS COM identifier.</param>
        /// <param name="TextId">The text identifier.</param>
        /// <param name="Params">The parameters.</param>
        /// <returns>System.Int32.</returns>
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
        /// Joins two stacks of messages
        /// </summary>
        /// <param name="Messages"></param>
        /// <param name="NewMessages"></param>
        /// <returns></returns>
        public static int AddMessages(ref object Messages, object NewMessages)
        {
            try
            {
                if (Messages == null)
                {
                    Messages = NewMessages;
                    return GetMessagesCount(Messages);
                }
                else if (NewMessages == null)
                {
                    return GetMessagesCount(Messages);
                }
                else if (!(Messages is object[,]) || !(NewMessages is object[,]))
                {
                    return -2;
                }

                object[,] _msg = Messages as object[,];
                object[,] _new = NewMessages as object[,];

                int index = _msg.GetLength(1);
                int len   = _new.GetLength(1);

                ResizeArray(ref _msg, index + len, _msg.GetLength(0));

                for (var i=0; i<len; i++)
                {
                    _msg[0, index + i] = _new[0, i];
                    _msg[1, index + i] = _new[1, i];
                    _msg[2, index + i] = _new[2, i];
                }

                Messages = _msg;
                return GetMessagesCount(Messages);
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
            if (!(Messages is object[,]))
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
        /// Procedure creates empty message for RunMethods that cause application to not show any message.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static int CreateEmptyRunMethodMessage(out object msg)
        {
            msg = (object) new object[3, 1] { { "" }, { "" }, { 0 } };
            return 1;
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

                                    //B³êdne pliki bed¹ rzucaæ wyj¹tkami!!!
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
        /// Checks if value is null, DBNull
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool VarIsNullOrEmpty(object value)
        {
            return (value == null) || (value is DBNull);
        }

        /// <summary>
        /// Checks if value is null, DBNull or default
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static bool VarIsNullOrEmptyOrDefault(object value, object defValue)
        {
            return (value == null) || (value is DBNull) || value.Equals(defValue);
        }

        /// <summary>
        /// Converts object to int with optional default value assignment
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="var_null">The variable null.</param>
        /// <param name="def">if set to <c>true</c> [definition].</param>
        /// <returns>System.Int32.</returns>
        public static int VarToInt(object value, int var_null = TUniConstants._INT_NULL, bool def = true)
        {
            if (VarIsNullOrEmpty(value))
            {
                return var_null;
            }
            else if (int.TryParse(value.ToString(), out int ivalue))
            {
                return ivalue;
            }
            else if (def)
            {
                return var_null;
            }
            else
            {
                return (int)value;
            }
        }

        /// <summary>
        /// Converts object to string with optional default value assignment
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="var_null">The variable null.</param>
        /// <param name="len">Optional length of output string</param>
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
        /// Converts object to string with quotation and special additional adjustment for SQL statement
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <returns>System.String.</returns>
        public static string VarToSQLStr(object value, string dateFormat = "yyyyMMdd HH:mm:ss.fff")
        {
            string res = "null";
            NumberFormatInfo nfi;
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
                        res = ((decimal)value).ToString(nfi);
                        break;
                    case "DOUBLE":
                        nfi = new NumberFormatInfo
                        {
                            NumberDecimalSeparator = ".",
                            NumberGroupSeparator = ""
                        };
                        res = ((double)value).ToString(nfi);
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

        /// <summary>
        /// Converts object to DateTime with optional default value assignment
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="var_null">The default value.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Converts object to nullable DateTime
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
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
        /// Converts object to Double with optional default value assignment
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="var_null">The default value.</param>
        /// <returns></returns>
        public static double VarToDouble(object value, double var_null = TUniConstants._DOUBLE_NULL)
        {
            if (VarIsNullOrEmpty(value))
            {
                return var_null;
            }
            else
            {
                return (double)value;
            }
        }

        /// <summary>
        /// Converts object to Decimal with optional default value assignment
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="var_null">The default value.</param>
        /// <returns></returns>
        public static decimal VarToDecimal(object value, decimal var_null = TUniConstants._DECIMAL_NULL)
        {
            if (VarIsNullOrEmpty(value))
            {
                return var_null;
            }
            else
            {
                return (decimal)value;
            }
        }

        /// <summary>
        /// Converts unseparated string date to DateTime
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>DateTime</returns>
        public static DateTime UnseparatedDate(string value)
        {

            if (DateTime.TryParseExact(value,
                                       "yyyyMMdd",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out DateTime res))
            {
                return res;
            }
            else
            {
                return DateTime.FromOADate(TUniConstants._DATE_NULL);
            }
        }

        /// <summary>
        /// Checks if object is an array with apropriate amount of dimentions
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

    /// <summary>
    /// Class TUniTools
    /// </summary>
    public static class TUniTools
    {
        /// <summary>
        /// Builds full string\path containing tokens %UniParamName%
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Function checks if result value indicates error
        /// </summary>
        /// <param name="result_">Result to check</param>
        /// <param name="include_csWMK">Optional switch to check WMK</param>
        /// <returns></returns>
        public static bool ErrorDetected(int result_, bool include_csWMK = false)
        {
            return (result_ < 0) || (result_ == (int)TCSWMK.csWMK_Error) || ((result_ == (int)TCSWMK.csWMK) && include_csWMK);
        }

        /// <summary>
        /// Gets identifier from lookup control
        /// </summary>
        /// <param name="FieldValue">Packet from lookup</param>
        /// <returns>System.Int32.</returns>
        public static int GetIdFromLookup(object FieldValue)
        {
            if (TUniVar.VarIsArray(FieldValue, 2))
            {
                object packetFieldValue = null;

                if (FieldValue is object[,] value)
                {
                    ComUtils.GetPFieldValue(FieldValue, value[0, 0], ref packetFieldValue);
                }
                return TUniVar.VarToInt(packetFieldValue);
            }
            else // lookup compatibility mode
            {
                return TUniVar.VarToInt(FieldValue);
            }
        }

        /// <summary>
        /// Gets current process identifier
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static int ProcessId
        {
            get
            {
                if (_processId == null)
                {
                    using (var thisProcess = System.Diagnostics.Process.GetCurrentProcess())
                    {
                        _processId = thisProcess.Id;
                    }
                }
                return _processId.Value;
            }
        }
        private static int? _processId;
    }

    /// <summary>
    ///
    /// </summary>
    public static class TuniDebug
    {
        private static readonly string DDTempSubFolder = "$ddtemp";
        private static readonly string DDTempSubFolderPath = "";

        static TuniDebug()
        {
            DDTempSubFolderPath = Path.Combine(Path.GetTempPath(), DDTempSubFolder);
        }

        public static string DDTEMP()
        {
            return DDTempSubFolderPath;
        }

        /// <summary>
        /// Not implemented!!!
        /// </summary>
        /// <returns></returns>
        public static string LogEventA()
        {
            return "";
        }

        /// <summary>
        /// Not implemented!!!
        /// </summary>
        /// <returns></returns>
        public static string LogEventE()
        {
            return "";
        }

        /// <summary>
        /// Not implemented!!!
        /// </summary>
        /// <returns></returns>
        public static string OV2HTML(object data, bool createFile, string fileSuffix, string fileName)
        {
            StringBuilder sl = new StringBuilder();
            sl.AppendLine("<!DOCTYPE HTML PUBLIC \" -//W3C//DTD HTML 4.0 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">");
            sl.AppendLine("<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\">");
            sl.AppendLine("<script type=\"text/javascript\">");
            sl.AppendLine("function bypassInternetExplorer(){");
            sl.AppendLine("  t = document.getElementsByTagName(\"table\")");
            sl.AppendLine("  for (i=0; i<t.length; i++)");
            sl.AppendLine("    if (t[i].className == \"d2\")");
            sl.AppendLine("      t[i].style.display = \"none\"");
            sl.AppendLine("}");
            sl.AppendLine("function sw(o){");
            sl.AppendLine("  isCollapsed = o.className == \"plus\"");
            sl.AppendLine("  o.className = isCollapsed ? \"minus\" : \"plus\"");
            sl.AppendLine("  s = o.id.replace(\"i_\", \"t_\")");
            sl.AppendLine("  t = document.getElementById(s)");
            sl.AppendLine("  t.style.display = isCollapsed ? \"table\" : \"none\"");
            sl.AppendLine("}");
            sl.AppendLine("function ecall(){");
            sl.AppendLine("  o = document.getElementById(\"plusminus\")");
            sl.AppendLine("  var doexp = o.getAttribute(\"doexp\") == \"1\"");
            sl.AppendLine("  o.setAttribute(\"doexp\", doexp ? \"0\" : \"1\")");
            sl.AppendLine("  o.innerHTML = doexp ? \"[-all]\" : \"[+all]\"");
            sl.AppendLine("  o.style.color = doexp ? \"red\" : \"yellow\"");
            sl.AppendLine("  t = document.getElementsByTagName(\"table\")");
            sl.AppendLine("  for (i=0; i<t.length; i++)");
            sl.AppendLine("    if ((t[i].className == \"d1\") || (t[i].className == \"d2\"))");
            sl.AppendLine("      t[i].style.display = doexp ? \"table\" : \"none\"");
            sl.AppendLine("  t = document.getElementsByTagName(\"img\")");
            sl.AppendLine("  for (i=0; i<t.length; i++)");
            sl.AppendLine("    t[i].className = doexp ? \"minus\" : \"plus\"");
            sl.AppendLine("}");
            sl.AppendLine("function sh(ev){");
            sl.AppendLine("  if (ev.target)");
            sl.AppendLine("    el = ev.target");
            sl.AppendLine("  else");
            sl.AppendLine("    el = ev.srcElement");
            sl.AppendLine("  var tb");
            sl.AppendLine("  hd = document.getElementById(\"hintdiv\")");
            sl.AppendLine("  hd.style.position = \"absolute\"");
            sl.AppendLine("  if(el.id == \"plusminus\"){");
            sl.AppendLine("  } else {");
            sl.AppendLine("    var descr");
            sl.AppendLine("    var tip");
            sl.AppendLine("    var withCoords = false");
            sl.AppendLine("    var cx = \"\"");
            sl.AppendLine("    var cy = \"\"");
            sl.AppendLine("    var cd = \"/\"");
            sl.AppendLine("    var t = \"\"");
            sl.AppendLine("    var null_warn_level = 0");
            sl.AppendLine("    ht = document.getElementById(\"hint\")");
            sl.AppendLine("    pm = document.getElementById(\"plusminus\")");
            sl.AppendLine("    if(ev.ctrlKey){");
            sl.AppendLine("      pm.style.display = \"inline\"");
            sl.AppendLine("      pm.style.left = window.pageXOffset + 4");
            sl.AppendLine("      pm.style.top = window.pageYOffset + 4");
            sl.AppendLine("    } else");
            sl.AppendLine("      pm.style.display = \"none\"");
            sl.AppendLine("    switch(el.tagName){");
            sl.AppendLine("      case \"IMG\": {");
            sl.AppendLine("          tb = document.getElementById(el.id.replace(\"i_\", \"t_\"))");
            sl.AppendLine("          tx = document.getElementById(el.id.replace(\"i_\", \"tx_\"))");
            sl.AppendLine("          if (tx) descr = tx.getAttribute(\"descr\")");
            sl.AppendLine("          ht.style.color = \"white\"");
            sl.AppendLine("          break");
            sl.AppendLine("        }");
            sl.AppendLine("      case \"TD\": {");
            sl.AppendLine("          descr = el.getAttribute(\"descr\")");
            sl.AppendLine("          tip = el.getAttribute(\"tip\")");
            sl.AppendLine("          withCoords = true");
            sl.AppendLine("          t = el.className");
            sl.AppendLine("          cx = el.cellIndex");
            sl.AppendLine("          cy = el.parentNode.rowIndex");
            sl.AppendLine("        }");
            sl.AppendLine("      case \"TR\":");
            sl.AppendLine("      case \"TBODY\":");
            sl.AppendLine("      case \"TABLE\":");
            sl.AppendLine("        if (ev.shiftKey || t == \"dtstr0\"){");
            sl.AppendLine("          if (t == \"dtstr0\") {");
            sl.AppendLine("            t = \"dtstr\"");
            sl.AppendLine("            null_warn_level = ev.shiftKey ? 1 : 2");
            sl.AppendLine("          }");
            sl.AppendLine("          while (el.tagName != \"TABLE\") el = el.parentNode;");
            sl.AppendLine("          ht.style.color = \"lightgray\"");
            sl.AppendLine("          tb = el");
            sl.AppendLine("        }");
            sl.AppendLine("    }");
            sl.AppendLine("  }");
            sl.AppendLine("  var s=\"\"");
            sl.AppendLine("  var wx = ev.pageX + 16");
            sl.AppendLine("  var wy = ev.pageY + 16");
            sl.AppendLine("  if(tb){");
            sl.AppendLine("    var d=\"\"");
            sl.AppendLine("    var d0=\"\"");
            sl.AppendLine("    if(descr){");
            sl.AppendLine("      s = descr");
            sl.AppendLine("      withCoords = false");
            sl.AppendLine("    } else switch(tb.className){");
            sl.AppendLine("      case \"d1\":");
            sl.AppendLine("      case \"d1m\": {");
            sl.AppendLine("          s = \"vector\"");
            sl.AppendLine("          d = tb.rows[0].cells.length; cd = \"\"; cy = \"\";");
            sl.AppendLine("          break;");
            sl.AppendLine("        }");
            sl.AppendLine("      case \"d2\":");
            sl.AppendLine("      case \"d2m\": {");
            sl.AppendLine("          s = \"array\"");
            sl.AppendLine("          d = tb.rows.length * tb.rows[0].cells.length");
            sl.AppendLine("          d0 = \" <b style =\\\"color:steelblue;\\\">\" + tb.rows.length + \" <i style=\\\"font-weight:normal;\\\">x</i> \" + tb.rows[0].cells.length + \"</b> \"; ");
            sl.AppendLine("        }");
            sl.AppendLine("    }");
            sl.AppendLine("  }");
            sl.AppendLine("  if(s!=\"\"){");
            sl.AppendLine("    var c = \"\"");
            sl.AppendLine("    if(withCoords)");
            sl.AppendLine("      c = \" <hr> current cell: <b style =\\\"color:limegreen;\\\">\" + cy + cd + cx + \"</b>\"");
          
            sl.AppendLine("    if(d!=\"\"){");
            sl.AppendLine("      d = \" of <b style =\\\"color:royalblue;\\\">\" + d + \"</b> elements\"");
          
            sl.AppendLine("    }");
            sl.AppendLine("    if(t!=\"\"){");
            sl.AppendLine("      t = \"(type: \" + t + (tip?\"[\" + tip + \"]\":\"\") + \")\"");
            sl.AppendLine("    }");
            sl.AppendLine("    var nwl = \"\"");
            sl.AppendLine("    switch (null_warn_level) {");
            sl.AppendLine("      case 1: nwl = \" <hr> \"");
            sl.AppendLine("      case 2: nwl += \" <span style =\\\"color:darkorange;\\\">This string contains embedded #0 characters.<br>They have been substituted with spaces.</span>\"");
          
            sl.AppendLine("    }");
            sl.AppendLine("    s0 = tb.getAttribute(\"et\")");
            sl.AppendLine("    ht.innerHTML = (null_warn_level == 2) ? nwl : d0 + (s0 ? s0 + \" \" : \"\") + s + d + c + t + nwl");
            sl.AppendLine("    hd.style.display = \"inline\"");
            sl.AppendLine("    hd.style.left = 0");
            sl.AppendLine("    hd.style.width = null");
            sl.AppendLine("    var hdw = hd.offsetWidth");
            sl.AppendLine("    hd.style.width = hdw");
            sl.AppendLine("    hd.style.left = wx + \"px\"");
            sl.AppendLine("    hd.style.top = wy + \"px\"");
            sl.AppendLine("    var ff = (window.innerWidth ? 1 : 0)");
            sl.AppendLine("    var t");
            sl.AppendLine("    t = (ff ? document.body.clientWidth : document.documentElement.clientWidth)");
            sl.AppendLine("    if((wx + hdw) > t)");
            sl.AppendLine("      hd.style.left = t - hdw + \"px\"");
            sl.AppendLine("    t = (ff ? document.body.clientHeight : document.documentElement.clientHeight)");
            sl.AppendLine("    if((wy + hd.offsetHeight) > t)");
            sl.AppendLine("      hd.style.top = t - hd.offsetHeight + \"px\"");
            sl.AppendLine("  } else");
            sl.AppendLine("    hd.style.display = \"none\"");
            sl.AppendLine("}");
            sl.AppendLine("</script>");
            sl.AppendLine("<style type=\"text/css\">");
            sl.AppendLine("<!--");
            sl.AppendLine(".plus {width:9px; height:9px; cursor:pointer;");
            sl.AppendLine("  background-image:url(\"data: image/png; base64,iVBORw0KGgoAAAANSUhEUgAAAAkAAAAJCAQAAABKmM6bAAAAXUlEQVR4Xm3OPQ6CMACA0Vdl0qt4W07j5CXcXJywBgygW/mpiV008Rvf9IWTc/bVIajzb3WuWARszUiomABJMhZa7cEOF2xYDB64u5oK9aIGUedVqHXTOH7oiT9fb1vrMMdtZ9M3AAAAAElFTkSuQmCC\");");
            sl.AppendLine("}");
            sl.AppendLine(".minus {width:9px; height:9px; cursor:pointer;");
            sl.AppendLine("  background-image:url(\"data: image/png; base64,iVBORw0KGgoAAAANSUhEUgAAAAkAAAAJCAQAAABKmM6bAAAAW0lEQVR4Xm2OQQqAMAwEp+jNr/jbPshPePMJHlQEhZY2LqQHBSfsEuYQEiZm48UYiPYlWg + FgMCABEhlHJM4XFUGGiyuCrtSNTeXq01rxjB1cbVyktq1Dvj56wGuyjDz0PweWAAAAABJRU5ErkJggg == \");");
            sl.AppendLine("}");
            sl.AppendLine("#plusminus {cursor:pointer; background-color:black; padding:2px; color:yellow; position:absolute; display:none;}");
            sl.AppendLine("table {border-width:2px;}");
            sl.AppendLine("table.d1 {border-style:dashed; border-spacing:1px; border-collapse:collapse;}");
            sl.AppendLine("table.d1m {border-style:dashed;}");
            sl.AppendLine("table.d2 {border-style:solid; border-spacing:0px; border-collapse:collapse;}");
            sl.AppendLine("table.d2m {border-style:solid;}");
            sl.AppendLine("");
            sl.AppendLine("td, textarea {font-size:8pt; background-color:#ffffff;");
            if (TUniVar.VarIsArray(data))
                sl.AppendLine("  font-family:\"Lucida Console\";");
            else
                sl.AppendLine("  font-family:\"Tahoma\";");
            sl.AppendLine("}");
            sl.AppendLine("td:hover {border-style:solid; border-color:red; background-color:#bef0fe; color:#000099;}");
            sl.AppendLine(".dtstr {white-space: break-spaces;}");
            sl.AppendLine(".dtstr:hover {border-style:solid; border-color:red; background-color:#bef0fe; color:#000099;}");
            sl.AppendLine(".dtstr0 {border-style:solid; border-color:crimson; border-left-width:9px; white-space:break-spaces;}");
            sl.AppendLine(".dtstr0:hover {border-style:solid; border-color:red; border-left-width:9px; background-color:#bef0fe; color:#000099;}");
            sl.AppendLine(".dtnone {background-color:#e0e0e0;}");
            sl.AppendLine(".dtnone:hover {background-color:#b2b2b2;}");
            sl.AppendLine(".dtnull {background-color:#a0a0a0;}");
            sl.AppendLine(".dtnull:hover {background-color:#808080;}");
            sl.AppendLine(".dtemptyparam {background-color:#2d2d86;color:lightgray;}");
            sl.AppendLine(".dtemptyparam:hover {background-color:#4040bf;color:white;}");
            sl.AppendLine(".dtint {background-color:#ffffc1;}");
            sl.AppendLine(".dtint:hover {background-color:#dfdf73;}");
            sl.AppendLine(".dtfpoint {background-color:#dbdbff;}");
            sl.AppendLine(".dtfpoint:hover {background-color:#c1c1e1;}");
            sl.AppendLine(".dtbool {background-color:#ffd6d6;}");
            sl.AppendLine(".dtbool:hover {background-color:#e0aaaa;}");
            sl.AppendLine(".dtdate {background-color:#bcffbc;}");
            sl.AppendLine(".dtdate:hover {background-color:#a5e0a5;}");
            sl.AppendLine(".dtidisp {background-color:#ffcc00;}");
            sl.AppendLine(".dtidisp:hover {background-color:#ff9933;}");
            sl.AppendLine("div#hintdiv {display:none; position:absolute;}");
            sl.AppendLine("td#hint {border: 3px solid black; border-radius: 5px; background-color:black; font-family:\"Lucida Console\",\"Courier New\"; color:white; font-size:9pt; padding:2px;}");
            sl.AppendLine("-->");
            sl.AppendLine("</style>");
            sl.AppendLine("</head><body onload=\"bypassInternetExplorer(); \" onmousemove=\"sh(event)\">");
            sl.AppendLine("<div id=\"hintdiv\"><table cellpadding=0 cellspacing=0 border=0><tr><td id=\"hint\"></td></tr></table></div>");
            sl.AppendLine("<noscript>");
            sl.AppendLine("<style type=\"text/css\">");
            sl.AppendLine("<!--");
            sl.AppendLine(".plus {display:none;}");
            sl.AppendLine(".minus {display:none;}");
            sl.AppendLine("-->");
            sl.AppendLine("</style>");
            sl.AppendLine("</noscript>");
            sl.AppendLine("<tt id=\"plusminus\" doexp=\"1\" onclick=\"ecall()\">[+ all]</tt>");
            if (!TUniVar.VarIsArray(data))
                sl.AppendLine("<table cellpadding=0 cellspacing=0 style=\"display:table;\"><tr>");

            OV2HTMLinternal(sl, data, true);

            if (!TUniVar.VarIsArray(data))
                sl.AppendLine("</tr></table>");
            sl.AppendLine("</body></html>");

            if (createFile)
            {
                if (fileName.Length == 0)
                {
                    fileName = Path.Combine(DDTEMP(), string.Concat("ov2html_", Stopwatch.GetTimestamp().ToString("X"), fileSuffix, ".html"));
                }

                try
                {
                    File.WriteAllText(fileName, sl.ToString());
                    return fileName; //Path
                }
                catch
                {
                    return ""; //Path
                }
            }
            else
                return sl.ToString(); //Body
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static void OV2HTMLinternal(StringBuilder sl, object data, bool topLevel=false)
        {
            //Local function
            string UnHTMLEx(string input, ref bool nulls)
            {
                string txt = input.Replace("&", "&amp;").Replace("<", "&lt;");

                nulls = txt.IndexOf((char)0x0) > 0;
                if (nulls)
                {
                    txt = txt.Replace((char)0x0, ' '); //2020-03-25 [PP] to remove those damn #0 from string so they display somehow correctly...
                }

                return txt;
            }
            //Local function
            VarType Type2VarType(Type type)
            {
                if (type.Equals(typeof(int)))
                    return VarType.varInteger;
                if (type.Equals(typeof(short)))
                    return VarType.varShortInt;
                else if (type.Equals(typeof(long)))
                    return VarType.varInt64;
                else if (type.Equals(typeof(byte)))
                    return VarType.varByte;
                else if (type.Equals(typeof(DateTime)))
                    return VarType.varDate;
                else if (type.Equals(typeof(float)))
                    return VarType.varDouble;
                else if (type.Equals(typeof(double)))
                    return VarType.varDouble;
                else if (type.Equals(typeof(decimal)))
                    return VarType.varDouble;
                else if (type.Equals(typeof(bool)))
                    return VarType.varBoolean;
                else if (type.Equals(typeof(char)))
                    return VarType.varString;
                else if (type.Equals(typeof(string)))
                    return VarType.varString;
                else if (type.IsInterface || type.IsCOMObject)
                    return VarType.varDispatch;

                return VarType.varUnknown;
            }
            //Local function
            string ByteArrayToHexString(byte[] ba)
            {
                StringBuilder hex = new StringBuilder(ba.Length * 2);
                hex.Append("0x");
                foreach (byte b in ba)
                    hex.AppendFormat("{0:x2}", b);
                return hex.ToString();
            }

            string s;
            string sclass = "";
            string img = "";
            string et = "";
            bool _nulls=false;
            int _itemId = 0;

            Type _type = TUniVar.VarIsNullOrEmpty(data) ? null : data.GetType();
            VarType _vtype = _type == null ? VarType.varUnknown : Type2VarType(_type);

            switch (TUniVar.VarIsArray(data) ? ((Array)data).Rank : 0)
            {
                case 0: //simple
                    if (data == null)
                    {
                        sclass = " class=\"dtemptyparam\"";
                        s = "&nbsp;";
                    }
                    else if (data is DBNull)
                    {
                        sclass = " class=\"dtnull\"";
                        s = "&nbsp;";
                    }
                    /*
                    else if VarIsMissing(ovx) then
                        s := '<td class="dtemptyparam" tip="0x0A">&nbsp;empty&nbsp;'
                    */
                    else if (_vtype == VarType.varUnknown)
                    {
                        sclass = " class=\"dtunknown\"";
                        s = _type.FullName;
                    }
                    else
                    {
                        s = UnHTMLEx(TUniVar.VarToStr(data), ref _nulls);
                        if (s.Length == 0)
                            s = "&nbsp;";

                        if (_type.IsInterface || _type.IsCOMObject)
                        {
                            sclass = " class=\"dtidisp\"";
                            s = "IDispatch";
                        }
                        else if (_type.Equals(typeof(int)) || _type.Equals(typeof(long)) ||
                                 _type.Equals(typeof(short)) || _type.Equals(typeof(byte)))
                        {
                            sclass = " class=\"dtint\"";
                        }
                        else if (_type.Equals(typeof(float)) || _type.Equals(typeof(double)) ||
                                 _type.Equals(typeof(decimal)))
                        {
                            sclass = " class=\"dtfpoint\"";
                        }
                        else if (_type.Equals(typeof(bool)))
                        {
                            sclass = " class=\"dtbool\"";
                        }
                        else if (_type.Equals(typeof(DateTime)))
                        {
                            sclass = " class=\"dtdate\"";
                            s = TUniVar.VarToDateTime(data).ToString();
                        }
                        else if (_nulls)
                        {
                            sclass = " class=\"dtstr0\"";
                        }
                        else
                        {
                            sclass = " class=\"dtstr\"";
                        }
                    }

                    s = string.Concat("<td", sclass, " tip=\"0x", _vtype.ToString("X"), "\">", s, "</td>");

                    sl.AppendLine(s);
                    break;
                case 1: //vector
                    _itemId++;

                    var v1data = (object[])data;

                    if (_vtype == VarType.varUnknown)
                    {
                        _type = v1data[0].GetType();
                        _vtype = Type2VarType(_type);
                    }

                    if (topLevel)
                        s = "d1m";
                    else
                    {
                        s = "d1";
                        img = string.Concat("<img class=\"minus\" id=\"i_", _itemId.ToString(), "\" onclick=\"sw(this)\">");
                    }

                    if (_vtype != VarType.varByte)
                        et = "et=\"" + Enum.GetName(typeof(VarType), _vtype).Substring(3).ToLower() + "\" ";

                    sl.AppendLine(string.Concat(img, "<table class=\"", s, "\" id=\"t_", _itemId.ToString(), "\" ", et, "border=1 cellpadding=3><tr>"));

                    if (_vtype == VarType.varByte)
                    {
                        sl.AppendLine(string.Concat("\"<td id=\"tx_", _itemId.ToString(), "\" descr=\"byte array (", (v1data.GetUpperBound(0)-v1data.GetLowerBound(0)+1).ToString(), " bytes)\">"));
                        s = ByteArrayToHexString((byte[])data);
                        if (s.Length > 80)
                        {
                            var i = s.Length / 80;
                            if ((i * 80) < s.Length)
                                i++;
                            s = string.Concat("\"<textarea style=\"overflow: auto;\" readonly=\"readonly\" rows=\"", i.ToString(), "\" cols=\"80\">", s, "\"</textarea>");
                        }

                        sl.AppendLine(s);
                        sl.AppendLine("</td>");
                    }
                    else
                    {
                        for (var x = v1data.GetLowerBound(0); x <= v1data.GetUpperBound(0); x++)
                        {
                            var ov = v1data[x];

                            if (TUniVar.VarIsArray(ov))
                            {
                                sl.AppendLine("<td>");
                                OV2HTMLinternal(sl, ov);
                                sl.AppendLine("</td>");
                            }
                            else
                                OV2HTMLinternal(sl, ov);
                        }
                    }

                    sl.AppendLine("\"</tr></table>");

                    break;
                case 2: //Table
                    _itemId++;

                    var v2data = (object[,])data;

                    if (topLevel)
                        s = "d2m";
                    else
                    {
                        s = "d2";
                        img = string.Concat("<img class=\"plus\" id=\"i_", _itemId.ToString(), "\" onclick=\"sw(this)\">");
                    }

                    et = string.Concat("et=\"", Enum.GetName(typeof(VarType), _vtype).Substring(3).ToLower(), "\" ");
                    sl.AppendLine(string.Concat(img, "<table class=\"", s, "\" id=\"t_", _itemId.ToString(), "\" ", et, "border=1 cellpadding=3>"));

                    for (var x=v2data.GetLowerBound(0); x<=v2data.GetUpperBound(0); x++)
                    {
                        sl.AppendLine("<tr>");
                        for (var y=v2data.GetLowerBound(1); y<=v2data.GetUpperBound(1); y++)
                        {
                            var ov = v2data[x, y];
                            if (TUniVar.VarIsArray(ov))
                            {
                                sl.AppendLine("<td>");
                                OV2HTMLinternal(sl,ov);
                                sl.AppendLine("</td>");
                            }
                            else
                                OV2HTMLinternal(sl,ov);
                        }
                        sl.AppendLine("</tr>");
                    }
                    sl.AppendLine("</table>");

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Logging to table
        /// </summary>
        /// <returns></returns>
        public static int UpdateSysLog(string AccessCode, int ComPlusID, int TransId, int UserId, string TableName,
            string ComPlusName, string MethodName, string Description, int MandantId = 0, int LogTypeId = 0, int ModulId = 0)
        {
            if (TableName.Length > 0)
            {
                ComWrapper comObj = null;
                try
                {
                    string pk = $"{TableName}Id";
                    string sql = $"SELECT * FROM {TableName} WHERE {pk} = -1";

                    int result = ComUtils.OpenResultset(AccessCode, sql, pk, -1, -1, out comObj);
                    if (result < 0)
                        return result;

                    object[] fields = new object[]
                    {
                    "ModulId",
                    "ComName",
                    "MethodName",
                    "MTSComId",
                    "AccessCode",
                    "TransactionId",
                    "ProcessId",
                    "ThreadId",
                    "Description",
                    "UserId",
                    "mulMandantId",
                    "logTypeId"
                    };

                    object[] values = new object[]
                    {
                    ModulId,
                    ComPlusName,
                    MethodName,
                    ComPlusID,
                    AccessCode,
                    TransId,
                    TUniTools.ProcessId,
                    #pragma warning disable CS0618 // Type or member is obsolete
                    AppDomain.GetCurrentThreadId(),
                    #pragma warning restore CS0618 // Type or member is obsolete
                    Description,
                    UserId,
                    MandantId,
                    LogTypeId
                    };

                    return ComUtils.RecordNew(ref comObj, fields, values, pk);
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
            else
                return 0;
        }
    }
}
