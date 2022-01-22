using System;
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
        public int MTSCOMId { get; set; } = -1;
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
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageException"/> class.
        /// </summary>
        /// <param name="errMsgStack">The error MSG stack.</param>
        public MessageException(object errMsgStack)
        {
            ErrMsgStack = errMsgStack;
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
        public int MTSComId { get; set; }
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
        /// Clears whole message stack.
        /// </summary>
        public void ClearMessageStack()
        {
            _errMsgStack = null;
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
                AddMsgStd(e.MTSCOMId, e.TextId, e.Params);
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
        public void MessageRaiseOnMsgStack(int errCode = 0)
        {
            if (errCode != 0)
                ErrCode = errCode;

            throw new MessageException(_errMsgStack);
        }

        /// <summary>
        /// Messages the raise.
        /// </summary>
        /// <param name="textId">The text identifier.</param>
        /// <param name="Params">The optional parameters.</param>
        /// <exception cref="DomConsult.GlobalShared.Utilities.MessageException"></exception>
        public void MessageRaise(int textId, params object[] Params)
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
        public void MessageRaise(int mtsComId, int textId, params object[] Params)
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
        /// <param name="Params">Optional message parameters.</param>
        /// <returns>System.Int32.</returns>
        public int AddMsgStd(int textId, params object[] Params)
        {
            try
            {
                TWMK.AddMessage(ref _errMsgStack, MTSComId, textId, Params);
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
        /// <param name="mtsComId">The in MTS COM identifier.</param>
        /// <param name="textId">The text identifier.</param>
        /// <param name="Params">Optiona; message parameters.</param>
        /// <returns>System.Int32.</returns>
        public int AddMsgStd(int mtsComId, int textId, params object[] Params)
        {
            try
            {
                if (mtsComId > 0)
                {
                    TWMK.AddMessage(ref _errMsgStack, mtsComId, textId, Params);
                }
                else
                {
                    TWMK.AddMessage(ref _errMsgStack, MTSComId, textId, Params);
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
                TWMK.AddMessage(ref _errMsgStack, 50009, 16, Text);
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
                TWMK.AddMessage(ref _errMsgStack, 50009, 6, Text);
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
                TWMK.AddMessage(ref _errMsgStack, 50009, 1, MethodName);
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
            int result;
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
        public static void Check(int res)
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
