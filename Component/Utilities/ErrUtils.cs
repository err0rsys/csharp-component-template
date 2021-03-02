// ***********************************************************************
// Assembly         : Component
// Author           : Artur Maciejowski
// Created          : 16-02-2020
//
// Last Modified By : Artur Maciejowski
// Last Modified On : 24-02-2020
// ***********************************************************************
// <copyright file="ErrUtils.cs" company="DomConsult Sp. z o.o.">
//     Copyright ©  2021 All rights reserved
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DomConsult.GlobalShared.Utilities
{
    /// <summary>
    /// Class MessageException.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable()]
    public class MessageException : Exception
    {
        /// <summary>
        /// Gets or sets the mtscom identifier.
        /// </summary>
        /// <value>The mtscom identifier.</value>
        public int MTSCOMId { get; set; }
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrCode { get; set; }
        /// <summary>
        /// Gets or sets the text identifier.
        /// </summary>
        /// <value>The text identifier.</value>
        public int TextId { get; set; }
        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public object[] Params { get; set; }
        /// <summary>
        /// Gets or sets the error MSG stack.
        /// </summary>
        /// <value>The error MSG stack.</value>
        public object ErrMsgStack { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageException"/> class.
        /// </summary>
        public MessageException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MessageException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageException"/> class.
        /// </summary>
        /// <param name="mtsComId">The MTS COM identifier.</param>
        /// <param name="textId">The text identifier.</param>
        /// <param name="param">The parameter.</param>
        public MessageException(int mtsComId, int textId, object[] param)
        {
            MTSCOMId = mtsComId;
            TextId = textId;
            Params = param;
            ErrCode = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageException"/> class.
        /// </summary>
        /// <param name="errMsgStack">The error MSG stack.</param>
        public MessageException(object errMsgStack)
        {
            ErrMsgStack = errMsgStack;
            ErrCode = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public MessageException(string message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information.</param>
        /// <param name="streamingContext">The streaming context.</param>
        /// <exception cref="NotImplementedException"></exception>
        protected MessageException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Class Err.
    /// </summary>
    public class Err
    {
        /// <summary>
        /// Gets or sets the MTS COM identifier.
        /// </summary>
        /// <value>The MTS COM identifier.</value>
        private int MTSComId { get; set; }
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrCode { get; set; }
        /// <summary>
        /// Gets or sets the state of the form.
        /// </summary>
        /// <value>The state of the form.</value>
        public TFormState FormState { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [send others].
        /// </summary>
        /// <value><c>true</c> if [send others]; otherwise, <c>false</c>.</value>
        public bool SendOthers { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [send fields].
        /// </summary>
        /// <value><c>true</c> if [send fields]; otherwise, <c>false</c>.</value>
        public bool SendFields { get; set; }

        /// <summary>
        /// The error MSG stack
        /// </summary>
        private object _errMsgStack;

        /// <summary>
        /// Gets or sets the error MSG stack.
        /// </summary>
        /// <value>The error MSG stack.</value>
        public object ErrMsgStack
        {
            get { return _errMsgStack; }
            set { _errMsgStack = value; }
        }

        /// <summary>
        /// Gets the error MSG count.
        /// </summary>
        /// <value>The error MSG count.</value>
        public object ErrMsgCount
        {
            get
            {
                try
                {
                    return TWMK.GetMessagesCount(ErrMsgStack);
                }
                catch
                {

                    return -1;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Err"/> class.
        /// </summary>
        /// <param name="mtsComId">The MTS COM identifier.</param>
        public Err(int mtsComId)
        {
            ErrCode = 0;
            MTSComId = mtsComId;

            SendOthers = false;
            SendFields = false;

            FormState = TFormState.cfsNone;
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="reRaise">if set to <c>true</c> [re raise].</param>
        public void HandleMessage(MessageException e, bool reRaise = false)
        {
            if (!ComUtils.Assigned(e.ErrMsgStack))
            {
                int tempMTSCOMId = MTSComId;
                MTSComId = e.MTSCOMId;

                if (!ComUtils.Assigned(e.Params))
                {
                    AddMsgStd(e.TextId);
                }
                else
                {
                    AddMsgStd(e.TextId, e.Params, false);
                }
                MTSComId = tempMTSCOMId;
            }
            else if (e.ErrMsgStack.GetType().IsArray)
            {
                ErrMsgStack = e.ErrMsgStack;
            }
            else
            {
                AddMsgInfoCompose(ErrMsgStack.ToString());
            }

            if (reRaise)
            {
                MessageRaiseOnMsgStack();
            }
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="reRaise">if set to <c>true</c> [re raise].</param>
        /// <exception cref="Exception"></exception>
        public void HandleException(Exception ex, string methodName, bool reRaise = true)
        {
            AddMsgFatal(ex, methodName);

            if (reRaise)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="comObj">The COM object.</param>
        /// <exception cref="Exception"></exception>
        public void HandleException(Exception ex, ComWrapper comObj)
        {
            if (ComUtils.Assigned(comObj))
            {
                comObj.GetLastErrorDescription(out _errMsgStack);
            }
            throw new Exception(ex.Message);
        }

        /// <summary>
        /// Messages the raise on MSG stack.
        /// </summary>
        /// <exception cref="DomConsult.GlobalShared.Utilities.MessageException"></exception>
        private void MessageRaiseOnMsgStack()
        {
            throw new MessageException();
        }

        /// <summary>
        /// Messages the raise.
        /// </summary>
        /// <param name="textId">The text identifier.</param>
        /// <param name="Params">The optional parameters.</param>
        /// <exception cref="DomConsult.GlobalShared.Utilities.MessageException"></exception>
        public void MessageRaise(int textId, object[] Params = null)
        {
            throw new MessageException(MTSComId, textId, Params);
        }

        /// <summary>
        /// Messages the raise.
        /// </summary>
        /// <param name="mtsComId">The COM dictionary identifier.</param>
        /// <param name="textId">The text identifier.</param>
        /// <param name="Params">The optional parameters.</param>
        /// <exception cref="DomConsult.GlobalShared.Utilities.MessageException">null</exception>
        public void MessageRaise(int mtsComId, int textId, object[] Params = null)
        {
            throw new MessageException(mtsComId, textId, Params);
        }

        /// <summary>
        /// Messages the COM raise.
        /// </summary>
        /// <param name="comObj">The COM object.</param>
        /// <exception cref="DomConsult.GlobalShared.Utilities.MessageException"></exception>
        public void MessageComRaise(ComWrapper comObj)
        {
            if (ComUtils.Assigned(comObj))
            {
                comObj.GetLastErrorDescription(out _errMsgStack);
            }
            throw new MessageException(ErrMsgStack);
        }

        /// <summary>
        /// Adds the MSG standard.
        /// </summary>
        /// <param name="textId">The text identifier.</param>
        /// <param name="clearMsgStack">if set to <c>true</c> [clear MSG stack].</param>
        /// <param name="inMtsComId">The in MTS COM identifier.</param>
        /// <returns>System.Int32.</returns>
        public int AddMsgStd(int textId, bool clearMsgStack = false, int inMtsComId = -1)
        {
            try
            {
                if (inMtsComId > 0)
                {
                    TWMK.AddMessage(inMtsComId, textId, null, ref _errMsgStack, clearMsgStack);
                }
                else
                {
                    TWMK.AddMessage(MTSComId, textId, null, ref _errMsgStack, clearMsgStack);
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Adds the MSG standard.
        /// </summary>
        /// <param name="textId">The text identifier.</param>
        /// <param name="Params">The parameters.</param>
        /// <param name="clearMsgStack">if set to <c>true</c> [clear MSG stack].</param>
        /// <param name="inMtsComId">The in MTS COM identifier.</param>
        /// <returns>System.Int32.</returns>
        public int AddMsgStd(int textId, object[] Params, bool clearMsgStack = false, int inMtsComId = -1)
        {
            try
            {
                if (inMtsComId > 0)
                {
                    TWMK.AddMessage(inMtsComId, textId, Params, ref _errMsgStack, clearMsgStack);
                }
                else
                {
                    TWMK.AddMessage(MTSComId, textId, Params, ref _errMsgStack, clearMsgStack);
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Adds the MSG information compose.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <returns>System.Int32.</returns>
        public int AddMsgInfoCompose(string Text)
        {
            try
            {
                TWMK.AddMessage(50009, 16, new object[] { Text }, ref _errMsgStack, false);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Adds the MSG compose.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <returns>System.Int32.</returns>
        public int AddMsgCompose(string Text)
        {
            try
            {
                TWMK.AddMessage(50009, 6, new object[] { Text }, ref _errMsgStack, false);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Adds the MSG fatal.
        /// </summary>
        /// <param name="MethodName">Name of the method.</param>
        /// <returns>System.Int32.</returns>
        public int AddMsgFatal(string MethodName)
        {
            try
            {
                TWMK.AddMessage(50009, 1, new object[] { MethodName }, ref _errMsgStack, false);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Adds the MSG fatal.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>System.Int32.</returns>
        public int AddMsgFatal(Exception ex, string methodName)
        {
            int result = 0;
            try
            {
                if (ex.Message.Length > 0)
                {
                    result = AddMsgFatalSys(ex.Message);
                }
                else
                {
                    result = AddMsgFatal(methodName);
                }

                return result;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Adds the MSG fatal system.
        /// </summary>
        /// <param name="exceptionMsg">The exception MSG.</param>
        /// <returns>System.Int32.</returns>
        public int AddMsgFatalSys(string exceptionMsg)
        {
            int result = 0;
            try
            {
                if (TWMK.GetMessagesCount(ErrMsgStack) == 0)
                {
                    result = AddMsgCompose(exceptionMsg);
                }

                return result;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Checks the specified resource.
        /// </summary>
        /// <param name="res">The resource.</param>
        /// <exception cref="Exception"></exception>
        public void Check(int res)
        {
            if (res < 0)
                throw new Exception();
        }

        /// <summary>
        /// Checks the specified resource.
        /// </summary>
        /// <param name="res">The resource.</param>
        /// <param name="comObj">The COM object.</param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="Exception">Error RC = { res }</exception>
        /// <exception cref="Exception">COM not exists, message could not be retrived. RC = { res }</exception>
        public void Check(int res, ComWrapper comObj)
        {
            if (ComUtils.Assigned(comObj))
            {
                if (res < 0)
                {
                    comObj.GetLastErrorDescription(out _errMsgStack);

                    throw new Exception();
                }
                else if ((res == (int)TCSWMK.csWMK) || (res == (int)TCSWMK.csWMK_Error))
                {
                    MessageComRaise(comObj);
                }
            }
            else if (res < 0)
            {
                throw new Exception($"Error RC = { res }");
            }
            else if ((res == (int)TCSWMK.csWMK) || (res == (int)TCSWMK.csWMK_Error))
            {
                throw new Exception($"COM not exists, message could not be retrived. RC = { res }");
            }
        }
    }
}
