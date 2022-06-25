using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomConsult.GlobalShared.Utilities
{

    /// <summary>
    /// Class used to store secret settings stored in devel_secret.json file localized in ApplicationData folder
    /// </summary>
    public class DevelSecret
    {
        public Dictionary<string, string> AC { get; set; } = new Dictionary<string, string>();
    }

    public sealed class AssertWMK
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public static void IsOK(int result, string message)
        {
            Assert.IsTrue((result >= 0) && 
                          (result != (int)TCSWMK.csWMK_Error) &&
                          (result != (int)TCSWMK.csWMK_JobError), 
                          message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public static void IsOK(int result, string message, params object[] parameters)
        {
            Assert.IsTrue((result >= 0) &&
                          (result != (int)TCSWMK.csWMK_Error) &&
                          (result != (int)TCSWMK.csWMK_JobError),
                          message, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public static void IsERR(int result, string message)
        {
            Assert.IsTrue((result < 0) &&
                          (result == (int)TCSWMK.csWMK_Error) &&
                          (result == (int)TCSWMK.csWMK_JobError),
                          message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public static void IsERR(int result, string message, params object[] parameters)
        {
            Assert.IsTrue((result < 0) &&
                          (result == (int)TCSWMK.csWMK_Error) &&
                          (result == (int)TCSWMK.csWMK_JobError),
                          message, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public static void IsWMK(int result, string message)
        {
            Assert.IsTrue(Enum.IsDefined(typeof(TCSWMK), result), message);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public static void IsWMK(int result, string message, params object[] parameters)
        {
            Assert.IsTrue(Enum.IsDefined(typeof(TCSWMK), result), message, parameters);
        }
    }
}
