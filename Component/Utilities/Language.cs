using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DomConsult.GlobalShared.Utilities
{
    public class Language
    {
        private int MTSComId;
        private int LangId;
        private int LangRef;
        private dynamic LangManager;
        public Language(int mtsComId, object accessCode)
        {
            MTSComId = mtsComId;
            Type LangComType = Type.GetTypeFromProgID("Language.Manager", true);
            LangManager = Activator.CreateInstance(LangComType);
            BDWrapper Par = new BDWrapper();
            Par.LoadParams(accessCode.ToString());
            LangId = (int)Par.Params("L");
            LangRef = LangManager.SetCom(mtsComId);
        }

        public void Destroy() /* jawnie zwalniamy DBComa nie czekając na odśmiecanie pamięci */
        {
            while (Marshal.ReleaseComObject(LangManager) > 0) { }
        }

        public string GetText(int textId)
        {
            return LangManager.GetText(MTSComId, LangId, textId);
        }
    }
}
