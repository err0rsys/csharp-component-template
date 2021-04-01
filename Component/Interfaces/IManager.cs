using System;
using System.Runtime.InteropServices;

namespace DomConsult.Components.Interfaces
{
    /// <summary>
    /// Interface IManager
    /// </summary>
    [ComVisible(true)]
    [Guid("D4AC193C-826C-44D7-AAB8-8F04F83C27FD")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IManager // IDCPlatform - using inheritance methods are invisible in COM+ console and our application :-(
    {
        /// <summary>
        /// Assigns the start up parameter.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="others">The others.</param>
        /// <returns>System.Int32.</returns>
        int AssignStartUpParameter(object param, ref object others);
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        /// <returns>System.Int32.</returns>
        int GetValue(object fieldName, ref object fieldValue);
        /// <summary>
        /// Sets the get values.
        /// </summary>
        /// <param name="formState">State of the form.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="messages">The messages.</param>
        /// <param name="others">The others.</param>
        /// <returns>System.Int32.</returns>
        int SetGetValues(ref object formState, ref object fields,
                         ref object messages, ref object others);
        /// <summary>
        /// Supports the SQL.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="sqlArray">The SQL array.</param>
        /// <returns>System.Int32.</returns>
        int SupportSQL(object methodName, object param, ref object sqlArray);
        /// <summary>
        /// Runs the method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>System.Int32.</returns>
        int RunMethod(object methodName, ref object param);
        /// <summary>
        /// Assigns the WMK result.
        /// </summary>
        /// <param name="wmkResult">The WMK result.</param>
        /// <returns>System.Int32.</returns>
        int AssignWMKResult(object wmkResult);
        /// <summary>
        /// Gets the last error description.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>System.Int32.</returns>
        int GetLastErrorDescription(ref object error);

        #region IDCPlatform - Should be done by inheritance but how?
        // https://social.msdn.microsoft.com/Forums/vstudio/en-US/7313191a-10db-4a16-9cdd-de9fb80b378a/com-interop-base-class-properties-not-exposed-to-com?forum=csharpgeneral

        /// <summary>
        /// Assigns access code.
        /// </summary>
        /// <param name="AccessCode">Access code</param>
        /// <returns>System.Int32.</returns>
        int AssignAccessCode(object AccessCode);

        /// <summary>
        /// Checks the state of current transaction.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int CheckTransaction();

        /// <summary>
        /// Commits current transaction.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int Transaction_Commit();

        /// <summary>
        /// Rollback current transaction.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int Transaction_Rollback();

        #endregion
    }
}
