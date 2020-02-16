using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DomConsult.GlobalShared.Utilities
{
    public class ComLogger
    {
        public static bool APPLICATION = false;

        private object thisLock = new object();
        public string LogFilePrefix { get; set; }
        public bool AllowDebug { get; set; }
        public string LogDir { get; set; }
        public string LogFileName { get; set; }
        public bool AddDateToLog { get; set; }

        private List<string> m_debug = new List<string>();
        private Stopwatch m_StopWatch = new Stopwatch();
        
        public ComLogger(bool allowDebugLog)
        {
            AllowDebug = allowDebugLog;
        }

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

        public ComLogger(bool allowDebugLog, string logFilePrefix, string logDir):this(allowDebugLog,logFilePrefix)
        {
            LogDir = logDir;
        }

        public void ResetTimer()
        {
            m_StopWatch.Reset();
        }

        public string PrintDebug()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < m_debug.Count; i++)
            {
                sb.AppendLine(m_debug[i]);
            }
            return sb.ToString();
        }

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

        public void Add(string message, params object[] args)
        {
            AddFormat(message, args);
        }

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

        public void Add(string message,Exception ex)
        {
            Add(message);
            Add(ex);
        }

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
