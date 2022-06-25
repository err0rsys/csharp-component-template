using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomConsult.GlobalShared.Utilities.Interfaces
{
    /// <summary>
    /// Logger interface
    /// </summary>
    public interface IComLogger
    {
        /// <summary>
        /// Gets or sets the log file prefix.
        /// </summary>
        /// <value>The log file prefix.</value>
        string LogFilePrefix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow debug].
        /// </summary>
        /// <value><c>true</c> if [allow debug]; otherwise, <c>false</c>.</value>
        bool AllowDebug { get; set; }

        /// <summary>
        /// Gets or sets the log dir.
        /// </summary>
        /// <value>The log dir.</value>
        string LogDir { get; set; }

        /// <summary>
        /// Gets or sets the name of the log file.
        /// </summary>
        /// <value>The name of the log file.</value>
        string LogFileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add date to log].
        /// </summary>
        /// <value><c>true</c> if [add date to log]; otherwise, <c>false</c>.</value>
        bool AddDateToLog { get; set; }

        /// <summary>
        /// Resets the timer.
        /// </summary>
        void ResetTimer();

        /// <summary>
        /// Prints the debug.
        /// </summary>
        /// <returns>System.String.</returns>
        string PrintDebug();

        /// <summary>
        /// Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="addElapsedTime">if set to <c>true</c> [add elapsed time].</param>
        void Add(string message, bool addElapsedTime = true);

        /// <summary>
        /// Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void Add(string message, params object[] args);

        /// <summary>
        /// Adds the format.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void AddFormat(string message, params object[] args);

        /// <summary>
        /// Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        void Add(string message, Exception ex);

        /// <summary>
        /// Adds the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void Add(Exception ex);

        /// <summary>
        /// Add the specified params array to log
        /// </summary>
        /// <param name="params"></param>
        /// <param name="printPasswords"></param>
        void AddParamsArray(object @params, bool printPasswords = false);

        /// <summary>
        /// Add the specified dictionary to log
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="printPasswords"></param>
        void AddDictionary(Dictionary<string, object> dict, bool printPasswords = false);

        /// <summary>
        /// Saves the log.
        /// </summary>
        /// <param name="force"></param>
        void SaveLog(bool force = false);
    }
}
