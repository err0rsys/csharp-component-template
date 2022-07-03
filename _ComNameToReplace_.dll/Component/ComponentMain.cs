using System;
using System.Runtime.InteropServices;
using System.EnterpriseServices;
using DomConsult.GlobalShared.Utilities;
using DomConsult.Components.Extensions;
using DomConsult.Components.Interfaces;
using DomConsult.Platform;
using DomConsult.Platform.Extensions;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using DomConsult.CIPHER;
using System.Linq;

/****************************************************************************************************
 *
 *  1. For dependency issues, run Package Manager Console: Update-Package -reinstall
 *  2. Check the Task List (TO DO). If you have completed the task, throw out the TO DO comment.
 *
 ****************************************************************************************************/

namespace DomConsult.Components
{
    //TODO: enter the component description here
    /// <summary>
    /// Component description
    /// </summary>
    /// <seealso cref="ManagerBase" />

#if !TEST
    [EventTrackingEnabled]
    [JustInTimeActivation]
    [ComVisible(true)]
    [Guid("3611FDAE-CA45-4EB3-A474-1783339A568A")]
    [ProgId("_ComNameToReplace_.Manager")]
    [Transaction(TransactionOption.NotSupported)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(IManager))]
#else
#endif
    public partial class Manager : ManagerBase, IManager
    {
        /// <summary>
        /// Put initialization code here.
        /// This Code is executed once during life of this component.
        /// </summary>
        protected override void OnInitialize()
        {
            //INFO : Initialize class but remember that Accesscode was not assigned yet.
            // You can also initialize class in OnAssignAccessCode wich is called once after AssignAccessCode.
        }

        /// <summary>
        /// Put deinitialization coede here. Method implements Disposing pattern.
        /// </summary>
        /// <param name="disposing">Indicates if method is called from Dispose or GC</param>
        protected override void OnDeinitialize(bool disposing)
        {
            if (disposing)
            {

                Logger.SaveLog(true);
                Logger = null;

                //INFO: free managed resources here
            }

            // INFO: free unmanaged resources (unmanaged objects) and override finalizer
            // INFO: set large fields to null
        }

        /// <summary>
        /// Assigns the access code (method body).
        /// This Code is executed once during life of this component.
        /// </summary>
        public override void OnAssignAccessCode()
        {
            MtsComId   = ComponentDef.ComPlusID;
            MtsComName = ComponentDef.ComPlusName;
        }

        /// <summary>
        /// Assigns the start up parameters (method body).
        /// This Code can be executed several times when Formular is connected to statement in the application.
        /// </summary>
        public override void OnAssignStartUpParameter()
        {
            //INFO: Determine which formId to use and on which record component will work on

            switch (FormType)
            {
                case 1:
                    FormId = ComponentDef.FRM_EXAMPLE1_ID;
                    break;
                case 2:
                    FormId = ComponentDef.FRM_EXAMPLE2_ID;
                    break;
                case 3:
                    FormId = ComponentDef.FRM_EXAMPLE3_ID;
                    break;
                default:
                    FormId = ComponentDef.FRM_EXAMPLE1_ID;
                    break;
            }

            // entity example

            //Record.TableName = "uniExample"
            //Record.ViewName  = "uniExampleView"
            //Record.Query     = $"SELECT * FROM {Record.TableName} WHERE {Record.KeyName} = {{0}}"
            //Record.ViewQuery = $"SELECT StringFld, IntegerFld, ... FROM {Record.ViewName} WHERE {Record.KeyName} = {{0}}"

            //Record.LogTableName = "uniSystemLog"

            BDW.AddModifyOther(TBDOthers.coDisabledFunction, 0);
            BDW.AddModifyOther(TBDOthers.coAddAllFieldValuesToArray, 1);
        }

        /// <summary>
        /// Processes the input parameters.
        /// </summary>
        public override void OnProcessInputParams()
        {
            // set up own input parameters
            //if (BDW.ParamExists(Record.KeyName))
            //    Record.Id = BDW.Params[Record.KeyName].AsInt();

            if (Record.Id < 0)
            {
                NewFormState = TFormState.cfsNew;
            }

            TuniDebug.UpdateSysLog(AccessCode, MtsComId, TransactionId, TUserSession.GetCurrentUserId(AccessCode),
                Record.LogTableName, MtsComName, "OnAssignStartUpParameter", $"Params: {BDW.Params.ToPrettyString()}");
        }

        /// <summary>
        /// Initializes the main form (method body).
        /// </summary>
        public override void OnInitializeForm()
        {
            // set up controls for the first time
        }

        /// <summary>
        /// Gets the value of the given field (method body).
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        public override void OnGetValue(string fieldName, ref object fieldValue)
        {
            // Set fieldValue = parameters or the panel count for given fieldName
        }

        /// <summary>
        /// Actualizes the controls (method body).
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        public override void OnActualizeControls(string fieldName, object fieldValue)
        {
            // check field values and return changes to application if any
        }

        /// <summary>
        /// Buttons pressed (method body).
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        public override void OnButtonPressed(string fieldName)
        {
            // check field values
        }

        /// <summary>
        /// Creates new record (method body).
        /// </summary>
        public override void OnNewRecord()
        {
            // set the field values

            //BDW.AddModifyField("StringFld", null)
        }

        /// <summary>
        /// Views the record (method body).
        /// </summary>
        public override void OnViewRecord()
        {
            // modify data or set properties

            if (Record.DataSize > 0)
            {
                for (int i = 0; i < Record.Data.GetLength(1); i++)
                {
                    BDW.AddModifyField(Record.Data[0, i].ToString(), Record.Data[1, i]);
                }
            }
        }

        /// <summary>
        /// Edits the record (method body).
        /// </summary>
        public override void OnEditRecord()
        {
            // on edit code
        }

        /// <summary>
        /// Cancels editing of the record (method body).
        /// </summary>
        public override void OnCancelRecord()
        {
            // on cancel code
        }

        /// <summary>
        /// Checks before save (method body).
        /// </summary>
        /// <param name="errorDescription">The error description.</param>
        /// <param name="noDialog">Set True to turn off standard dialog</param>
        /// <returns>System.Int32.</returns>
        public override int OnCheckBeforeSave(out string errorDescription, out bool noDialog)
        {
            errorDescription = "";
            noDialog = false; //Set true only if there is no errors and You don't want to ask user about continuation

            int errorCount = 0;

            // check some save conditions

            /*
            if (BDW.FieldExists("StringFld"))
            {
                string value = BDW.Fields["StringFld"].AsString;

                if (value.Length < 5)
                {
                    errorCount++;

                    //INFO: get MLText instead of static text: Language.GetText(0)
                    errorDescription = String.Concat(errorDescription,
                        "Pole [String] powinno mieæ przynajmniej 5 znaków.", Environment.NewLine);
                }
            }
            */

            return errorCount;
        }

        /// <summary>
        /// Saves the record (method body).
        /// </summary>
        /// <param name="decision">User confirmation if any</param>
        /// <param name="proceed">Set True to continue Delete process or False to break it.</param>
        public override void OnSaveRecord(TConfirmResult decision, ref bool proceed)
        {
            // Save record accordingly to the decision of the user if any expected.

            /*
            if (decision == TConfirmResult.acrYes)
            {
                // Save record to database
                var fields = new object[]
                {
                    "StringValue",
                    "IntegerValue",
                    "FloatValue",
                    "CurrencyValue",
                    "DateValue"
                };

                var values = new object[]
                {
                    BDW.Fields["StringFld"].AsVar,
                    BDW.Fields["IntegerFld"].AsVar,
                    BDW.Fields["FloatFld"].AsVar,
                    BDW.Fields["CurrencyFld"].AsVar,
                    BDW.Fields["DateFld"].AsVar
                };

                if (UserFormState == TFormState.cfsNew) // or (Record.Id < 0)
                {
                    Record.Id = -1;
                    ComWrapper com = null;

                    try
                    {
                        int result = ComUtils.OpenResultset(
                                                AccessCode,
                                                string.Format(Record.Query, Record.Id),
                                                Record.KeyName,
                                                TransactionId,
                                                DefaultTimeOut,
                                                out com);
                        Err.Check(result, com);

                        Record.Id = ComUtils.RecordNew(ref com, fields, values, Record.KeyName);
                        Err.Check(Record.Id, com);
                    }
                    finally
                    {
                        com.Dispose();
                    }
                }
                else
                {
                    Record.Id = ComUtils.RecordUpdate(ref Record.ComObj, fields, values, Record.KeyName);
                    Err.Check(Record.Id, Record.ComObj);
                }
            }
            */
        }

        /// <summary>
        /// Checks conditions before delete (method body).
        /// </summary>
        /// <param name="errorDescription">The error description.</param>
        /// <param name="noDialog">Set True to turn off standard dialog</param>
        /// <returns>System.Int32.</returns>
        public override int OnCheckBeforeDelete(out string errorDescription, out bool noDialog)
        {
            errorDescription = string.Empty;
            noDialog = false; //Set true only if there is no errors and You don't want to ask user about continuation

            int errorCount = 0;

            // check some delete conditions

            /*
            if (BDW.FieldExists("IntegerFld"))
            {
                int value = BDW.Fields["IntegerFld"].AsInt;

                if (value < 10)
                {
                    errorCount++;
                    errorDescription = String.Concat(errorDescription,
                        string.Format(Language.GetText(ComponentDef.TXT_A00101_ID), "10"));
                }
            }
            */

            return errorCount;
        }

        /// <summary>
        /// Deletes the record (method body).
        /// </summary>
        /// <param name="decision">User confirmation if any</param>
        /// <param name="proceed">Set True to continue Delete process or False to break it.</param>
        public override void OnDeleteRecord(TConfirmResult decision, ref bool proceed)
        {
            // Delete record accordingly to the user decision if any expected.
        }

        /// <summary>
        /// Initialize supporting the SQL (run only once for the method).
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        public override void OnInitSupportSQL(string methodName, object param)
        {
            if (RunOnce)
            {
                // do something

                var _params = param as object[];

                TuniDebug.UpdateSysLog(AccessCode, MtsComId, TransactionId, TUserSession.GetCurrentUserId(AccessCode),
                    Record.LogTableName, MtsComName, "OnInitSupportSQL", $"Params: {_params[0]}");

                RunOnce = false;
            }
        }

        /// <summary>
        /// Supports the SQL (method body).
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="sqlArray">The SQL array.</param>
        /// <returns>System.Int32.</returns>
        public override int OnSupportSQL(string methodName, object param, ref object sqlArray)
        {
            return -1;
        }

        /// <summary>
        /// Initialize running the method (run only once for the method).
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        public override void OnInitRunMethod(string methodName, ref object param)
        {
            if (RunOnce)
            {
                // do something

                var _params = param as object[];

                TuniDebug.UpdateSysLog(AccessCode, MtsComId, TransactionId, TUserSession.GetCurrentUserId(AccessCode),
                    Record.LogTableName, MtsComName, "OnInitRunMethod", $"Params: {_params[0]}");

                RunOnce = false;
            }
        }

        /// <summary>
        /// Runs the method (method body).
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>System.Int32.</returns>
        public override int OnRunMethod(string methodName, ref object param)
        {
            object data;
            int result = -1;
            var _params = param as object[];

            switch (methodName)
            {
                case ComponentDef.RM_XXXXX:
                    result = RM_XXXXX(_params[0], out data);
                    _params[2] = data;
                    param = _params;
                    break;
                default:
                    break;
            }

            return result;
        }


        #region Register - ThreadingModel
        /*
        /// <summary>
        /// Set threading model for component while registration. Default is ThreadingModelType.Both
        /// If change is not needed comment out method.
        /// If change is needed do not forget about attribute ComRegisterFunction. It's required!
        /// </summary>
        //[ComRegisterFunction]
        //private static void Register(Type registerType) => Register(registerType, ThreadingModelType.STA);
        */
        #endregion
    }
}