// ***********************************************************************
// Assembly         : Component
// Author           : Artur Maciejowski
// Created          : 16-02-2020
//
// Last Modified By : Artur Maciejowski
// Last Modified On : 28-02-2020
// ***********************************************************************
// <copyright file="IManager.cs" company="DomConsult Sp. z o.o.">
//     Copyright ©  2021 All rights reserved
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Runtime.InteropServices;

namespace Component
{
    /// <summary>
    /// Interface IManager
    /// </summary>
    [ComVisible(true), Guid("CAF8BD41-EA4C-4C49-9782-BAC86CA5B5F9")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IManager
    {
        /// <summary>
        /// Assigns the access code.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.Int32.</returns>
        int AssignAccessCode(object accessCode);
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
    }
}
