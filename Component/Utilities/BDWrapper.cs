// ***********************************************************************
// Assembly         : Component
// Author           : Artur Maciejowski
// Created          : 16-02-2020
//
// Last Modified By : Artur Maciejowski
// Last Modified On : 02-04-2020
// ***********************************************************************
// <copyright file="BDWrapper.cs" company="DomConsult Sp. z o.o.">
//     Copyright ©  2021 All rights reserved
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace DomConsult.GlobalShared.Utilities
{
    /// <summary>
    /// Enum TFormState
    /// </summary>
    public enum TFormState
    {
        /// <summary>
        /// The CFS none
        /// </summary>
        cfsNone = 0x0,
        /// <summary>
        /// The CFS changes forbidden
        /// </summary>
        cfsChangesForbidden = 0x1,
        /// <summary>
        /// The CFS no changes
        /// </summary>
        cfsNoChanges = 0x8,
        /// <summary>
        /// The CFS view
        /// </summary>
        cfsView = 0x10,
        /// <summary>
        /// The CFS edit
        /// </summary>
        cfsEdit = 0x20,
        /// <summary>
        /// The CFS new
        /// </summary>
        cfsNew = 0x40,
        /// <summary>
        /// The CFS update
        /// </summary>
        cfsUpdate = 0x80,
        /// <summary>
        /// The CFS cancel
        /// </summary>
        cfsCancel = 0x100,
        /// <summary>
        /// The CFS delete
        /// </summary>
        cfsDelete = 0x200,
        /// <summary>
        /// The CFS close bd
        /// </summary>
        cfsCloseBD = 0x400,
        /// <summary>
        /// The CFS ask again
        /// </summary>
        cfsAskAgain = 0x800,
        /// <summary>
        /// The CFS button pressed
        /// </summary>
        cfsButtonPressed = 0x1000
    }

    /// <summary>
    /// Enum TBDFieldProps
    /// </summary>
    public enum TBDFieldProps
    {
        /// <summary>
        /// The c cell hint
        /// </summary>
        cCellHint,
        /// <summary>
        /// The c cell read only
        /// </summary>
        cCellReadOnly,
        /// <summary>
        /// The c cell value
        /// </summary>
        cCellValue,
        /// <summary>
        /// The c color editable
        /// </summary>
        cColorEditable,
        /// <summary>
        /// The c color read only
        /// </summary>
        cColorReadOnly,
        /// <summary>
        /// The c columns information
        /// </summary>
        cColumnsInfo,
        /// <summary>
        /// The c data type
        /// </summary>
        cDataType,
        /// <summary>
        /// The c dummy
        /// </summary>
        cDummy,
        /// <summary>
        /// The c dummy2
        /// </summary>
        cDummy2,
        /// <summary>
        /// The c dummy3
        /// </summary>
        cDummy3,
        /// <summary>
        /// The c dummy4
        /// </summary>
        cDummy4,
        /// <summary>
        /// The c dummy5
        /// </summary>
        cDummy5,
        /// <summary>
        /// The c enabled
        /// </summary>
        cEnabled,
        /// <summary>
        /// The c font color
        /// </summary>
        cFontColor,
        /// <summary>
        /// The c font name
        /// </summary>
        cFontName,
        /// <summary>
        /// The c font size
        /// </summary>
        cFontSize,
        /// <summary>
        /// The c font style
        /// </summary>
        cFontStyle,
        /// <summary>
        /// The c grid column
        /// </summary>
        cGridColumn,
        /// <summary>
        /// The c grid row
        /// </summary>
        cGridRow,
        /// <summary>
        /// The c hint
        /// </summary>
        cHint,
        /// <summary>
        /// The c hint map
        /// </summary>
        cHintMap,
        /// <summary>
        /// The c input status
        /// </summary>
        cInputStatus,
        /// <summary>
        /// The c maximum length
        /// </summary>
        cMaxLength,
        /// <summary>
        /// The c modify status
        /// </summary>
        cModifyStatus,
        /// <summary>
        /// The c multi selection
        /// </summary>
        cMultiSelection,
        /// <summary>
        /// The c object type identifier
        /// </summary>
        cObjectTypeId,
        /// <summary>
        /// The c password character
        /// </summary>
        cPasswordChar,
        /// <summary>
        /// The c read only map
        /// </summary>
        cReadOnlyMap,
        /// <summary>
        /// The c SQL
        /// </summary>
        cSQL,
        /// <summary>
        /// The c SQL sort by
        /// </summary>
        cSQLSortBy,
        /// <summary>
        /// The c tool identifier
        /// </summary>
        cToolId,
        /// <summary>
        /// The c visible
        /// </summary>
        cVisible
    }

    /// <summary>
    /// Enum TBDOthers
    /// </summary>
    public enum TBDOthers
    {
        /// <summary>
        /// The co active control field name
        /// </summary>
        coActiveControlFieldName,
        /// <summary>
        /// The co active control value
        /// </summary>
        coActiveControlValue,
        /// <summary>
        /// The co actualization type
        /// </summary>
        coActualizationType,
        /// <summary>
        /// The co add all fields to call again
        /// </summary>
        coAddAllFieldsToCallAgain,
        /// <summary>
        /// The co add all field values to array
        /// </summary>
        coAddAllFieldValuesToArray,
        /// <summary>
        /// The co add editable field values to array
        /// </summary>
        coAddEditableFieldValuesToArray,
        /// <summary>
        /// The co add search control to fields
        /// </summary>
        coAddSearchControlToFields,
        /// <summary>
        /// The co allow direct edit
        /// </summary>
        coAllowDirectEdit,
        /// <summary>
        /// The co allow new form connected with list
        /// </summary>
        coAllowNewFormConnectedWithList,
        /// <summary>
        /// The co allow processing
        /// </summary>
        coAllowProcessing,
        /// <summary>
        /// The co bd token
        /// </summary>
        coBDToken,
        /// <summary>
        /// The co button click from dd grid
        /// </summary>
        coButtonClickFromDDGrid,
        /// <summary>
        /// The co call again
        /// </summary>
        coCallAgain,
        /// <summary>
        /// The co cancel call again
        /// </summary>
        coCancelCallAgain,
        /// <summary>
        /// The co cancel messages
        /// </summary>
        coCancelMessages,
        /// <summary>
        /// The co clear all fields
        /// </summary>
        coClearAllFields,
        /// <summary>
        /// The co click dd button
        /// </summary>
        coClickDDButton,
        /// <summary>
        /// The co client variable
        /// </summary>
        coClientVariable,
        /// <summary>
        /// The co client version
        /// </summary>
        coClientVersion,
        /// <summary>
        /// The co confirmation result
        /// </summary>
        coConfirmationResult,
        /// <summary>
        /// The co custom command
        /// </summary>
        coCustomCommand,
        /// <summary>
        /// The co default values get for component
        /// </summary>
        coDefaultValuesGetForComponent,
        /// <summary>
        /// The co default values possible
        /// </summary>
        coDefaultValuesPossible,
        /// <summary>
        /// The co default values save for component in
        /// </summary>
        coDefaultValuesSaveForComponentIn,
        /// <summary>
        /// The co default values save for component out
        /// </summary>
        coDefaultValuesSaveForComponentOut,
        /// <summary>
        /// The co disabled function
        /// </summary>
        coDisabledFunction,
        /// <summary>
        /// The co enter like tab
        /// </summary>
        coEnterLikeTab,
        /// <summary>
        /// The co execute tool at client
        /// </summary>
        coExecuteToolAtClient,
        /// <summary>
        /// The co field name to activate
        /// </summary>
        coFieldNameToActivate,
        /// <summary>
        /// The co force call set get on exit
        /// </summary>
        coForceCallSetGetOnExit,
        /// <summary>
        /// The co force refresh
        /// </summary>
        coForceRefresh,
        /// <summary>
        /// The co force return all field name
        /// </summary>
        coForceReturnAllFieldName,
        /// <summary>
        /// The co form caption
        /// </summary>
        coFormCaption,
        /// <summary>
        /// The co form definition
        /// </summary>
        coFormDefinition,
        /// <summary>
        /// The co form identifier
        /// </summary>
        coFormId,
        /// <summary>
        /// The co graf assign flag
        /// </summary>
        coGRAFAssignFlag,
        /// <summary>
        /// The co graf assign result
        /// </summary>
        coGRAFAssignResult,
        /// <summary>
        /// The co graf assign type
        /// </summary>
        coGRAFAssignType,
        /// <summary>
        /// The co grafcom name
        /// </summary>
        coGRAFCOMName,
        /// <summary>
        /// The co graffk value
        /// </summary>
        coGRAFFKValue,
        /// <summary>
        /// The co grafpk value
        /// </summary>
        coGRAFPKValue,
        /// <summary>
        /// The co ignore dd buttons for fields array
        /// </summary>
        coIgnoreDDButtonsForFIELDSArray,
        /// <summary>
        /// The co inform about parent close
        /// </summary>
        coInformAboutParentClose,
        /// <summary>
        /// The co inform about user decision
        /// </summary>
        coInformAboutUserDecision,
        /// <summary>
        /// The co in parameters
        /// </summary>
        coInParams,
        /// <summary>
        /// The co lock save method
        /// </summary>
        coLockSaveMethod,
        /// <summary>
        /// The co mandant context
        /// </summary>
        coMandantContext,
        /// <summary>
        /// The co mandant enable
        /// </summary>
        coMandantEnable,
        /// <summary>
        /// The co mandant identifier
        /// </summary>
        coMandantId,
        /// <summary>
        /// The co modifications cancelled
        /// </summary>
        coModificationsCancelled,
        /// <summary>
        /// The co null
        /// </summary>
        coNull,
        /// <summary>
        /// The co object identifier
        /// </summary>
        coObjectId,
        /// <summary>
        /// The co object type identifier
        /// </summary>
        coObjectTypeId,
        /// <summary>
        /// The co rebuild form
        /// </summary>
        coRebuildForm,
        /// <summary>
        /// The co record values modified
        /// </summary>
        coRecordValuesModified,
        /// <summary>
        /// The co refresh all dictionaries
        /// </summary>
        coRefreshAllDictionaries,
        /// <summary>
        /// The co required fields array
        /// </summary>
        coRequiredFieldsArray,
        /// <summary>
        /// The co return field name during cancel
        /// </summary>
        coReturnFieldNameDuringCancel,
        /// <summary>
        /// The co select mandant
        /// </summary>
        coSelectMandant,
        /// <summary>
        /// The co set get value after message
        /// </summary>
        coSetGetValueAfterMessage,
        /// <summary>
        /// The co set get value on close
        /// </summary>
        coSetGetValueOnClose,
        /// <summary>
        /// The co tool bar visibility
        /// </summary>
        coToolBarVisibility
    }


    /// <summary>
    /// Class BDField.
    /// </summary>
    public class BDField
    {
        /// <summary>
        /// The property list
        /// </summary>
        private Dictionary<int, object> PropList = new Dictionary<int, object>();

        /// <summary>
        /// The field name
        /// </summary>
        public string FieldName;
        /// <summary>
        /// The field value
        /// </summary>
        public object FieldValue;
        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public object Properties
        {
            get
            {
                return GetProperties();
            }
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <returns>System.Object.</returns>
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

        /// <summary>
        /// The dictionary
        /// </summary>
        public object Dictionary;

        /// <summary>
        /// Gets as string.
        /// </summary>
        /// <value>As string.</value>
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

        /// <summary>
        /// Gets as int.
        /// </summary>
        /// <value>As int.</value>
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

        /// <summary>
        /// Gets as variable.
        /// </summary>
        /// <value>As variable.</value>
        public object AsVar
        {
            get
            {
                return FieldValue;
            }
        }

        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="PropId">The property identifier.</param>
        /// <param name="PropValue">The property value.</param>
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

    /// <summary>
    /// Class BDWrapper.
    /// </summary>
    public class BDWrapper
    {
        /// <summary>
        /// The field list
        /// </summary>
        Dictionary<string, BDField> FieldList = new Dictionary<string, BDField>();
        /// <summary>
        /// The other list
        /// </summary>
        Dictionary<TBDOthers, object> OtherList = new Dictionary<TBDOthers, object>();
        /// <summary>
        /// The parameter list
        /// </summary>
        Dictionary<string, string> ParamList = new Dictionary<string, string>();
        /// <summary>
        /// Adds the modify field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>BDField.</returns>
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

        /// <summary>
        /// Adds the modify field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldValue">The field value.</param>
        /// <returns>BDField.</returns>
        public BDField AddModifyField(string fieldName, object fieldValue)
        {
            return AddModifyField(fieldName, fieldValue, null);
        }

        /// <summary>
        /// Adds the modify other.
        /// </summary>
        /// <param name="otherId">The other identifier.</param>
        /// <param name="otherValue">The other value.</param>
        public void AddModifyOther(TBDOthers otherId, object otherValue)
        {
            OtherList[otherId] = otherValue;
        }

        /// <summary>
        /// Adds the modify parameter.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        public void AddModifyParam(string paramName, object paramValue)
        {
            ParamList[paramName] = paramValue.ToString();
        }

        /// <summary>
        /// Others the exists.
        /// </summary>
        /// <param name="otherId">The other identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool OtherExists(TBDOthers otherId)
        {
            return OtherList.ContainsKey(otherId);
        }

        /// <summary>
        /// Fields the exists.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool FieldExists(string fieldName)
        {
            return FieldList.ContainsKey(fieldName);
        }

        /// <summary>
        /// Parameters the exists.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ParamExists(string paramName)
        {
            return ParamList.ContainsKey(paramName);
        }

        /// <summary>
        /// Gets the <see cref="BDField"/> with the specified field name.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>BDField.</returns>
        public BDField this[string fieldName]
        {
            get
            {
                return FieldList[fieldName];
            }
        }

        /// <summary>
        /// Deletes the other.
        /// </summary>
        /// <param name="otherId">The other identifier.</param>
        /// <returns>System.Object.</returns>
        public object DeleteOther(TBDOthers otherId)
        {
            return OtherList.Remove(otherId);
        }

        /// <summary>
        /// Deletes the parameter.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>System.Object.</returns>
        public object DeleteParam(string paramName)
        {
            return ParamList.Remove(paramName);
        }

        /// <summary>
        /// Fieldses the specified field name.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>System.Object.</returns>
        public object Fields(string fieldName)
        {
            return FieldList[fieldName];
        }

        /// <summary>
        /// Otherses the specified other identifier.
        /// </summary>
        /// <param name="otherId">The other identifier.</param>
        /// <returns>System.Object.</returns>
        public object Others(TBDOthers otherId)
        {
            return OtherList[otherId];
        }

        /// <summary>
        /// Parameterses the specified parameter name.
        /// </summary>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>System.Object.</returns>
        public object Params(string paramName)
        {
            return ParamList[paramName];
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        public void ClearFields()
        {
            FieldList.Clear();
        }

        /// <summary>
        /// Clears the others.
        /// </summary>
        public void ClearOthers()
        {
            OtherList.Clear();
        }

        /// <summary>
        /// Loads the fields.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="clear">if set to <c>true</c> [clear].</param>
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

        /// <summary>
        /// Loads the others.
        /// </summary>
        /// <param name="others">The others.</param>
        /// <param name="clear">if set to <c>true</c> [clear].</param>
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

        /// <summary>
        /// Loads the parameters.
        /// </summary>
        /// <param name="ParamsStr">The parameters string.</param>
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

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <returns>System.Object.</returns>
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

        /// <summary>
        /// Gets the others.
        /// </summary>
        /// <returns>System.Object.</returns>
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
