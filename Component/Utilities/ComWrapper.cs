// ***********************************************************************
// Assembly         : Component
// Author           : Artur Maciejowski
// Created          : 16-02-2020
//
// Last Modified By : Artur Maciejowski
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="ComWrapper.cs" company="DomConsult Sp. z o.o.">
//     Copyright ©  2021 All rights reserved
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DomConsult.GlobalShared.Utilities
{
    /// <summary>
    /// Class ComWrapper.
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class ComWrapper : IDisposable
    {
        /// <summary>
        /// The application
        /// </summary>
        public static bool APPLICATION = false;

        /// <summary>
        /// The COM object
        /// </summary>
        private object comObject;
        /// <summary>
        /// The COM type
        /// </summary>
        private Type comType;

        /// <summary>
        /// Gets a value indicating whether this <see cref="ComWrapper"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        [XmlIgnore]
        public bool Connected { get; private set; }

        /// <summary>
        /// Class Transaction.
        /// </summary>
        public class Transaction
        {
            /// <summary>
            /// The identifier
            /// </summary>
            public int Id;
            /// <summary>
            /// The COM wrapper
            /// </summary>
            public ComWrapper ComWrapper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComWrapper"/> class.
        /// </summary>
        public ComWrapper()
        {
            Connected = false;
            comObject = null;
            comType = null;
        }

        #region Local variables
        /// <summary>
        /// The m server name
        /// </summary>
        private string m_serverName = "localhost";
        /// <summary>
        /// The m server application path
        /// </summary>
        private string m_serverAppPath = String.Empty;
        /// <summary>
        /// The m server log path
        /// </summary>
        private string m_serverLogPath = String.Empty;
        /// <summary>
        /// The m access code
        /// </summary>
        private string m_accessCode = String.Empty;
        /// <summary>
        /// The m database identifier
        /// </summary>
        private int m_dbId = -1;
        /// <summary>
        /// The m session identifier
        /// </summary>
        private int m_sessionId = -1;
        /// <summary>
        /// The m user identifier
        /// </summary>
        private int m_userId = -1;
        /// <summary>
        /// The m language identifier
        /// </summary>
        private int m_languageId = 1;
        /// <summary>
        /// The m deployment identifier
        /// </summary>
        private int m_deploymentId = 1;
        /// <summary>
        /// The m is admin
        /// </summary>
        private bool m_isAdmin = false;
        //private int transactionId;
        /// <summary>
        /// The m transaction
        /// </summary>
        private Transaction m_transaction;
        /// <summary>
        /// The m unique identifier
        /// </summary>
        private string m_GUID = "";

        #endregion

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID
        {
            get { return m_GUID; }
        }

        /// <summary>
        /// Gets or sets the access code.
        /// </summary>
        /// <value>The access code.</value>
        public string AccessCode
        {
            get { return m_accessCode; }
            set { setAccessCode(value); }
        }

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        public string ServerName
        {
            get {
                return APPLICATION ? m_serverName : "localhost";
            }
            set { m_serverName = value; }
        }

        /// <summary>
        /// Gets or sets the server application path.
        /// </summary>
        /// <value>The server application path.</value>
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

        /// <summary>
        /// Gets the server log path.
        /// </summary>
        /// <value>The server log path.</value>
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

        /// <summary>
        /// Gets the database identifier.
        /// </summary>
        /// <value>The database identifier.</value>
        public int DBId
        {
            get { return m_dbId; }
        }

        /// <summary>
        /// Gets the session identifier.
        /// </summary>
        /// <value>The session identifier.</value>
        public int SessionId
        {
            get { return m_sessionId; }
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId
        {
            get { return m_userId; }
        }

        /// <summary>
        /// Gets the language identifier.
        /// </summary>
        /// <value>The language identifier.</value>
        public int LanguageId
        {
            get { return m_languageId; }
        }

        /// <summary>
        /// Gets the deployment identifier.
        /// </summary>
        /// <value>The deployment identifier.</value>
        public int DeploymentId
        {
            get { return m_deploymentId; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is admin.
        /// </summary>
        /// <value><c>true</c> if this instance is admin; otherwise, <c>false</c>.</value>
        public bool IsAdmin
        {
            get { return m_isAdmin; }
        }

        /// <summary>
        /// Sets the admin.
        /// </summary>
        public void SetAdmin()
        {
            m_isAdmin = true;
        }

        /// <summary>
        /// Sets the admin.
        /// </summary>
        /// <param name="ForceAdmin">if set to <c>true</c> [force admin].</param>
        public void SetAdmin(bool ForceAdmin)
        {
            m_isAdmin = ForceAdmin;
        }

        /// <summary>
        /// Sets the access code.
        /// </summary>
        /// <param name="AccessCode">The access code.</param>
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

        /// <summary>
        /// Gets or sets the transaction object.
        /// </summary>
        /// <value>The transaction object.</value>
        public Transaction TransactionObject
        {
            get { return m_transaction; }
            set { m_transaction = value; }
        }

        /// <summary>
        /// Assigns the access code.
        /// </summary>
        /// <param name="ACC">The acc.</param>
        /// <returns>System.Int32.</returns>
        public int AssignAccessCode(string ACC = "")
        {
            object[] arguments = new object[1];
            arguments[0] = ACC.Length > 0 ? ACC : AccessCode;
            object res = InvokeMethod("AssignAccessCode", arguments, new bool[] { false });
            return res == null ? 0 : (int)res;
        }

        /// <summary>
        /// Connects the specified class name.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Connects the specified CLSID.
        /// </summary>
        /// <param name="clsid">The CLSID.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Connects the specified class identifier.
        /// </summary>
        /// <param name="classID">The class identifier.</param>
        /// <param name="COMPartition">The COM partition.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Connects the remote.
        /// </summary>
        /// <param name="classID">The class identifier.</param>
        /// <param name="serverNameOrIP">The server name or ip.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Disconnect();
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
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

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>System.Object.</returns>
        public object InvokeMethod(string methodName)
        {
            return InvokeMethod(methodName, null, null);
        }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="argsByRef">The arguments by reference.</param>
        /// <returns>System.Object.</returns>
        public object InvokeMethod(string methodName, object[] arguments, bool[] argsByRef)
        {
            string exception = "";
            return InvokeMethod(methodName, arguments, argsByRef, ref exception);
        }

        /// <summary>
        /// Invokes the method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="argsByRef">The arguments by reference.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>System.Object.</returns>
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

        /// <summary>
        /// Gets the last error description.
        /// </summary>
        /// <param name="wmk">The WMK.</param>
        /// <returns>System.Int32.</returns>
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

        /// <summary>
        /// Gets the last error description.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal void GetLastErrorDescription()
        {
            throw new NotImplementedException();
        }
    }
}