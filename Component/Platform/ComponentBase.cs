// ***********************************************************************
// Assembly         : Component
// Author           : Artur Maciejowski
// Created          : 16-02-2020
//
// Last Modified By : Artur Maciejowski
// Last Modified On : 28-10-2020
// ***********************************************************************
// <copyright file="ComponentBase.cs" company="DomConsult Sp. z o.o.">
//     Copyright ©  2021 All rights reserved
// </copyright>
// <summary></summary>
// ***********************************************************************

//TODO: ManagerBaseDef - [MP] Language manager vs. Cache
//TODO: ManagerBaseDef - [MP] FormId, FormCaption, ObjectTypeId, MainObjectId, : czy set-ery tych właściwości nie powinny robić BDW.AddModifyOther?
//TODO: ManagerBaseDef - [MP] Trace.TraceXXX - używanie Trace jest raczej złym pomysłem. Trzeba przejść na Logger-a i ewentualnie coś co rzuca info na consolę.

using System;
using System.Diagnostics;
using DomConsult.Platform.Extensions;
using DomConsult.GlobalShared.Utilities;
using DomConsult.Components.Interfaces;

namespace DomConsult.Platform
{
    public static class ManagerBaseDef
    {
        //sp_devGenerateMTSCOMDIC @MTSCOMID=2000000, @DICPREFIX='P', @TEXTIDIN='7,8,9,10,11', @LANG='C#'

        /// <summary>
        /// Platformowy słownik z uniwersalnymi wielojęzycznymi komunikatami i tekstami.
        /// </summary>
        public static int MtsComDicShared_ID  = 2000000;
        /// <summary>
        /// Wykonanie operacji nie jest możliwe. Funkcjonalność jest wyłączona.
        /// </summary>
        /// <remarks>
        /// Type:Informacja, Buttons:OK, Result: Informacja
        /// </remarks>
        public static int MSG_P00007_ID = 7;
        public static string MSG_P00007 = "Wykonanie operacji nie jest możliwe. Funkcjonalność jest wyłączona.";

        /// <summary>
        /// Dane nie mogą zostać zapisane. Wystąliły błędy walidacji: %s
        /// </summary>
        /// <remarks>
        /// Type:Błąd, Buttons:OK, Result: Błąd
        /// </remarks>
        public static int MSG_P00008_ID = 8;
        public static string MSG_P00008 = "Dane nie mogą zostać zapisane. Wystąliły błędy walidacji:" + Environment.NewLine +
                     "%s";

        /// <summary>
        /// Czy zapisać wprowadzone zmiany?
        /// </summary>
        /// <remarks>
        /// Type:Potwierdzenie, Buttons:OKCANCEL, Result: Powtórzenie
        /// </remarks>
        public static int MSG_P00009_ID = 9;
        public static string MSG_P00009 = "Czy zapisać wprowadzone zmiany?";

        /// <summary>
        /// Nie można usunąć rekordu. Wystąpiły błędy walidacji: %s
        /// </summary>
        /// <remarks>
        /// Type:Błąd, Buttons:OK, Result: Błąd
        /// </remarks>
        public static int MSG_P00010_ID = 10;
        public static string MSG_P00010 = "Nie można usunąć rekordu. Wystąpiły błędy walidacji:" + Environment.NewLine +
                     "%s";

        /// <summary>
        /// Czy na pewno usunąć rekord?
        /// </summary>
        /// <remarks>
        /// Type:Potwierdzenie, Buttons:OKCANCEL, Result: Powtórzenie
        /// </remarks>
        public static int MSG_P00011_ID = 11;
        public static string MSG_P00011 = "Czy na pewno usunąć rekord?";
    }

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
            get { return _err; }
        }
        /// <summary>
        /// The MTS COM identifier (private)
        /// </summary>
        private int _mtsComId;
        /// <summary>
        /// Gets or sets the MTS COM identifier.
        /// </summary>
        /// <value>The MTS COM identifier.</value>
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
        public new int AssignAccessCode(object accessCode)
        {
            try
            {
                base.AssignAccessCode(accessCode);

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
                BDW.AddModifyOther(TBDOthers.coAddAllFieldValuesToArray, 1);

                if (TUniVar.VarIsArray(param))
                {
                    Record.ObjectId = string.Empty;
                    BDW.LoadMultiInOneParams(param);
                }
                else
                {
                    Record.ObjectId = TUniVar.VarToStr(param);
                    BDW.LoadParams(Record.ObjectId);
                }

                if (BDW.ParamExists("_Silent"))
                    Silent = BDW.Params["_Silent"].AsInt();

                if (BDW.ParamExists("_TransactionId"))
                    ExTransactionId = BDW.Params["_TransactionId"].AsInt();

                OnAssignStartUpParameter();

                //if (BDW.ParamExists(Record.KeyName))
                //    Record.Id = BDW.Params[Record.KeyName].AsInt();

                if (!BDW.OtherExists(TBDOthers.coObjectTypeId))
                    BDW.AddModifyOther(TBDOthers.coObjectTypeId, ObjectTypeId);
               
                if (!TUniVar.VarIsNullOrEmpty(FormCaption))
                    BDW.AddModifyOther(TBDOthers.coFormCaption, FormCaption);

                OnProcessInputParams();
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
                Err.HandleException(ex, "AssignStartUpParameter", false);
                result = (int)TCSWMK.csWMK_Error;
            }
            finally
            {
                // always set formId and objectId
                // WARNING : thera are forms that have an id smaller then 0!!!

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

                if (!FormInitialized)
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
            //TODO: NewRecord - [MP] Co powinno się wydarzyć jeżeli tu wystąpi błąd?

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
                            Err.MessageRaise(ManagerBaseDef.MtsComDicShared_ID, ManagerBaseDef.MSG_P00010_ID, new object[] { errorDescription });
                        }

                        if (!noDialog)
                            Err.MessageRaise(ManagerBaseDef.MtsComDicShared_ID, ManagerBaseDef.MSG_P00011_ID, null);
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
        /// <param name="errorDescription">The error description.</param>
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
                            Err.MessageRaise(ManagerBaseDef.MtsComDicShared_ID, ManagerBaseDef.MSG_P00008_ID, new object[] { errorDescription });
                        }

                        if (!noDialog)
                            Err.MessageRaise(ManagerBaseDef.MtsComDicShared_ID, ManagerBaseDef.MSG_P00009_ID, null);
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
        /// <param name="decision">State of the form.</param>
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
                result = OnRunMethod(TUniVar.VarToStr(methodName), ref param);
                return result;
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
        /// <param name="result">The result.</param>
        /// <returns>System.Int32.</returns>
        private int CheckTransactionState(int result)
        {
            //TODO: CheckTransactionState - [MP] Na podstawie czego ta metoda powinna działać: Result czy TransactionStatus?
            //TODO: CheckTransactionState - [MP] Gdzie i jak ta metoda powinna być wołana uwzględniając konwersacje z użytkownikiem?

            if (TransactionStatus == TTransactionStatus.ctsCommitEnabled)
                Transaction_Commit();
            else if (TransactionStatus == TTransactionStatus.ctsRollbackRequired)
                Transaction_Rollback();

            return result;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ManagerBase() : base()
        {
            OnInitialize();
        }

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

        protected virtual void OnDeinitialize(bool disposing)
        {
            //throw new NotImplementedException();
        }
    }
}
