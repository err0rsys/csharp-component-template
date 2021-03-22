// ***********************************************************************
// Assembly         : Component
// Author           : Artur Maciejowski
// Created          : 16-02-2020
//
// Last Modified By : Artur Maciejowski
// Last Modified On : 16-02-2020
// ***********************************************************************
// <copyright file="Language.cs" company="DomConsult Sp. z o.o.">
//     Copyright ©  2021 All rights reserved
// </copyright>
// <summary></summary>
// ***********************************************************************

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
        private readonly int MTSComId;
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
            MTSComId = mtsComId;

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
            /* jawnie zwalniamy Coma nie czekając na odśmiecanie pamięci */
            //while (Marshal.ReleaseComObject(LangManager) > 0) { }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="textId">The text identifier.</param>
        /// <returns>System.String.</returns>
        public string GetText(int textId, int mtsComId = -1, int langId = -1)
        {
            return TuniGlobalCache.GetMLText(mtsComId > 0 ? mtsComId : MTSComId, langId > 0 ? langId : LangId, textId);
            //return LangManager.GetText(MTSComId, LangId, textId);
        }
    }
}
