using System;
using System.Runtime.InteropServices;
using DomConsult.Platform.Extensions;

namespace DomConsult.GlobalShared.Utilities
{
    /// <summary>
    /// Class Language.
    /// </summary>
    public class Language: IDisposable
    {
        /// <summary>
        /// The MTS COM identifier
        /// </summary>
        private readonly int ComId;
        /// <summary>
        /// The language identifier
        /// </summary>
        private readonly int LangId;
        
        /*
        /// <summary>
        /// The language manager
        /// </summary>
        private readonly dynamic LangManager;
        */

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="mtsComId">The MTS COM identifier.</param>
        /// <param name="accessCode">The access code.</param>
        public Language(int mtsComId, object accessCode)
        {
            ComId = mtsComId;

            //Type LangComType = Type.GetTypeFromProgID("Language.Manager", true);
            //LangManager = Activator.CreateInstance(LangComType);

            BDWrapper Par = new BDWrapper();
            Par.LoadParams(accessCode.ToString());
            LangId = Par.Params["L"].AsInt();

            //LangManager.SetCom(mtsComId);
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Dispose()
        {
            /* jawnie zwalniamy Coma nie czekaj¹c na odœmiecanie pamiêci */
            //while (Marshal.ReleaseComObject(LangManager) > 0) { }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="textId">Id of the multilanguage text.</param>
        /// <param name="comId">Optional Id of the dictionary.</param>
        /// <param name="langId">Optional Id of the language.</param>
        /// <returns>System.String.</returns>
        public string GetText(int textId, int comId = -1, int langId = -1)
        {
            return TuniGlobalCache.GetMLText(comId > 0 ? comId : ComId, langId > 0 ? langId : LangId, textId);
            //return LangManager.GetText(MTSComId, LangId, textId);
        }
    }
}
