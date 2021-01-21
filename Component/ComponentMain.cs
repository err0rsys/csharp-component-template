// ***********************************************************************
// Assembly         : Component
// Author           : Artur Maciejowski
// Created          : 16-02-2020
//
// Last Modified By : Artur Maciejowski
// Last Modified On : 28-10-2020
// ***********************************************************************
// <copyright file="ComponentMain.cs" company="DomConsult Sp. z o.o.">
//     Copyright ©  2021 All rights reserved
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.EnterpriseServices;
using DomConsult.GlobalShared.Utilities;
using System.Diagnostics;

namespace Component
{
    /// <summary>
    /// Class Manager.
    /// Implements the <see cref="Component.ManagerBase" />
    /// </summary>
    /// <seealso cref="Component.ManagerBase" />
    public class Manager : ManagerBase
    {
        /// <summary>
        /// Assigns the access code (method body).
        /// </summary>
        public override void AssignAccessCodeBody()
        {
            MtsComId = 999;
        }

        /// <summary>
        /// Assigns the start up parameters (method body).
        /// </summary>
        public override void AssignStartUpParameterBody()
        {
            Record.TableName = "Table";
            Record.KeyName = "TableId";

            Record.Query = $"SELECT * FROM { Record.TableName } m WHERE m.{Record.KeyName} = " + "{0}";

            BDW.AddModifyOther(TBDOthers.coDisabledFunction, 0);
        }

        /// <summary>
        /// Processes the input parameters.
        /// </summary>
        public override void ProcessInputParams()
        {
            try
            {
                // read or set initial parameters
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "ProcessInputParams");
            }
        }

        /// <summary>
        /// Initializes the main form (method body).
        /// </summary>
        public override void InitializeFormBody()
        {
            try
            {
                // set up controls for the first time
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "InitializeFormBody");
            }
        }

        /// <summary>
        /// Gets the value (method body).
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        public override void GetValueBody(string fieldName, ref object fieldValue)
        {
            try
            {
                // set parameters or the panel count
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "GetValueBody");
            }
        }

        /// <summary>
        /// Actualizes the controls (method body).
        /// </summary>
        /// <param name="formState">State of the form.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        public override void ActualizeControlsBody(TFormState formState, string fieldName, object fieldValue)
        {
            try
            {
                // check field values
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "ActualizeControlsBody");
            }
        }

        /// <summary>
        /// Buttons the pressed (method body).
        /// </summary>
        /// <param name="formState">State of the form.</param>
        /// <param name="fieldName">Name of the field.</param>
        public override void ButtonPressedBody(TFormState formState, string fieldName)
        {
            try
            {
                // check field values
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "ButtonPressedBody");
            }
        }

        /// <summary>
        /// Creates new record (method body).
        /// </summary>
        /// <param name="formState">State of the form.</param>
        public override void NewRecordBody(TFormState formState)
        {
            try
            {
                // set the field values

                BDW.AddModifyField("Field1", "some value");

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "NewRecordBody");
            }
        }

        /// <summary>
        /// Views the record (method body).
        /// </summary>
        /// <param name="formState">State of the form.</param>
        public override void ViewRecordBody(TFormState formState)
        {
            try
            {
                // modify data or set properties
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "ViewRecordBody");
            }
        }

        /// <summary>
        /// Checks before delete (method body).
        /// </summary>
        /// <param name="formState">State of the form.</param>
        /// <param name="errorDescription">The error description.</param>
        /// <returns>System.Int32.</returns>
        public override int CheckBeforeDeleteBody(TFormState formState, string errorDescription)
        {
            int errorCount = 0;
            try
            {
                string info = String.Empty;

                // check some delete conditions

                if (1 == 0)
                {
                    errorCount++;

                    errorDescription = String.Concat(errorDescription, Language.GetText(999));
                }

                return errorCount;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "CheckBeforeDeleteBody");

                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Deletes the record (method body).
        /// </summary>
        /// <param name="formState">State of the form.</param>
        public override void DeleteRecordBody(TFormState formState)
        {
            try
            {
                // modify data or set properties
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "DeleteRecordBody");
            }
        }

        /// <summary>
        /// Supports the SQL (method body).
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="sqlArray">The SQL array.</param>
        /// <returns>System.Int32.</returns>
        public override int SupportSQLBody(string methodName, object param, ref object sqlArray)
        {
            return 0;
        }

        /// <summary>
        /// Runs the method (method body).
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>System.Int32.</returns>
        public override int RunMethodBody(string methodName, ref object param)
        {
            return 0;
        }
    }
}