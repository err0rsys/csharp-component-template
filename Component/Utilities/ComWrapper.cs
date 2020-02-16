using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DomConsult.GlobalShared.Utilities
{
    public class ComWrapper : IDisposable
    {
        public static bool APPLICATION = false;

        private object comObject;
        private Type comType;

        [XmlIgnore]
        public bool Connected { get; private set; }

        public class Transaction
        {
            public int Id;
            public ComWrapper ComWrapper;
        }

        public ComWrapper()
        {
            Connected = false;
            comObject = null;
            comType = null;
        }

        #region Local variables
        private string m_serverName = "localhost";
        private string m_serverAppPath = String.Empty;
        private string m_serverLogPath = String.Empty;
        private string m_accessCode = String.Empty;
        private int m_dbId = -1;
        private int m_sessionId = -1;
        private int m_userId = -1;
        private int m_languageId = 1;
        private int m_deploymentId = 1;
        private bool m_isAdmin = false;
        //private int transactionId;
        private Transaction m_transaction;
        private string m_GUID = "";

        #endregion

        public string GUID
        {
            get { return m_GUID; }
        }

        public string AccessCode
        {
            get { return m_accessCode; }
            set { setAccessCode(value); }
        }

        public string ServerName
        {
            get {
                return APPLICATION ? m_serverName : "localhost";
            }
            set { m_serverName = value; }
        }

        public string ServerAppPath
        {
            get
            {
                if (m_serverAppPath.Length == 0)
                {

                    RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(ComUtils.CS_REGKEY_ROOT);

                    // searching for alternative registry key
                    if (!ComUtils.Assigned(RegKey))
                        RegKey = Registry.CurrentUser.OpenSubKey(ComUtils.CS_REGKEY_ALTR);

                    try
                    {
                        m_serverAppPath = (string)RegKey.GetValue(ComUtils.CS_REG_SRVPATH);
                    }
                    finally
                    {
                        RegKey.Close();
                    }

                }
                return m_serverAppPath;
            }
            set { m_serverAppPath = value; }
        }

        public string ServerLogPath
        {
            get
            {
                if (m_serverLogPath.Length == 0)
                {
                    m_serverLogPath = ComUtils.GetParam(
                        AccessCode, "LOG_PATH", String.Empty);

                    if (m_serverLogPath.Length == 0)
                        m_serverLogPath = ComUtils.GetRegParam(
                            "LogDirectory", String.Empty, AccessCode);

                    if (m_serverLogPath.Length > 0)
                    {
                        string subDir = ComUtils.GetParam(
                            AccessCode, "LOG_ERRORS", String.Empty);

                        if (subDir.Length == 0)
                            subDir = ComUtils.GetParam(
                                AccessCode, "LOG_ALL", String.Empty);

                        if (subDir.Length > 0)
                            m_serverLogPath = Path.Combine(m_serverLogPath, subDir);
                    }
                    else
                        m_serverLogPath = Path.GetTempPath();

                    if (!Directory.Exists(m_serverLogPath))
                        Directory.CreateDirectory(m_serverLogPath);
                }
                return m_serverLogPath;
            }
        }

        public int DBId
        {
            get { return m_dbId; }
        }

        public int SessionId
        {
            get { return m_sessionId; }
        }

        public int UserId
        {
            get { return m_userId; }
        }

        public int LanguageId
        {
            get { return m_languageId; }
        }

        public int DeploymentId
        {
            get { return m_deploymentId; }
        }

        public bool IsAdmin
        {
            get { return m_isAdmin; }
        }

        public void SetAdmin()
        {
            m_isAdmin = true;
        }

        public void SetAdmin(bool ForceAdmin)
        {
            m_isAdmin = ForceAdmin;
        }

        private void setAccessCode(string AccessCode)
        {
            m_accessCode = AccessCode;

            Dictionary<string, string> ac = ComUtils.decodeInputString(AccessCode, "/");

            if (ac.ContainsKey("QID"))
                m_sessionId = Convert.ToInt32(ac["QID"]);
            else
                m_sessionId = -1;

            if (ac.ContainsKey("DBID"))
                m_dbId = Convert.ToInt32(ac["DBID"]);
            else
                m_dbId = -1;

            if (ac.ContainsKey("L"))
                m_languageId = Convert.ToInt32(ac["L"]);
            else
                m_languageId = 1;

            m_isAdmin = (ac.ContainsKey("ADM"));

            if (ac.ContainsKey("UID"))
                m_userId = Convert.ToInt32(ac["UID"]);
            else
                m_userId = -1;

            if (ac.ContainsKey("KDID"))
                m_deploymentId = Convert.ToInt32(ac["KDID"]);
            else
                if (ac.ContainsKey("KID"))
                    m_deploymentId = Convert.ToInt32(ac["KID"]);
                else
                    m_deploymentId = -1;

            if (ac.ContainsKey("CSN"))
                m_serverName = Convert.ToString(ac["CSN"]);
            else
                if (m_serverName.Length == 0)
                    m_serverName = "localhost";
        }

        public Transaction TransactionObject
        {
            get { return m_transaction; }
            set { m_transaction = value; }
        }

        public int AssignAccessCode(string ACC = "")
        {
            object[] arguments = new object[1];
            arguments[0] = ACC.Length > 0 ? ACC : AccessCode;
            object res = InvokeMethod("AssignAccessCode", arguments, new bool[] { false });
            return res == null ? 0 : (int)res;
        }

        public bool Connect(string className)
        {
            try
            {
                comType = Type.GetTypeFromProgID(className, true);
                m_GUID = comType.GUID.ToString();
                return ConnectRemote(comType.GUID, ServerName);
            }
            catch (COMException ex)
            {
                m_GUID = "";
                System.Diagnostics.Debug.WriteLine("ComWrapper.Connect error: ", ex.Message);
                throw;
            }
        }

        public bool Connect(Guid clsid)
        {
            try
            {
                m_GUID = clsid.ToString();
                return ConnectRemote(clsid, ServerName);
            }
            catch (COMException ex)
            {
                m_GUID = "";
                System.Diagnostics.Debug.WriteLine("ComWrapper.Connect error: ", ex.Message);
                throw;
            }
        }

        public bool Connect(Guid classID, Guid COMPartition)
        {
            try
            {
                if (comType != null || comObject != null)
                    Disconnect();

                string moniker;

                if (COMPartition == Guid.Empty)
                {
                    moniker = string.Format("new:{0}", classID.ToString("B"));
                }
                else
                {
                    moniker = string.Format("partition:{0}/new:{1}", COMPartition.ToString("B"), classID.ToString("B"));
                }

                comObject = Marshal.BindToMoniker(moniker);
                //comType = Type.GetTypeFromCLSID(classID); W tym przypadku to nie działa prawidłowo!!!
                m_GUID = classID.ToString();
                comType = Type.GetTypeFromCLSID(classID, "127.0.0.1", true); //Tylko localhost
                Connected = true;
                return true;

            }
            catch (COMException ex)
            {
                m_GUID = "";
                System.Diagnostics.Debug.WriteLine("ComWrapper.Connect error: ", ex.Message);
                throw;
            }
        }

        public bool ConnectRemote(Guid classID, string serverNameOrIP)
        {
            if (string.Equals(serverNameOrIP, "127.0.0.1") || string.Equals(serverNameOrIP, "localhost"))
            {
                return Connect(classID, Guid.Empty);
            }
            else
            {
                try
                {
                    if (comType != null || comObject != null)
                        Disconnect();
                    comType = Type.GetTypeFromCLSID(classID, serverNameOrIP, true);
                    m_GUID = classID.ToString();
                    comObject = Activator.CreateInstance(comType);
                    Connected = true;
                    return true;
                }
                catch (COMException ex)
                {
                    m_GUID = "";
                    System.Diagnostics.Debug.WriteLine("ComWrapper.ConnectRemote error: " + ex.Message);
                    throw;
                }
            }
    }

        public void Dispose()
        {
            Disconnect();
        }

        public void Disconnect()
        {
            try
            {
                m_GUID = "";
                //if (comObject != null) - http://www.dotnetperls.com/tester-doer
                Marshal.FinalReleaseComObject(comObject);
            }
            catch { }
            finally
            {
                comObject = null;
                comType = null;
                Connected = false;
            }
        }

        public object InvokeMethod(string methodName)
        {
            return InvokeMethod(methodName, null, null);
        }

        public object InvokeMethod(string methodName, object[] arguments, bool[] argsByRef)
        {
            string exception = "";
            return InvokeMethod(methodName, arguments, argsByRef, ref exception);
        }

        public object InvokeMethod(string methodName, object[] arguments, bool[] argsByRef, ref string exception)
        {
            if (arguments != null)
            {
                int length = ((arguments.Length >= argsByRef.Length) ? argsByRef.Length : arguments.Length);
                ParameterModifier modifier = new ParameterModifier(length);
                for (int i = 0; i < length; i++)
                {
                    modifier[i] = argsByRef[i];
                }
                return comType.InvokeMember(methodName, BindingFlags.InvokeMethod, null, comObject, arguments, new ParameterModifier[] { modifier }, null, null);
            }
            else
            {
                return comType.InvokeMember(methodName, BindingFlags.InvokeMethod, null, comObject, null, null, null, null);
            }
        }

        public int GetLastErrorDescription(out object wmk)
        {
            int res = 0;
            wmk = null;

            if (comObject == null)
                return res;

            object result = null;
            string _method = "GetLastErrorDescription";
            object[] _arguments = new object[1];
            _arguments[0] = wmk;

            try
            {
                result = InvokeMethod(_method, _arguments, new bool[] { true });
            }
            catch {}

            if (result == null)
                return res;

            int.TryParse(result.ToString(), out res);
            return res;
        }

        internal void GetLastErrorDescription()
        {
            throw new NotImplementedException();
        }
    }
}