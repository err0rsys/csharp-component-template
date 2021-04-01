using System;
using System.Runtime.InteropServices;
using System.EnterpriseServices;
using DomConsult.GlobalShared.Utilities;
using DomConsult.Components.Interfaces;
using DomConsult.Platform;
using DomConsult.Platform.Extensions;

namespace DomConsult.Components
{
#if !TEST
    [ComVisible(true)]
    [Guid("BEDF705E-B788-47A6-B915-2BD71DA159D4")]
    [ProgId("Component.Manager")]
    [Transaction(TransactionOption.Disabled)]
    [JustInTimeActivation(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(IManager))]
#else
#endif

    /// <summary>
    /// Class that contain business logic.
    /// Implements the <see cref="ManagerBase" />
    /// </summary>
    /// <seealso cref="ManagerBase" />
    public class Manager: ManagerBase, IManager
    {
        /// <summary>
        /// Put initialization code here.
        /// This Code is executed once during life of this component.
        /// </summary>
        protected override void OnInitialize()
        {
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
            MtsComId = ComponentDef.MtsComID;
            MtsComName = ComponentDef.MtsComName;
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
            Record.TableName = "uniExample";
            Record.KeyName = Record.TableName + "Id";
            Record.Query = @"
SELECT
  uniExampleId,
  StringValue,
  IntegerValue,
  FloatValue,
  CurrencyValue,
  DateValue
FROM uniExample m
WHERE m.uniExampleId = {0}";

            BDW.AddModifyOther(TBDOthers.coDisabledFunction, 0);
            BDW.AddModifyOther(TBDOthers.coAddAllFieldValuesToArray, 1);
        }

        /// <summary>
        /// Processes the input parameters.
        /// </summary>
        public override void OnProcessInputParams()
        {
            //TODO: OnProcessInputParams - [MP] Czy ta metoda jest do czegoœ w ogóle potrzebna
            //                             [AM] do wczytywania i obróbki parametrów zewnêtrznych komponentu

            //INFO: Analyze input parameters

            if (BDW.ParamExists(Record.KeyName))
                Record.Id = BDW.Params[Record.KeyName].AsInt();

            /*
            if (BDW.ParamExists("param_1"))
                param1 = BDW.Params["param1"].AsInt();
            */

            if (Record.Id > 0)
            {
                var sql = string.Format(@"
SELECT
  StringValue
FROM uniExample
WHERE uniExampleId={0}", Record.Id);

                int res = ComUtils.GetPacket(AccessCode, sql, -1, -1, out object[,] data);

                if (res > 0)
                {
                    FormCaption = string.Format("{0}:{1}", Language.GetText(ComponentDef.TXT_A00100_ID), TUniVar.VarToStr(data[1, 0]));
                }
                else
                {
                    NewFormState = TFormState.cfsCloseBD;
                    //TODO: OnAssignStartUpParameter - [MP] Komunikat!
                    Err.MessageRaise(0);
                }
            }
            else
            {
                NewFormState = TFormState.cfsNew;
            }
        }

        /// <summary>
        /// Initializes the main form (method body).
        /// </summary>
        public override void OnInitializeForm()
        {
            // set up controls for the first time
        }

        /// <summary>
        /// Gets the value (method body).
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

            BDW.AddModifyField("StringFld", null);
            BDW.AddModifyField("IntegerFld", null);
            BDW.AddModifyField("FloatFld", null);
            BDW.AddModifyField("CurrencyFld", null);
            BDW.AddModifyField("DateFld", null);
        }

        /// <summary>
        /// Views the record (method body).
        /// </summary>
        public override void OnViewRecord()
        {
            // modify data or set properties

            if (Record.DataSize > 0)
            {
                BDW.AddModifyField("StringFld", Record.Data[1, 1]);
                BDW.AddModifyField("IntegerFld", Record.Data[1, 2]);
                BDW.AddModifyField("FloatFld", Record.Data[1, 3]);
                BDW.AddModifyField("CurrencyFld", Record.Data[1, 4]);
                BDW.AddModifyField("DateFld", Record.Data[1, 5]);
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
            return -1;
        }
    }
}