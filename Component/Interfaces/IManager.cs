using System;
using System.Runtime.InteropServices;

namespace Component
{
    [ComVisible(true), Guid("CAF8BD41-EA4C-4C49-9782-BAC86CA5B5F9")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IManager
    {
        int AssignAccessCode(object accessCode);
        int AssignStartUpParameter(object param, ref object others);
        int GetValue(object fieldName, ref object fieldValue);
        int SetGetValues(ref object formState, ref object fields, 
                         ref object messages, ref object others);
        int SupportSQL(object methodName, object param, ref object sqlArray);
        int RunMethod(object methodName, ref object param);
        int AssignWMKResult(object wmkResult);
        int GetLastErrorDescription(ref object error);
    }
}
