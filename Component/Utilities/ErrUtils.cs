using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DomConsult.GlobalShared.Utilities
{
    [Serializable()]
    public class MessageException : Exception
    {
        public int MTSCOMId { get; set; }
        public int ErrCode { get; set; }
        public int TextId { get; set; }
        public object[] Params { get; set; }
        public object ErrMsgStack { get; set; }

        public MessageException()
        { }

        public MessageException(string message)
            : base(message)
        { }

        public MessageException(int mtsComId, int textId, object[] param)
        {
            MTSCOMId = mtsComId;
            TextId = textId;
            Params = param;
            ErrCode = 0;
        }

        public MessageException(object errMsgStack)
        {
            ErrMsgStack = errMsgStack;
            ErrCode = 0;
        }

        public MessageException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected MessageException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }

    public class Err
    {
        private int MTSComId { get; set; }
        private int ErrCode { get; set; }
        private TFormState FormState { get; set; }
        public bool SendOthers { get; set; }
        public bool SendFields { get; set; }

        private object _errMsgStack;

        public object ErrMsgStack
        {
            get { return _errMsgStack; }
            set { _errMsgStack = value; }
        }

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

        public Err(int mtsComId)
        {
            ErrCode = 0;
            MTSComId = mtsComId;

            SendOthers = false;
            SendFields = false;

            FormState = TFormState.cfsNone;
        }

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

        public void HandleException(Exception ex, string methodName, bool reRaise = true)
        {
            AddMsgFatal(ex, methodName);

            if (reRaise)
            {
                throw new Exception(ex.Message);
            }
        }

        public void HandleException(Exception ex, ComWrapper comObj)
        {
            if (ComUtils.Assigned(comObj))
            {
                comObj.GetLastErrorDescription(out _errMsgStack);
            }
            throw new Exception(ex.Message);
        }

        private void MessageRaiseOnMsgStack()
        {
            throw new MessageException();
        }

        public void MessageRaise(int textId)
        {
            throw new MessageException(MTSComId, textId, null);
        }

        public void MessageRaise(int textId, object[] Params)
        {
            throw new MessageException(MTSComId, textId, Params);
        }

        public void MessageComRaise(ComWrapper comObj)
        {
            if (ComUtils.Assigned(comObj))
            {
                comObj.GetLastErrorDescription(out _errMsgStack);
            }
            throw new MessageException(ErrMsgStack);
        }

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

        public void Check(int res)
        {
            if (res < 0)
                throw new Exception();
        }

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
