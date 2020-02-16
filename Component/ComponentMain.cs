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
    public class Manager : ManagerBase
    {
        public override void AssignAccessCodeBody()
        {
            MtsComId = 999;
        }

        public override void AssignStartUpParameterBody()
        {
            Record.TableName = "Table";
            Record.KeyName = "TableId";

            Record.Query = $"SELECT * FROM { Record.TableName } m WHERE m.{Record.KeyName} = " + "{0}";

            BDW.AddModifyOther(TBDOthers.coDisabledFunction, 0);
        }

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

        public override void InitializeFormBody()
        {
            try
            {
                // set up controls first time
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "InitializeFormBody");
            }
        }

        public override void GetValueBody(string fieldName, ref object fieldValue)
        {
            try
            {
                // set parameters or panel count
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "GetValueBody");
            }
        }

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

        public override void NewRecordBody(TFormState formState)
        {
            try
            {
                // set field values

                BDW.AddModifyField("Field1", "some value");

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "NewRecordBody");
            }
        }

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

        public override int SupportSQLBody(string methodName, object param, ref object sqlArray)
        {
            return 0;
        }

        public override int RunMethodBody(string methodName, ref object param)
        {
            return 0;
        }
    }
}