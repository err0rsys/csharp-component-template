﻿//DEVEL: ManagerBaseDef - [MP] Language manager vs. Cache
//DEVEL: ManagerBaseDef - [MP] FormId, FormCaption, ObjectTypeId, MainObjectId, : czy set-ery tych właściwości nie powinny robić BDW.AddModifyOther?
//                       [AM] mogą, będzie trochę czytelniej
//DEVEL: ManagerBaseDef - [MP] Trace.TraceXXX - używanie Trace jest raczej złym pomysłem. Trzeba przejść na Logger-a i ewentualnie coś co rzuca info na consolę.
//                       [AM] jakoś nie widzę w tym nic złego, Logger? Hmmm, to już nie moje klocki.

using System;
using System.Diagnostics;
using DomConsult.Platform.Extensions;
using DomConsult.GlobalShared.Utilities;

namespace DomConsult.Platform
{
    /// <summary>
    /// Class implements simplified scheme of formular/statement/job management.
    /// Programmer implements all OnXXX methods accoording to the buisness logic.
    /// </summary>
    /// <seealso cref="ComponentPlatform" />
    public abstract class ManagerBase: ComponentPlatform
    {
        /// <summary>
        /// Real form state (cfsView, cfsEdit, cfsNew) - without substates.
        /// </summary>
        public TFormState UserFormState { get; set; } = TFormState.cfsNone;

        /// <summary>
        /// Internal NewFormState
        /// </summary>
        private TFormState _newFormState = TFormState.cfsNone;

        /// <summary>
        /// The form state that is required by the component.
        /// </summary>
        public TFormState NewFormState {
            get
            {
                return _newFormState;
            }

            set
            {
                _newFormState = value;
                switch (_newFormState)
                {
                    case TFormState.cfsView:
                    case TFormState.cfsEdit:
                    case TFormState.cfsNew:
                        UserFormState = _newFormState;
                        break;
                }
            }
        }

        /// <summary>
        /// Indicates if form initialization method was already called.
        /// </summary>
        public bool FormInitialized { get; set; }  = false;

        /// <summary>
        /// Gets or sets the form identifier.
        /// </summary>
        /// <value>The form identifier.</value>
        public int FormId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the form type.
        /// Can be used to select form
        /// </summary>
        /// <value>The formtype flag.</value>
        public int FormType { get; set; } = 1;

        /// <summary>
        /// Gets or sets the form caption.
        /// </summary>
        /// <value>Form caption.</value>
        public object FormCaption { get; set; }

        /// <summary>
        /// Gets or sets the object type identifier.
        /// </summary>
        /// <value>The object type identifier.</value>
        public int ObjectTypeId { get; set; } = -1;

        /// <summary>
        /// Gets or sets the main object identifier used as an input parameters for actions
        /// from form context menu or specially configurated buttons.
        /// </summary>
        /// <value>Main object identifier.</value>
        public string MainObjectId { get; set; } = "";

        /// <summary>
        /// Gets or sets the silent flag.
        /// </summary>
        /// <value>The silent flag.</value>
        public int Silent { get; set; } = 0;

        /// <summary>
        /// The error variable (private)
        /// </summary>
        private Err _err;

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>The error variable.</value>
        public Err Err
        {
            get
            {
                if (!ComUtils.Assigned(_err))
                {
                    _err = new Err(-1);
                }

                return _err; 
            }
        }

        /// <summary>
        /// Gets or sets the MTS COM identifier.
        /// </summary>
        /// <value>The MTS COM identifier.</value>
        public int MtsComId
        {
            get
            {
                return Err.MTSComId;
            }

            set
            {
                Err.MTSComId = value;
            }
        }

        /// <summary>
        /// The language
        /// </summary>
        private Language _language;

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
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

        /// <summary>
        /// The base detail wrapper (BDW)
        /// </summary>
        private BDWrapper _bdw;

        /// <summary>
        /// Gets or sets the BDW.
        /// </summary>
        /// <value>The BDW.</value>
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

        /// <summary>
        /// The record
        /// </summary>
        private Record _record;

        /// <summary>
        /// Gets or sets the record.
        /// Record identify current record context.
        /// </summary>
        /// <value>The record.</value>
        public Record Record
        {
            get
            {
                if (!ComUtils.Assigned(_record))
                {
                    _record = new Record();
                }
                return _record;
            }
            set { _record = value; }
        }

        /// <summary>
        /// Get or sets default TimeOut
        /// </summary>
        public int DefaultTimeOut { get; set; } = -1;

        /// <summary>
        /// Assigns the access code.
        /// </summary>
        /// <param name="accessCode">The access code.</param>
        /// <returns>System.Int32.</returns>
        public override int AssignAccessCode(object accessCode)
        {
            try
            {
                base.AssignAccessCode(accessCode);
                Err.ErrMsgStack = null;
                Err.ErrCode = 0;

                UserFormState = TFormState.cfsNone;
                NewFormState = TFormState.cfsNone;
                ExTransactionId = -1;
                FormInitialized = false;

                OnAssignAccessCode();

                return 0;
            }
            //catch (MessageException) { throw; } //throwing messages from this method is impossible
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Assigns the access code (method body).
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnAssignAccessCode()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Assigns the start up parameters.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="others">The others.</param>
        /// <returns>System.Int32.</returns>
        public int AssignStartUpParameter(object param, ref object others)
        {
            int result = 0;
            try
            {
                Err.ErrMsgStack = null;
                Err.ErrCode = 0;

                Record.ObjectId = string.Empty;
                BDW.LoadMultiInOneParams(param);

                if (!TUniVar.VarIsArray(param))
                {
                    Record.ObjectId = TUniVar.VarToStr(param);
                    BDW.LoadParams(Record.ObjectId);
                }

                if (BDW.ParamExists("_FormType"))
                    FormType = BDW.Params["_FormType"].AsInt();

                if (BDW.ParamExists("_Silent"))
                    Silent = BDW.Params["_Silent"].AsInt();

                if (BDW.ParamExists("_TransactionId"))
                    ExTransactionId = BDW.Params["_TransactionId"].AsInt();

                OnProcessInputParams();

                if (!BDW.OtherExists(TBDOthers.coObjectTypeId))
                    BDW.AddModifyOther(TBDOthers.coObjectTypeId, ObjectTypeId);

                if (!TUniVar.VarIsNullOrEmpty(FormCaption))
                    BDW.AddModifyOther(TBDOthers.coFormCaption, FormCaption);

                OnAssignStartUpParameter();
            }
            catch (MessageException mex)
            {
                Trace.TraceInformation(mex.Message);
                Err.HandleMessage(mex);
                result = (int)TCSWMK.csWMK;
                Err.ErrCode = result;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "AssignStartUpParameter", false);
                result = (int)TCSWMK.csWMK_Error;
                Err.ErrCode = result;
            }
            finally
            {
                // always set formId and objectId
                // WARNING : there are forms that have an id smaller then 0!!!

                if (!BDW.OtherExists(TBDOthers.coFormId) && ((FormId > 0) || (FormId < -1)))
                    BDW.AddModifyOther(TBDOthers.coFormId, FormId);

                if (!BDW.OtherExists(TBDOthers.coObjectId))
                    BDW.AddModifyOther(TBDOthers.coObjectId, MainObjectId);

                others = BDW.GetOthers();
            }

            return result;
        }

        /// <summary>
        /// Assigns the start up parameters (method body).
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnAssignStartUpParameter()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Processes the input parameters.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnProcessInputParams()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        public void InitializeForm()
        {
            try
            {
                OnInitializeForm();

                FormInitialized = true;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "InitializeForm");
            }
        }

        /// <summary>
        /// Initializes the form (method body).
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnInitializeForm()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        /// <returns>System.Int32.</returns>
        public int GetValue(object fieldName, ref object fieldValue)
        {
            int result = 0;
            try
            {
                OnGetValue(TUniVar.VarToStr(fieldName), ref fieldValue);
            }
            catch (MessageException mex)
            {
                Trace.TraceInformation(mex.Message);
                Err.HandleMessage(mex);
                result = (int)TCSWMK.csWMK;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "GetValue", false);
                result = (int)TCSWMK.csWMK_Error;
            }

            return result;
        }

        /// <summary>
        /// Gets the value (method body).
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnGetValue(string fieldName, ref object fieldValue)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Base class main loop
        /// </summary>
        /// <param name="formState">State of the form.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="messages">The messages.</param>
        /// <param name="others">The others.</param>
        /// <returns>System.Int32.</returns>
        public int SetGetValues(ref object formState, ref object fields, ref object messages, ref object others)
        {
            int result = 0;
            messages = null;
            TFormState fs;

            try
            {
                if (NewFormState > TFormState.cfsNone)
                {
                    formState = (int)NewFormState;
                    NewFormState = TFormState.cfsNone;
                }

                fs = (TFormState)formState;
                switch (fs)
                {
                    case TFormState.cfsView:
                    case TFormState.cfsEdit:
                    case TFormState.cfsNew:
                        UserFormState = fs;
                        break;
                }

                BDW.LoadFields(fields, false);
                BDW.LoadOthers(others);

                if ((!FormInitialized) && (fs != TFormState.cfsCloseBD))
                {
                    InitializeForm();
                }

                switch (fs)
                {
                    case TFormState.cfsNoChanges:
                        result = ActualizeControls();
                        break;
                    case TFormState.cfsView:
                        result = ViewRecord();
                        break;
                    case TFormState.cfsEdit:
                        result = EditRecord();
                        break;
                    case TFormState.cfsNew:
                        result = NewRecord();
                        break;
                    case TFormState.cfsUpdate:
                        result = SaveRecord();
                        break;
                    case TFormState.cfsCancel:
                        result = CancelRecord();
                        break;
                    case TFormState.cfsDelete:
                        result = DeleteRecord();
                        break;
                    case TFormState.cfsButtonPressed:
                        result = ButtonPressed();
                        break;
                }

                fields = BDW.GetFields();
                others = BDW.GetOthers();

                if (NewFormState > TFormState.cfsNone)
                {
                    fs = NewFormState;
                    formState = (int)NewFormState;
                    NewFormState = TFormState.cfsNone;
                }

                if (!BDW.OtherExists(TBDOthers.coCallAgain))
                {
                    if (
                        (fs == TFormState.cfsUpdate) ||
                        (fs == TFormState.cfsCancel) ||
                        (fs == TFormState.cfsDelete))
                    {
                        fields = null;
                    }
                }

                if ((fs == TFormState.cfsCloseBD) && (Err.ErrCode != 0))
                {
                    result = Err.ErrCode;
                    Err.ErrCode = 0;
                }
            }
            catch (MessageException mex)
            {
                Trace.TraceInformation(mex.Message);
                Err.HandleMessage(mex);

                if (Err.SendFields)
                    fields = BDW.GetFields();
                if (Err.SendOthers)
                    others = BDW.GetOthers();
                if (Err.FormState != 0)
                    formState = Err.FormState;
                if (Err.ErrCode != 0)
                    result = Err.ErrCode;
                else
                    result = (int)TCSWMK.csWMK;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "SetGetValues", false);
                result = (int)TCSWMK.csWMK_Error;
            }

            fs = (TFormState)formState;
            switch (fs)
            {
                case TFormState.cfsView:
                case TFormState.cfsEdit:
                case TFormState.cfsNew:
                    UserFormState = fs;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Actualizes the controls.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int ActualizeControls()
        {
            try
            {
                string fieldName = String.Empty;
                object fieldValue = null;

                if (BDW.OtherExists(TBDOthers.coActiveControlFieldName))
                {
                    fieldName = BDW.Others[TBDOthers.coActiveControlFieldName].AsString();
                }

                if (BDW.OtherExists(TBDOthers.coActiveControlValue))
                {
                    fieldValue = BDW.Others[TBDOthers.coActiveControlValue];
                }

                //There are also events based on TBDOthers different then coActiveControlFieldName & coActiveControlValue
                OnActualizeControls(fieldName, fieldValue);

                return 0;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "SetGetValues");
                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Actualizes the controls (method body).
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnActualizeControls(string fieldName, object fieldValue)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Buttons the pressed.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int ButtonPressed()
        {
            try
            {
                string fieldName = String.Empty;

                if (BDW.OtherExists(TBDOthers.coActiveControlFieldName))
                {
                    fieldName = BDW.Others[TBDOthers.coActiveControlFieldName].AsString();
                    OnButtonPressed(fieldName);
                }

                return 0;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "ButtonPressed");
                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Buttons the pressed (method body).
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnButtonPressed(string fieldName)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Creates new record.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int NewRecord()
        {
            //DEVEL: NewRecord - [MP] Co powinno się wydarzyć jeżeli tu wystąpi błąd?
            //                   [AM] Chodzi o coś specjalnego?

            try
            {
                int result = 0;
                Record.Id = -1;

                if (Record.Query.Length > 0)
                {
                    if (Record.ComObj != null)
                    {
                        ComUtils.RecordClose(Record.ComObj);
                        Record.ComObj.Dispose();
                        Record.ComObj = null;
                    }

                    result = ComUtils.OpenResultset(
                        AccessCode,
                        String.Format(Record.Query, Record.Id),
                        Record.KeyName,
                        TransactionId,
                        DefaultTimeOut,
                        out Record.ComObj);

                    Err.Check(result, Record.ComObj);
                }

                OnNewRecord();

                if (!BDW.OtherExists(TBDOthers.coObjectTypeId))
                    BDW.AddModifyOther(TBDOthers.coObjectTypeId, ObjectTypeId);
                if (!BDW.OtherExists(TBDOthers.coObjectId))
                    BDW.AddModifyOther(TBDOthers.coObjectId, MainObjectId);
                if (!TUniVar.VarIsNullOrEmpty(FormCaption))
                    BDW.AddModifyOther(TBDOthers.coFormCaption, FormCaption);

                return result;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "NewRecord");
                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Creates new record (method body).
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnNewRecord()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Checks the before delete.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int CheckBeforeDelete()
        {
            try
            {
                if (!BDW.OtherExists(TBDOthers.coConfirmationResult))
                {
                    bool noDialog = false;
                    try
                    {
                        string errorDescription = String.Empty;
                        int errorCount = OnCheckBeforeDelete(out errorDescription, out noDialog);

                        if (errorCount > 0)
                        {
                            Err.SendOthers = true;
                            Err.MessageRaise(ManagerBaseDef.DIC_P2000000_ID, ManagerBaseDef.MSG_P00010_ID, new object[] { errorDescription });
                        }

                        if (!noDialog)
                            Err.MessageRaise(ManagerBaseDef.DIC_P2000000_ID, ManagerBaseDef.MSG_P00011_ID, null);
                    }
                    finally
                    {
                        if (!noDialog)
                            BDW.AddModifyOther(TBDOthers.coModificationsCancelled, 1);
                    }

                    if (!noDialog)
                        BDW.AddModifyOther(TBDOthers.coConfirmationResult, TConfirmResult.acrYes);
                }

                return 0;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "CheckBeforeDelete");
                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Checks the before delete (method body).
        /// </summary>
        /// <param name="errorDescription">The error description that makes delete operation impossible</param>
        /// <param name="noDialog">Parameter determines whether delete operation should be performed without any dialog</param>
        /// <returns>System.Int32.</returns>
        public virtual int OnCheckBeforeDelete(out string errorDescription, out bool noDialog)
        {
            errorDescription = "";
            noDialog = false;
            return 0;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int DeleteRecord()
        {
            int recordId = Record.Id;

            try
            {
                TransactionStatus = TTransactionStatus.ctsCommitIgnore;

                int result = CheckBeforeDelete();

                TConfirmResult decision = (TConfirmResult)BDW.Others[TBDOthers.coConfirmationResult];
                BDW.DeleteOther(TBDOthers.coConfirmationResult);

                bool proceed = decision == TConfirmResult.acrYes;
                TransactionStatus = TTransactionStatus.ctsCommitEnabled;

                OnDeleteRecord(decision, ref proceed);

                if (!proceed)
                {
                    TransactionStatus = TTransactionStatus.ctsCommitIgnore;
                    BDW.AddModifyOther(TBDOthers.coModificationsCancelled, 1);
                }
                else if (proceed && (Record.Query.Length>0) && (Record.Id != TUniConstants._INT_NULL))
                {
                    ComWrapper com = null;

                    try
                    {
                        result = ComUtils.OpenResultset(
                                                AccessCode,
                                                String.Format(Record.Query, Record.Id),
                                                Record.KeyName,
                                                TransactionId,
                                                DefaultTimeOut,
                                                out com);
                        Err.Check(result, com);

                        result = ComUtils.RecordDelete(ref com);
                        Err.Check(result, com);
                    }
                    finally
                    {
                        com.Dispose();
                    }
                }

                Record.Id = -1;
                Record.Data = null;

                if (Record.ComObj != null)
                {
                    ComUtils.RecordClose(Record.ComObj);
                    Record.ComObj.Dispose();
                    Record.ComObj = null;
                }

                return 0;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Record.Id = recordId;
                TransactionStatus = TTransactionStatus.ctsRollbackRequired;
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "DeleteRecord");
                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Deletes the record (method body).
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnDeleteRecord(TConfirmResult decision, ref bool proceed)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Views the record.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int ViewRecord(bool fromEdit = false)
        {
            try
            {
                int result = 0;

                Record.DataSize = 0;
                Record.Data = null;

                if (Record.Query.Length > 0)
                {
                    if (Record.ComObj != null)
                    {
                        ComUtils.RecordClose(Record.ComObj);
                        Record.ComObj.Dispose();
                        Record.ComObj = null;
                    }

                    result = ComUtils.OpenResultset(
                        AccessCode,
                        String.Format(Record.Query, Record.Id),
                        Record.KeyName,
                        fromEdit ? TransactionId: -1,
                        DefaultTimeOut,
                        out Record.ComObj);
                    Err.Check(result, Record.ComObj);

                    Record.DataSize = ComUtils.RecordPacket(ref Record.ComObj, out Record.Data);
                }

                OnViewRecord();

                if (!fromEdit)
                {
                    ComUtils.RecordClose(Record.ComObj);
                    Record.ComObj.Dispose();
                    Record.ComObj = null;
                }

                return result;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "ViewRecord");
                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Views the record (method body).
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnViewRecord()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Checks the before save.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int CheckBeforeSave()
        {
            try
            {
                if (!BDW.OtherExists(TBDOthers.coConfirmationResult))
                {
                    bool noDialog = false;
                    try
                    {
                        string errorDescription = String.Empty;
                        int errorCount = OnCheckBeforeSave(out errorDescription, out noDialog);

                        if (errorCount > 0)
                        {
                            Err.SendOthers = true;
                            Err.MessageRaise(ManagerBaseDef.DIC_P2000000_ID, ManagerBaseDef.MSG_P00008_ID, new object[] { errorDescription });
                        }

                        if (!noDialog)
                            Err.MessageRaise(ManagerBaseDef.DIC_P2000000_ID, ManagerBaseDef.MSG_P00009_ID, null);
                    }
                    finally
                    {
                        if (!noDialog)
                            BDW.AddModifyOther(TBDOthers.coModificationsCancelled, 1);
                    }

                    if (!noDialog)
                        BDW.AddModifyOther(TBDOthers.coConfirmationResult, TConfirmResult.acrYes);
                }

                return 0;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "CheckBeforeSave");
                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Checks the before save (method body).
        /// </summary>
        /// <param name="errorDescription">The error description.</param>
        /// <param name="noDialog">Parameter determines whether save operation should be performed without any dialog</param>
        /// <returns>System.Int32.</returns>
        public virtual int OnCheckBeforeSave(out string errorDescription, out bool noDialog)
        {
            errorDescription = "";
            noDialog = false;
            return 0;
        }

        /// <summary>
        /// Saves the record.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int SaveRecord()
        {
            try
            {
                TransactionStatus = TTransactionStatus.ctsCommitIgnore;

                CheckBeforeSave();

                TConfirmResult decision = (TConfirmResult)BDW.Others[TBDOthers.coConfirmationResult];
                BDW.DeleteOther(TBDOthers.coConfirmationResult);

                bool proceed = decision == TConfirmResult.acrYes;
                TransactionStatus = TTransactionStatus.ctsCommitEnabled;

                //We can't do anything automatically because we don't know what to Save.
                //Everything need to be done in OnSaveRecord method.
                OnSaveRecord(decision, ref proceed);

                if (!proceed)
                {
                    TransactionStatus = TTransactionStatus.ctsCommitIgnore;
                    BDW.AddModifyOther(TBDOthers.coModificationsCancelled, 1);
                }

                return 0;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "SaveRecord");
                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Saves the record (method body).
        /// </summary>
        /// <param name="decision">Decision passed by user after dialog shown before save operation</param>
        /// <param name="proceed">Parameter that specifies whether save operation should be proceed</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnSaveRecord(TConfirmResult decision, ref bool proceed)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Cancels the record.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int CancelRecord()
        {
            //INFO: When user cancels editing, application ask him for confirmation by itself
            try
            {
                int result = 0;

                TransactionStatus = TTransactionStatus.ctsRollbackRequired;

                OnCancelRecord();

                if (Record.ComObj != null)
                {
                    result = ComUtils.RecordCancel(ref Record.ComObj);
                    Err.Check(result, Record.ComObj);

                    Record.ComObj.Dispose();
                    Record.ComObj = null;
                }

                return result;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "CanceRecord");
                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Cancels the record (method body).
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void OnCancelRecord()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Edits the record.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int EditRecord()
        {
            try
            {
                ViewRecord(true);
                LockRecord();
                OnEditRecord();
                return 0;
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "EditRecord");
                return (int)TCSWMK.csWMK_Error;
            }
        }

        /// <summary>
        /// Locks the record.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private void LockRecord()
        {
            try
            {
                if (Record.Query.Length > 0)
                {
                    int lockerId = -1;

                    int result = ComUtils.RecordEdit(ref Record.ComObj, out lockerId);

                    if (lockerId > 0)
                    {
                        Err.MessageComRaise(Record.ComObj);
                    }
                    else
                    {
                        Err.Check(result, Record.ComObj);
                    }
                }
            }
            catch (MessageException) { throw; }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "LockRecord");
            }
        }

        /// <summary>
        /// Additional lock record activity body
        /// </summary>
        public virtual void OnEditRecord()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Supports the SQL (builds the query).
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="sqlArray">The SQL array.</param>
        /// <returns>System.Int32.</returns>
        public int SupportSQL(object methodName, object param, ref object sqlArray)
        {
            int result;

            try
            {
                result = OnSupportSQL(TUniVar.VarToStr(methodName), param, ref sqlArray);
            }
            //catch (MessageException) { throw; } //throwing messages from this method is impossible
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                result = (int)TCSWMK.csWMK_Error;
            }

            try
            {
                CheckTransactionState(result);
            }
            catch { }

            return result;
        }

        /// <summary>
        /// Supports the SQL (method body).
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="sqlArray">The SQL array.</param>
        /// <returns>System.Int32.</returns>
        public virtual int OnSupportSQL(string methodName, object param, ref object sqlArray)
        {
            return 0;
        }

        /// <summary>
        /// Assigns the WMK result.
        /// </summary>
        /// <param name="wmkResult">The WMK result.</param>
        /// <returns>System.Int32.</returns>
        public virtual int AssignWMKResult(object wmkResult)
        {
            BDW.AddModifyOther(TBDOthers.coConfirmationResult, wmkResult);
            return 0;
        }

        /// <summary>
        /// Gets the last error description.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>System.Int32.</returns>
        public virtual int GetLastErrorDescription(ref object error)
        {
            error = Err.ErrMsgStack;
            Err.ErrMsgStack = null;
            Err.ErrCode = 0;
            return 0;
        }

        /// <summary>
        /// Runs the method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>System.Int32.</returns>
        public int RunMethod(object methodName, ref object param)
        {
            int result;
            try
            {
                return OnRunMethod(TUniVar.VarToStr(methodName), ref param);
            }
            catch (MessageException mex)
            {
                Trace.TraceInformation(mex.Message);
                Err.HandleMessage(mex);

                if (Err.ErrCode != 0)
                    result = Err.ErrCode;
                else
                    result = (int)TCSWMK.csWMK;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Err.HandleException(ex, "RunMethod", false);

                if (Err.ErrCode != 0)
                    result = Err.ErrCode;
                else
                    result = (int)TCSWMK.csWMK_Error;
            }

            try
            {
                CheckTransactionState(result);
            }
            catch { }

            return result;
        }

        /// <summary>
        /// Runs the method (method body).
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>System.Int32.</returns>
        public virtual int OnRunMethod(string methodName, ref object param)
        {
            return 0;
        }

        /// <summary>
        /// Checks the state of the transaction.
        /// </summary>
        /// <param name="result">The result to check.</param>
        private void CheckTransactionState(int result)
        {
            //DEVEL: CheckTransactionState - [MP] Na podstawie czego ta metoda powinna działać: Result czy TransactionStatus?
            //                              [AM] Na podstawie jednego i drugiego
            //DEVEL: CheckTransactionState - [MP] Gdzie i jak ta metoda powinna być wołana uwzględniając konwersacje z użytkownikiem?
            //                              [AM] SupportSQL, RunMethod

            if (ExTransactionId > 0) // external transaction
            {
                TransactionStatus = TTransactionStatus.ctsCommitIgnore;
            }
            else
            {
                if (TUniTools.ErrorDetected(result)) // internal transaction
                {
                    Transaction_Rollback();          // error notification
                }
                else
                {
                    switch (TransactionStatus)
                    {
                        case TTransactionStatus.ctsRollbackRequired:
                            Transaction_Rollback();
                            break;
                        case TTransactionStatus.ctsCommitEnabled:
                            Transaction_Commit();
                            break;
                        case TTransactionStatus.ctsCommitIgnore:
                            break; // nothing to do
                    }
                }
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        protected ManagerBase() : base()
        {
            OnInitialize();
        }

        /// <summary>
        /// Implements initialization of the component's state (method body).
        /// </summary>
        protected virtual void OnInitialize()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Free unmanaged resources
        /// </summary>
        ~ManagerBase()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// Flag indicating if object was already disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Dispose resources.
        /// </summary>
        /// <param name="disposing">Indicate call from Dispose or Finalizer</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                try
                {
                    OnDeinitialize(disposing);
                }
                catch { }

                if (disposing)
                {
                    Language.Dispose();
                }

                // INFO: free unmanaged resources (unmanaged objects) and override finalizer
                // INFO: set large fields to null
                _disposed = true;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Implements deinitialization of the component's state (method body).
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void OnDeinitialize(bool disposing)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Default WMK Handler
        /// </summary>
        /// <param name="wmk"></param>
        public virtual void HandleWMK(object wmk)
        {
            if (Err != null)
            {
                var _wmk = Err.ErrMsgStack;
                TWMK.AddMessages(ref _wmk, wmk);
                Err.ErrMsgStack = _wmk;
            }
        }
    }
}
