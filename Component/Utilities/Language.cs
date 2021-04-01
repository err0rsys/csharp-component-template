using System;
using System.Runtime.InteropServices;
using DomConsult.Platform.Extensions;

namespace DomConsult.GlobalShared.Utilities
{
    /// <summary>
    /// Class Language.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// The MTS COM identifier
        /// </summary>
        private readonly int MTSComId;
        /// <summary>
        /// The language identifier
        /// </summary>
        private readonly int LangId;
        /// <summary>
        /// The language reference
        /// </summary>
        private readonly int LangRef;
        /// <summary>
        /// The language manager
        /// </summary>
        private readonly dynamic LangManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="mtsComId">The MTS COM identifier.</param>
        /// <param name="accessCode">The access code.</param>
        public Language(int mtsComId, object accessCode)
        {
            MTSComId = mtsComId;
            Type LangComType = Type.GetTypeFromProgID("Language.Manager", true);
            LangManager = Activator.CreateInstance(LangComType);
            BDWrapper Par = new BDWrapper();
            Par.LoadParams(accessCode.ToString());
            LangId = Par.Params["L"].AsInt();
            LangRef = LangManager.SetCom(mtsComId);
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy() /* jawnie zwalniamy DBComa nie czekając na odśmiecanie pamięci */
        {
            while (Marshal.ReleaseComObject(LangManager) > 0) { }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="textId">The text identifier.</param>
        /// <returns>System.String.</returns>
        public string GetText(int textId)
        {
            return LangManager.GetText(MTSComId, LangId, textId);
        }
    }
}
