using System;
using System.Collections.Generic;
using System.Linq;

namespace DomConsult.GlobalShared.Utilities
{
    public enum TFormState
    {
        cfsNone = 0x0,
        cfsChangesForbidden = 0x1,
        cfsNoChanges = 0x8,
        cfsView = 0x10,
        cfsEdit = 0x20,
        cfsNew = 0x40,
        cfsUpdate = 0x80,
        cfsCancel = 0x100,
        cfsDelete = 0x200,
        cfsCloseBD = 0x400,
        cfsAskAgain = 0x800,
        cfsButtonPressed = 0x1000
    }

    public enum TBDFieldProps
    {
        cCellHint,
        cCellReadOnly,
        cCellValue,
        cColorEditable,
        cColorReadOnly,
        cColumnsInfo,
        cDataType,
        cDummy,
        cDummy2,
        cDummy3,
        cDummy4,
        cDummy5,
        cEnabled,
        cFontColor,
        cFontName,
        cFontSize,
        cFontStyle,
        cGridColumn,
        cGridRow,
        cHint,
        cHintMap,
        cInputStatus,
        cMaxLength,
        cModifyStatus,
        cMultiSelection,
        cObjectTypeId,
        cPasswordChar,
        cReadOnlyMap,
        cSQL,
        cSQLSortBy,
        cToolId,
        cVisible
    }

    public enum TBDOthers
    {
        coActiveControlFieldName,
        coActiveControlValue,
        coActualizationType,
        coAddAllFieldsToCallAgain,
        coAddAllFieldValuesToArray,
        coAddEditableFieldValuesToArray,
        coAddSearchControlToFields,
        coAllowDirectEdit,
        coAllowNewFormConnectedWithList,
        coAllowProcessing,
        coBDToken,
        coButtonClickFromDDGrid,
        coCallAgain,
        coCancelCallAgain,
        coCancelMessages,
        coClearAllFields,
        coClickDDButton,
        coClientVariable,
        coClientVersion,
        coConfirmationResult,
        coCustomCommand,
        coDefaultValuesGetForComponent,
        coDefaultValuesPossible,
        coDefaultValuesSaveForComponentIn,
        coDefaultValuesSaveForComponentOut,
        coDisabledFunction,
        coEnterLikeTab,
        coExecuteToolAtClient,
        coFieldNameToActivate,
        coForceCallSetGetOnExit,
        coForceRefresh,
        coForceReturnAllFieldName,
        coFormCaption,
        coFormDefinition,
        coFormId,
        coGRAFAssignFlag,
        coGRAFAssignResult,
        coGRAFAssignType,
        coGRAFCOMName,
        coGRAFFKValue,
        coGRAFPKValue,
        coIgnoreDDButtonsForFIELDSArray,
        coInformAboutParentClose,
        coInformAboutUserDecision,
        coInParams,
        coLockSaveMethod,
        coMandantContext,
        coMandantEnable,
        coMandantId,
        coModificationsCancelled,
        coNull,
        coObjectId,
        coObjectTypeId,
        coRebuildForm,
        coRecordValuesModified,
        coRefreshAllDictionaries,
        coRequiredFieldsArray,
        coReturnFieldNameDuringCancel,
        coSelectMandant,
        coSetGetValueAfterMessage,
        coSetGetValueOnClose,
        coToolBarVisibility
    }


    public class BDField
    {
        private Dictionary<int, object> PropList = new Dictionary<int, object>();

        public string FieldName;
        public object FieldValue;
        public object Properties
        {
            get
            {
                return GetProperties();
            }
        }

        public object GetProperties()
        {
            object[,] prop;

            if (PropList.Count == 0)
            {
                return null;
            }

            prop = new object[PropList.Count, 2];

            for (int i = 0; i < PropList.Count; i++)
            {
                prop[i, 0] = PropList.ElementAt(i).Key;
                prop[i, 1] = PropList.ElementAt(i).Value;
            }

            return prop;
        }

        public object Dictionary;

        public string AsString
        {
            get
            {
                if (FieldValue == null)
                {
                    return String.Empty;
                }
                else
                {
                    return FieldValue.ToString();
                }
            }
        }

        public int AsInt
        {
            get
            {
                if (FieldValue == null)
                    return -1;
                else
                    return (int)FieldValue;
            }
        }

        public object AsVar
        {
            get
            {
                return FieldValue;
            }
        }

        public void AddProperty(int PropId, object PropValue)
        {
            object prop;

            if (!PropList.TryGetValue(PropId, out prop))
            {
                prop = new object();
            }
            prop = PropValue;
            PropList[PropId] = prop;
        }
    }

    public class BDWrapper
    {
        Dictionary<string, BDField> FieldList = new Dictionary<string, BDField>();
        Dictionary<TBDOthers, object> OtherList = new Dictionary<TBDOthers, object>();
        Dictionary<string, string> ParamList = new Dictionary<string, string>();
        public BDField AddModifyField(string fieldName, object fieldValue, object dictionary)
        {
            BDField field;

            if (!FieldList.TryGetValue(fieldName, out field))
            {
                field = new BDField();
                field.FieldName = fieldName;
            }

            field.FieldValue = fieldValue;
            field.Dictionary = dictionary;

            FieldList[fieldName] = field;
            return field;
        }

        public BDField AddModifyField(string fieldName, object fieldValue)
        {
            return AddModifyField(fieldName, fieldValue, null);
        }

        public void AddModifyOther(TBDOthers otherId, object otherValue)
        {
            OtherList[otherId] = otherValue;
        }

        public void AddModifyParam(string paramName, object paramValue)
        {
            ParamList[paramName] = paramValue.ToString();
        }

        public bool OtherExists(TBDOthers otherId)
        {
            return OtherList.ContainsKey(otherId);
        }

        public bool FieldExists(string fieldName)
        {
            return FieldList.ContainsKey(fieldName);
        }

        public bool ParamExists(string paramName)
        {
            return ParamList.ContainsKey(paramName);
        }

        public BDField this[string fieldName]
        {
            get
            {
                return FieldList[fieldName];
            }
        }

        public object DeleteOther(TBDOthers otherId)
        {
            return OtherList.Remove(otherId);
        }

        public object DeleteParam(string paramName)
        {
            return ParamList.Remove(paramName);
        }

        public object Fields(string fieldName)
        {
            return FieldList[fieldName];
        }

        public object Others(TBDOthers otherId)
        {
            return OtherList[otherId];
        }

        public object Params(string paramName)
        {
            return ParamList[paramName];
        }

        public void ClearFields()
        {
            FieldList.Clear();
        }

        public void ClearOthers()
        {
            OtherList.Clear();
        }

        public void LoadFields(object fields, bool clear = true)
        {
            if (clear)
            {
                ClearFields();
            }

            if (fields == null)
            {
                return;
            }

            object[,] fieldArray = (object[,])fields;

            int count = fieldArray.GetUpperBound(0) + 1;

            for (int i = 0; i < count; i++)
            {
                AddModifyField(fieldArray[i, 0].ToString(), fieldArray[i, 1], fieldArray[i, 2]);
            }
        }

        public void LoadOthers(object others, bool clear = true)
        {
            if (clear)
            {
                ClearOthers();
            }

            if (others == null)
            {
                return;
            }    

            object[,] otherArray = (object[,])others;

            int count = otherArray.GetUpperBound(0) + 1;

            for (int i = 0; i < count; i++)
            {
                AddModifyOther((TBDOthers)otherArray[i, 0], otherArray[i, 1]);
            }
        }

        public void LoadParams(string ParamsStr)
        {
            string[] separators = new string[] { "/" };
            string[] paramsTable = ParamsStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            int count = paramsTable.GetUpperBound(0) + 1;

            separators = new string[] { "=" };

            string[] param;

            for (int i = 0; i < count; i++)
            {
                param = paramsTable[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);

                if (param.GetUpperBound(0) > 0)
                    ParamList[param[0]] = param[1];
                else
                    ParamList[param[0]] = String.Empty;
            }
        }

        public object GetFields()
        {
            object[,] fields;

            fields = new object[FieldList.Count, 4];

            for (int i = 0; i < FieldList.Count; i++)
            {
                fields[i, 0] = FieldList.ElementAt(i).Value.FieldName;
                fields[i, 1] = FieldList.ElementAt(i).Value.FieldValue;
                fields[i, 2] = FieldList.ElementAt(i).Value.Dictionary;
                fields[i, 3] = FieldList.ElementAt(i).Value.Properties;
            }

            return fields;
        }

        public object GetOthers()
        {
            object[,] others;

            others = new object[OtherList.Count, 2];

            for (int i = 0; i < OtherList.Count; i++)
            {
                others[i, 0] = OtherList.ElementAt(i).Key;
                others[i, 1] = OtherList.ElementAt(i).Value;
            }

            return others;
        }
    }
}
