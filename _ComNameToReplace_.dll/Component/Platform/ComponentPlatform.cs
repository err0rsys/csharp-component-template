using System;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Runtime.InteropServices;
using DomConsult.GlobalShared.Utilities;

namespace DomConsult.Platform
{
    /// <summary>
    /// Class with constants used in ComponentPlatform unit.
    /// </summary>
    public static class ComponentPlatformDef
    {
        /// <summary>
        /// Error number indicating missing transaction id.
        /// </summary>
        public static int ERR_MISSING_TRANSACTION = -3;
        /// <summary>
        /// Error number indicating missing transaction manager
        /// </summary>
        public static int ERR_MISSING_TRANSMANAGER = -4;
    }

    /// <summary>
    /// Transaction statuses
    /// </summary>
    public enum TTransactionStatus
    {
        /// <summary>
        /// Commit of the current transaction is needed
        /// </summary>
        ctsCommitEnabled = 0,
        /// <summary>
        /// Commit of the current transaction should be ignored
        /// </summary>
        ctsCommitIgnore = 1,
        /// <summary>
        /// Rollback of the current transaction is required
        /// </summary>
        ctsRollbackRequired = 2
    }

    /// <summary>
    /// Class ComponentPlatform responsible for transaction management.
    /// </summary>
#if TEST
    public abstract class ComponentPlatform : IDisposable
#else
    public abstract class ComponentPlatform: ServicedComponent
#endif
    {
        private dynamic _TransactionManager = null;
        private int _TransactionId = ComponentPlatformDef.ERR_MISSING_TRANSACTION;
        private bool _disposed = false;
        private string _mtsComName = "";
        private string _accessCode = "";

        /// <summary>
        /// Logger object
        /// </summary>
        public ComLogger Logger { get; internal set; } = new ComLogger(false);

        /// <summary>
        /// Unique ID of component instance.
        /// </summary>
        public Guid InstanceID { get; internal set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the access code.
        /// </summary>
        /// <value>The access code.</value>
        public string AccessCode
        {
            get
            {
                return _accessCode;
            }

            set
            {
                _accessCode = value;
                Logger.AllowDebug = _accessCode.Contains("/LOG_ALL=", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        /// <summary>
        /// Gets or sets AppID of current component
        /// </summary>
        public string MtsComName
        {
            get
            {
                return _mtsComName;
            }
            set
            {
                _mtsComName = value;
                Logger.LogFilePrefix = _mtsComName.Contains(".") ? _mtsComName.Substring(1, _mtsComName.IndexOf(".")) : _mtsComName;
            }
        }

        /// <summary>
        /// Gets or sets transaction status
        /// </summary>
        public TTransactionStatus TransactionStatus { get; set; }

        /// <summary>
        /// Id of external transaction. You can't commit or rollback this transation!
        /// </summary>
        public int ExTransactionId { get; set; } = -1;

        /// <summary>
        /// Automatically creates new transaction.
        /// Form formular transaction is managed by aplication according to TransactionStatus.
        /// In other purposes You need to manage transaction by Yourself using proprietary methods.
        /// </summary>
        public int TransactionId
        {
            get {
                if (ExTransactionId > 0)
                {
                    return ExTransactionId;
                }

                if (_TransactionManager == null)
                {
                    Type DbcTransType = Type.GetTypeFromProgID("DBC.Trans", true);
                    _TransactionManager = Activator.CreateInstance(DbcTransType);
                    _TransactionManager.AssignAccessCode(AccessCode);

                    try
                    {
                        _TransactionManager.AppName("COM:" + MtsComName);
                    }
                    catch { }
                }

                if (_TransactionId < 0)
                {
                    _TransactionId = _TransactionManager.OpenTransaction;
                    if (_TransactionId < 0)
                    {
                        Marshal.FinalReleaseComObject(_TransactionManager);
                        _TransactionManager = null;
                    }
                }

                return _TransactionId;
            }
        }

        /// <summary>
        /// Assign access code
        /// </summary>
        /// <param name="accessCode"></param>
        /// <returns></returns>
        public virtual int AssignAccessCode(object accessCode)
        {
            int res = -1;

            try
            {
                this.AccessCode = TUniVar.VarToStr(accessCode);
                res = 0;
            }
            catch { }

            return res;
        }

        /// <summary>
        /// Returns current transaction status.
        /// </summary>
        /// <returns></returns>
        public int CheckTransaction()
        {
            //At this moment we don't support table of transactions like in Delphi template
            return (int)TransactionStatus;
        }

        /// <summary>
        /// Commits transaction if exists and transaction status is TTransactionStatus.ctsCommitEnabled
        /// </summary>
        /// <returns></returns>
        public int Transaction_Commit()
        {
            int res = ComponentPlatformDef.ERR_MISSING_TRANSMANAGER;

            if (_TransactionManager != null)
            {
                if (TransactionStatus == TTransactionStatus.ctsCommitEnabled)
                {
                    try
                    {
                        _TransactionManager.AppName("COMD:" + MtsComName);
                    }
                    catch { }

                    _ = _TransactionManager.CommitTransaction;
                }

                _ = _TransactionManager.CloseTransaction;
                Marshal.FinalReleaseComObject(_TransactionManager);
                _TransactionManager = null;
                res = ComponentPlatformDef.ERR_MISSING_TRANSACTION;
            }

            _TransactionId = ComponentPlatformDef.ERR_MISSING_TRANSACTION;

            return res;
        }

        /// <summary>
        /// Rollbacks transaction if exists
        /// </summary>
        /// <returns></returns>
        public int Transaction_Rollback()
        {
            int res = ComponentPlatformDef.ERR_MISSING_TRANSMANAGER;

            if (_TransactionManager != null)
            {
                _ = _TransactionManager.RollBackTransaction;
                _ = _TransactionManager.CloseTransaction;

                Marshal.FinalReleaseComObject(_TransactionManager);
                _TransactionManager = null;
                res = ComponentPlatformDef.ERR_MISSING_TRANSACTION;
            }

            _TransactionId = ComponentPlatformDef.ERR_MISSING_TRANSACTION;

            return res;
        }

#if TEST
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (_TransactionManager != null)
                    {
                        Marshal.FinalReleaseComObject(_TransactionManager);
                        _TransactionManager = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposed = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        //~ComponentPlatform()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //    Dispose(disposing: false);
        //}

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // INFO: dispose managed state (managed objects)
                    if (_TransactionManager != null)
                    {
                        Marshal.FinalReleaseComObject(_TransactionManager);
                        _TransactionManager = null;
                    }
                }

                // INFO: free unmanaged resources (unmanaged objects) and override finalizer
                // INFO: set large fields to null
                _disposed = true;
            }

            base.Dispose(disposing);
        }
#endif
    }
}
