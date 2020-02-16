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
#if !DEBUG
    [ComVisible(true)]
    [Guid("902545D5-2699-43b1-8D8A-CE50F2127886")]
    [ProgId("Component.Manager")]
    [Transaction(TransactionOption.Required)]
    [JustInTimeActivation(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(IManager))]

    public abstract class ManagerBase : ServicedComponent, IManager
#else
    public abstract class ManagerBase : IManager
#endif
    {
        private int StartFormState;
        private bool FormInitialized;

        public int FormId { get; set; }
        public int ObjectTypeId { get; set; }
        public object WMK { get; set; }
        public int ExTransactionId { get; set; }
        public int Silent { get; set; }

        private Err _err;
        public Err Err
        {
            get { return _err; }
        }

        private string _accessCode;

        public string AccessCode
        {
            get { return _accessCode; }
            set
            {
                ComWrapper.AccessCode = "/AC=" + value;

                _accessCode = ComWrapper.AccessCode;
            }
        }

        private int _mtsComId;
        public int MtsComId
        {
            get
            {
                return _mtsComId;
            }

            set
            {
                _mtsComId = value;

                if (!ComUtils.Assigned(_err))
                {
                    _err = new Err(_mtsComId);
                }
            }
        }

        private Language _language;
        public Language Language
        {
            get
            {
                if (!ComUtils.Assigned(_language))
                {
                    _language = new Language(MtsComId, AccessCode);
                }
                return _language;
            }
            set { _language = value; }
        }

        private BDWrapper _bdw;

        public BDWrapper BDW
        {
            get
            {
                if (!ComUtils.Assigned(_bdw))
                {
                    _bdw = new BDWrapper();
                }
                return _bdw;
            }

            set { _bdw = value; }
        }

        private ComWrapper _comWrapper;
        public ComWrapper ComWrapper
        {
            get
            {
                if (!ComUtils.Assigned(_comWrapper))
                {
                    _comWrapper = new ComWrapper();
                }
                return _comWrapper;
            }
            set { _comWrapper = value; }
        }

        private Record _record;
        public Record Record
        {
            get
            {
                if (!ComUtils.Assigned(_comWrapper))
                {
                    _record = new Record();
                }
                return _record;
            }
            set { _record = value; }
        }

        public int AssignAccessCode(object accessCode)
        {
            try
            {
                MtsComId = -1;
                StartFormState = -1;
                ExTransactionId = -1;

                AssignAccessCodeBody();

                FormInitialized = false;

                AccessCode = TUniVar.VarToStr(accessCode);
                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return -1;
            }
        }

        public virtual void AssignAccessCodeBody()
        {
            throw new NotImplementedException();
        }

        public int AssignStartUpParameter(object param, ref object others)
        {
            int result = 0;
            try
            {
                BDW.AddModifyOther(TBDOthers.coObjectTypeId, ObjectTypeId);
                BDW.AddModifyOther(TBDOthers.coAddAllFieldValuesToArray, 1);

                AssignStartUpParameterBody();

                Record.ObjectId = TUniVar.VarToStr(param);

                BDW.LoadParams(Record.ObjectId);

                if (BDW.ParamExists("_Silent"))
                    Silent = (int)BDW.Params("_Silent");

                if (BDW.ParamExists("_TransactionId"))
                    ExTransactionId = (int)BDW.Params("_TransactionId");

                if (BDW.ParamExists(Record.KeyName))
                    Record.Id = (int)BDW.Params(Record.KeyName);

                ProcessInputParams();
            }
            catch (MessageException mex)
            {
                Trace.WriteLine(mex.Message);

                Err.HandleMessage(mex);

                result = (int)TCSWMK.csWMK;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "AssignStartUpParameter", false);

                result = (int)TCSWMK.csWMK_Error;
            }
            finally
            {
                // always set form id
                BDW.AddModifyOther(TBDOthers.coFormId, FormId);

                others = BDW.GetOthers();
            }

            return result;
        }

        public virtual void AssignStartUpParameterBody()
        {
            throw new NotImplementedException();
        }

        public virtual void ProcessInputParams()
        {
            throw new NotImplementedException();
        }

        public void InitializeForm()
        {
            try
            {
                InitializeFormBody();

                FormInitialized = true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "InitializeForm");
            }
        }

        public virtual void InitializeFormBody()
        {
            throw new NotImplementedException();
        }

        public int GetValue(object fieldName, ref object fieldValue)
        {
            int result = 0;
            try
            {
                GetValueBody(TUniVar.VarToStr(fieldName), ref fieldValue);
            }
            catch (MessageException mex)
            {
                Trace.WriteLine(mex.Message);

                Err.HandleMessage(mex);

                result = (int)TCSWMK.csWMK;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "GetValue", false);

                result = (int)TCSWMK.csWMK_Error;
            }

            return result;
        }

        public virtual void GetValueBody(string fieldName, ref object fieldValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Base class main loop
        /// </summary>
        public int SetGetValues(ref object formState, ref object fields, ref object messages, ref object others)
        {
            int result = 0;
            try
            {
                TFormState fs = (TFormState)formState;

                if (Enum.IsDefined(typeof(TFormState), StartFormState))
                {
                    fs = (TFormState)StartFormState;
                    StartFormState = -1;
                }

                BDW.LoadFields(fields, false);
                BDW.LoadOthers(others);

                if (!FormInitialized)
                {
                    InitializeForm();
                }

                switch (fs)
                {
                    case TFormState.cfsNoChanges:
                        result = ActualizeControls(fs);
                        break;
                    case TFormState.cfsView:
                        result = ViewRecord(fs);
                        break;
                    case TFormState.cfsEdit:
                        result = EditRecord(fs);
                        break;
                    case TFormState.cfsNew:
                        result = NewRecord(fs);
                        break;
                    case TFormState.cfsUpdate:
                        result = SaveRecord(fs);
                        break;
                    case TFormState.cfsCancel:
                        result = CancelRecord(fs);
                        break;
                    case TFormState.cfsDelete:
                        result = DeleteRecord(fs);
                        break;
                    case TFormState.cfsButtonPressed:
                        result = ButtonPressed(fs);
                        break;
                }

                fields = BDW.GetFields();
                others = BDW.GetOthers();

                if (!BDW.OtherExists(TBDOthers.coCallAgain))
                {
                    if ((fs == TFormState.cfsUpdate) ||
                        (fs == TFormState.cfsCancel) ||
                        (fs == TFormState.cfsDelete))
                        {
                        fields = null;
                    }
                }
            }
            catch (MessageException mex)
            {
                Trace.WriteLine(mex.Message);

                Err.HandleMessage(mex);

                result = (int)TCSWMK.csWMK;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "SetGetValues", false);

                result = (int)TCSWMK.csWMK_Error;

            }

            return result;
        }

        public int ActualizeControls(TFormState formState)
        {
            try
            {
                string fieldName = String.Empty;
                object fieldValue = null;

                if (BDW.OtherExists(TBDOthers.coActiveControlFieldName))
                {
                    fieldName = BDW.Others(TBDOthers.coActiveControlFieldName).ToString();

                    if (BDW.OtherExists(TBDOthers.coActiveControlValue))
                    {
                        fieldValue = BDW.Others(TBDOthers.coActiveControlValue);

                        ActualizeControlsBody(formState, fieldName, fieldValue);
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "SetGetValues");

                return (int)TCSWMK.csWMK_Error;

            }
        }

        public virtual void ActualizeControlsBody(TFormState formState, string fieldName, object fieldValue)
        {
            throw new NotImplementedException();
        }

        public virtual int ButtonPressed(TFormState formState)
        {
            try
            {
                string fieldName = String.Empty;

                if (BDW.OtherExists(TBDOthers.coActiveControlFieldName))
                {
                    fieldName = BDW.Others(TBDOthers.coActiveControlFieldName).ToString();

                    ButtonPressedBody(formState, fieldName);
                }
                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "ButtonPressed");

                return (int)TCSWMK.csWMK_Error;

            }
        }

        public virtual void ButtonPressedBody(TFormState formState, string fieldName)
        {
            throw new NotImplementedException();
        }

        public virtual int NewRecord(TFormState formState)
        {
            try
            {
                Record.Id = -1;

                NewRecordBody(formState);

                BDW.AddModifyOther(TBDOthers.coObjectTypeId, ObjectTypeId);

                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "NewRecord");

                return (int)TCSWMK.csWMK_Error;

            }
        }

        public virtual void NewRecordBody(TFormState formState)
        {
            throw new NotImplementedException();
        }

        public int CheckBeforeDelete(TFormState formState)
        {
            int result = 0;

            TConfirmResult decision;

            try
            {
                if (!BDW.OtherExists(TBDOthers.coConfirmationResult))
                {
                    string errorDescription = String.Empty;

                    int errorCount = CheckBeforeDeleteBody(formState, errorDescription);

                    if (errorCount > 0)
                    {
                        BDW.AddModifyOther(TBDOthers.coModificationsCancelled, 1);

                        Err.SendOthers = true;
                        Err.MessageRaise(1, new object[] { errorDescription });

                    }

                    result = (int)TCSWMK.csWMK_JobConversation;

                }
                else
                {
                    decision = (TConfirmResult)BDW.Others(TBDOthers.coConfirmationResult);

                    BDW.DeleteOther(TBDOthers.coConfirmationResult);

                    switch (decision)
                    {
                        case TConfirmResult.acrYes:
                            break;
                        case TConfirmResult.acrNo:
                        case TConfirmResult.acrCancel:
                            formState = TFormState.cfsCloseBD;
                            break;
                        default:
                            break;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "CheckBeforeDelete");

                return (int)TCSWMK.csWMK_Error;

            }
        }

        public virtual int CheckBeforeDeleteBody(TFormState formState, string errorDescription)
        {
            return 0;
        }

        public int DeleteRecord(TFormState formState)
        {
            try
            {
                int result = CheckBeforeDelete(formState);

                if (result > 0)
                {
                    BDW.AddModifyOther(TBDOthers.coModificationsCancelled, 1);
                    return result;
                }

                DeleteRecordBody(formState);

                Record.Id = -1;

                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "DeleteRecord");

                return (int)TCSWMK.csWMK_Error;

            }
        }

        public virtual void DeleteRecordBody(TFormState formState)
        {
            throw new NotImplementedException();
        }

        public int ViewRecord(TFormState formState)
        {
            try
            { 
                if (Record.Query.Length > 0)
                {
                    object[,] packet = null;
                    int result = ComUtils.GetPacket(
                        AccessCode,
                        String.Format(Record.Query, Record.Id),
                        -1,
                        -1,
                        out packet);

                    if (result > 0)
                    {
                        for (int i = 0; i < packet.GetLength(1); i++)
                        {
                            BDW.AddModifyField(TUniVar.VarToStr(packet[0, i]), packet[1, i]);
                        }
                    }
                 }

                ViewRecordBody(formState);

                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "ViewRecord");

                return (int)TCSWMK.csWMK_Error;

            }
        }

        public virtual void ViewRecordBody(TFormState formState)
        {
            throw new NotImplementedException();
        }

        public virtual int SaveRecord(TFormState formState)
        {
            return 0;
        }

        public virtual int CancelRecord(TFormState formState)
        {
            try
            {
                CancelRecordBody(formState);

                int result = ComUtils.RecordCancel(ref Record.ComObj);

                Err.Check(result, Record.ComObj);

                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "CanceRecord");

                return (int)TCSWMK.csWMK_Error;

            }
        }

        public virtual void CancelRecordBody(TFormState formState)
        {
            throw new NotImplementedException();
        }

        public virtual int EditRecord(TFormState formState)
        {
            ViewRecord(formState);

            return LockRecord();
        }

        private int LockRecord()
        {
            int result = 0;
            try
            {
                result = ComUtils.OpenResultset(
                    AccessCode,
                    String.Format(Record.Query, Record.Id),
                    Record.KeyName,
                    -1,
                    -1,
                    out Record.ComObj);

                Err.Check(result, Record.ComObj);

                int lockerId = -1;

                result = ComUtils.RecordEdit(ref Record.ComObj, out lockerId);

                if (result > 0)
                {
                    Err.MessageComRaise(Record.ComObj);
                }
                else
                {
                    Err.Check(result, Record.ComObj);
                }

                return result;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);

                Err.HandleException(ex, "LockRecord");

                return (int) TCSWMK.csWMK_Error;
            }
        }

        public int SupportSQL(object methodName, object param, ref object sqlArray)
        {
            int result = 0;
            try
            {
                result = SupportSQLBody(TUniVar.VarToStr(methodName), param, ref sqlArray);
                CheckTransactionState(result);
                return result;
            }
            catch (Exception ex)
            {
                CheckTransactionState(result);
                Trace.WriteLine(ex.Message);
                return (int)TCSWMK.csWMK_Error;
            }
        }

        public virtual int SupportSQLBody(string methodName, object param, ref object sqlArray)
        {
            return 0;
        }

        public virtual int AssignWMKResult(object wmkResult)
        {
            BDW.AddModifyOther(TBDOthers.coConfirmationResult, wmkResult);
            return 0;
        }

        public int GetLastErrorDescription(ref object error)
        {
            error = WMK;
            return 0;
        }

        public int RunMethod(object methodName, ref object param)
        {
            int result = 0;
            try
            {
                result = RunMethodBody(TUniVar.VarToStr(methodName), ref param);
                CheckTransactionState(result);
                return result;
            }
            catch (Exception ex)
            {
                CheckTransactionState(result);
                Trace.WriteLine(ex.Message);
                return (int)TCSWMK.csWMK_Error;
            }
        }

        public virtual int RunMethodBody(string methodName, ref object param)
        {
            return 0;
        }

        private int CheckTransactionState(int result)
        {
            return 0;
        }
    }
}
