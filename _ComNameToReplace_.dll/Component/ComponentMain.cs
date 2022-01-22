using System;
using System.Runtime.InteropServices;
using System.EnterpriseServices;
using DomConsult.GlobalShared.Utilities;
using DomConsult.Components.Interfaces;
using DomConsult.Platform;
using DomConsult.Platform.Extensions;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using DomConsult.CIPHER;

/*
 * W razie problemów z zale¿noœciami uruchom w konsoli Package Manager: Update-Package -reinstall
 * Zmieñ nazwê aplikacji COM+ (dawniej - pakietu COM+) w plikach install.bat i deploy.bat
 * SprawdŸ listê zadañ (TODO). Je¿eli wykona³eœ zadanie wyrzuæ komentarz TODO.
 */

namespace DomConsult.Components
{
    /// <summary>
    /// Opis komponentu bran¿owego
    /// </summary>
    /// <seealso cref="ManagerBase" />

#if !TEST
    [ComVisible(true)]
    [Guid("3611FDAE-CA45-4EB3-A474-1783339A568A")]
    [ProgId("_ComNameToReplace_.Manager")]
    [Transaction(TransactionOption.Disabled)]
    [JustInTimeActivation(true)]
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
            MtsComId = ComponentDef.ComPlusID;
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

            Record.Id = -1;
            //Record.TableName = "uniExample";
            //Record.KeyName = Record.TableName + "Id";
            //Record.Query = @"";

            BDW.AddModifyOther(TBDOthers.coDisabledFunction, 0);
            BDW.AddModifyOther(TBDOthers.coAddAllFieldValuesToArray, 1);
        }

        /// <summary>
        /// Processes the input parameters.
        /// </summary>
        public override void OnProcessInputParams()
        {
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
        /// <param name="buttonName">Name of the field.</param>
        public override void OnButtonPressed(string buttonName)
        {
            // check field values
        }

        /// <summary>
        /// Creates new record (method body).
        /// </summary>
        public override void OnNewRecord()
        {
            // set the field values

            //BDW.AddModifyField("StringFld", null);
        }

        /// <summary>
        /// Views the record (method body).
        /// </summary>
        public override void OnViewRecord()
        {
            // modify data or set properties

            if (Record.DataSize > 0)
            {
                //BDW.AddModifyField("StringFld", Record.Data[1, 1]);
            }
        }

        /// <summary>
        /// Edits the record (method body).
        /// </summary>
        public override void OnEditRecord()
        {
        }

        /// <summary>
        /// Cancels editing of the record (method body).
        /// </summary>
        public override void OnCancelRecord()
        {
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
                    Record.Id = ComUtils.RecordNew(ref Record.ComObj, fields, values, Record.KeyName);
                else
                    Record.Id = ComUtils.RecordUpdate(ref Record.ComObj, fields, values, Record.KeyName);

                Err.Check(Record.Id, Record.ComObj);
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
            errorDescription = "";
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

            methodName = methodName.ToUpper();
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
    }
}