// ***********************************************************************
// Assembly         : Component
// Author           : Artur Maciejowski
// Created          : 16-02-2020
//
// Last Modified By : Artur Maciejowski
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="ComLogger.cs" company="DomConsult Sp. z o.o.">
//     Copyright ©  2021 All rights reserved
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DomConsult.GlobalShared.Utilities
{
    /// <summary>
    /// Class ComLogger.
    /// </summary>
    public class ComLogger
    {
        /// <summary>
        /// The application
        /// </summary>
        public static bool APPLICATION = false;

        /// <summary>
        /// The this lock
        /// </summary>
        private object thisLock = new object();
        /// <summary>
        /// Gets or sets the log file prefix.
        /// </summary>
        /// <value>The log file prefix.</value>
        public string LogFilePrefix { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [allow debug].
        /// </summary>
        /// <value><c>true</c> if [allow debug]; otherwise, <c>false</c>.</value>
        public bool AllowDebug { get; set; }
        /// <summary>
        /// Gets or sets the log dir.
        /// </summary>
        /// <value>The log dir.</value>
        public string LogDir { get; set; }
        /// <summary>
        /// Gets or sets the name of the log file.
        /// </summary>
        /// <value>The name of the log file.</value>
        public string LogFileName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [add date to log].
        /// </summary>
        /// <value><c>true</c> if [add date to log]; otherwise, <c>false</c>.</value>
        public bool AddDateToLog { get; set; }

        /// <summary>
        /// The m debug
        /// </summary>
        private List<string> m_debug = new List<string>();
        /// <summary>
        /// The m stop watch
        /// </summary>
        private Stopwatch m_StopWatch = new Stopwatch();

        /// <summary>
        /// Initializes a new instance of the <see cref="ComLogger"/> class.
        /// </summary>
        /// <param name="allowDebugLog">if set to <c>true</c> [allow debug log].</param>
        public ComLogger(bool allowDebugLog)
        {
            AllowDebug = allowDebugLog;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComLogger"/> class.
        /// </summary>
        /// <param name="allowDebugLog">if set to <c>true</c> [allow debug log].</param>
        /// <param name="logFilePrefix">The log file prefix.</param>
        public ComLogger(bool allowDebugLog, string logFilePrefix): this(allowDebugLog)
        {
            if(string.IsNullOrEmpty(logFilePrefix))
            {
                try
                {
                    System.Reflection.AssemblyTitleAttribute title = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), false)[0] as System.Reflection.AssemblyTitleAttribute;
                    logFilePrefix = title.Title;
                }
                catch { }
            }
            else
            {
                LogFilePrefix = logFilePrefix;
            }

            LogFileName = string.Format("{0}_{1}.log", LogFilePrefix, DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fff"));   
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComLogger"/> class.
        /// </summary>
        /// <param name="allowDebugLog">if set to <c>true</c> [allow debug log].</param>
        /// <param name="logFilePrefix">The log file prefix.</param>
        /// <param name="logDir">The log dir.</param>
        public ComLogger(bool allowDebugLog, string logFilePrefix, string logDir):this(allowDebugLog,logFilePrefix)
        {
            LogDir = logDir;
        }

        /// <summary>
        /// Resets the timer.
        /// </summary>
        public void ResetTimer()
        {
            m_StopWatch.Reset();
        }

        /// <summary>
        /// Prints the debug.
        /// </summary>
        /// <returns>System.String.</returns>
        public string PrintDebug()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m_debug.Count; i++)
            {
                sb.AppendLine(m_debug[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="addElapsedTime">if set to <c>true</c> [add elapsed time].</param>
        public void Add(string message, bool addElapsedTime = true)
        {
            try
            {
                m_StopWatch.Stop();
                
                if (AddDateToLog)
                    m_debug.Add(DateTime.Now.ToString());

                string elapsedTime = "";
                if (addElapsedTime)
                    elapsedTime = string.Format("ElapsedTime[ms]:{0} - ", m_StopWatch.ElapsedMilliseconds);

                m_debug.Add(string.Format("{0}{1}", elapsedTime, message));
                m_StopWatch.Start();
            }
            catch { }
        }

        /// <summary>
        /// Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void Add(string message, params object[] args)
        {
            AddFormat(message, args);
        }

        /// <summary>
        /// Adds the format.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public void AddFormat(string message, params object[] args)
        {
            try
            {
                m_StopWatch.Stop();
                string _msg = string.Format(message, args);
                
                if (AddDateToLog)
                    m_debug.Add(DateTime.Now.ToString());
                
                m_debug.Add(string.Format("ElapsedTime[ms]:{0} - {1}", m_StopWatch.ElapsedMilliseconds, _msg));
                m_StopWatch.Start();
            }
            catch { }
        }

        /// <summary>
        /// Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void Add(string message,Exception ex)
        {
            Add(message);
            Add(ex);
        }

        /// <summary>
        /// Adds the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void Add(Exception ex)
        {
            try
            {
                m_StopWatch.Stop();
                
                if (AddDateToLog)
                    m_debug.Add(DateTime.Now.ToString());

                m_debug.Add(string.Format("ElapsedTime[ms]:{0} - {1}", m_StopWatch.ElapsedMilliseconds, "Exception occured:"));
                m_debug.Add(ex.StackTrace);
                m_debug.Add(ex.Message);
                m_debug.Add("Exception type: "+ex.GetType().FullName);
                while(ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    m_debug.Add(ex.Message);
                }
                m_StopWatch.Start();
            }
            catch { }
        }

        /// <summary>
        /// Saves the log.
        /// </summary>
        public void SaveLog()
        {
            try
            {
                if (AllowDebug)
                {
                    if (string.IsNullOrEmpty(LogDir))
                    {
                        string tempPath = System.IO.Path.GetTempPath();
                        try
                        {
                            string key = "";
                            if (APPLICATION)
                            {
                                key = @"LogDirectoryForApp";
                                //LogDir = ComUtils.GetRegParam("LogDirectoryForApp", tempPath, "LOGON");

                            }
                            else
                            {
                                key = @"LogDirectory";
                                //LogDir = ComUtils.GetRegParam("LogDirectory", tempPath, "LOGON");
                            }

                            object value = TuniGlobalCache.GetSysRegParam("LOGON", key, tempPath, false);
                            LogDir = TUniVar.VarToStr(value);
                        }
                        catch(Exception ex)
                        {
                            LogDir = tempPath;
                            Add("Error occured while getting logdir", ex);
                        }

                    }

                    if (string.IsNullOrEmpty(LogFileName))
                    {
                        LogFileName = string.Format("{0}_{1}.log", LogFilePrefix, DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fff"));
                    
                    }

                    string logFilePath = System.IO.Path.Combine(LogDir, LogFileName);
                    lock(thisLock)
                    {
                        System.IO.File.AppendAllText(logFilePath, PrintDebug());
                    }
                }
                m_debug.Clear();
            }
            catch { }
        }
    }
}
