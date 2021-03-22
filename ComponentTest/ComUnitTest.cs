using DomConsult.Components;
using DomConsult.GlobalShared.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.IO;

namespace DomConsult.ComponentTest
{
    /// <summary>
    /// Class used to store secret settings stored in devel_secret.json file localized in ApplicationData folder
    /// </summary>
    public class DevelSecret
    {
        public Dictionary<string, string> ACC { get; set; } = new Dictionary<string, string>();
    }

    [TestClass]
    public class ComUnitTest
    {
        private DevelSecret Secret { get; set; }
        private string UseACC { get; set; }  = "ACC_1";

        private bool TestByComObject { get; set; } = true;
        private bool TestByDynObject { get; set; } = false;
        private bool TestByComClass { get; set; } = false;

        public ComUnitTest()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string secretPath = Path.Combine(appDataFolder, "devel_secret.json");

            var jsonsecreet =  File.ReadAllText(secretPath);
            Secret = JsonConvert.DeserializeObject<DevelSecret>(jsonsecreet);
            Assert.IsTrue(Secret != null);
            Assert.IsTrue(Secret.ACC.ContainsKey(UseACC));
            Debug.WriteLine(Secret.ACC[UseACC]);

#if !TEST

#else

#endif
        }

        /// <summary>
        /// Test registered COM object with late binding using ComWrapper
        /// </summary>
        [TestMethod("ComObj - Test uruchomienia nieznanej metody przez RunMethod")]
        public void ComObject_RunMethod_UnknownMethod()
        {
            if (!TestByComObject)
                Assert.Inconclusive("Skip! ComObject tests are switched Off.");

            ComWrapper comObject = ComUtils.CreateComInStandardCom("Component.Manager", Secret.ACC[UseACC]);

            var rm_params = new object[] { "/XXX=1", null, null };
            int res = ComUtils.RunMethod(comObject, "NO_METHOD", ref rm_params);
            //rm_params = _params as object[];

            Assert.IsTrue(res < 0, "Nieznana metoda powinna zwrócić błąd");
        }

        /// <summary>
        /// Test registered COM object using late binding without ComWrapper
        /// </summary>
        [TestMethod("ComDynObj - Test uruchomienia nieznanej metody przez RunMethod")]
        public void ComDynObject_RunMethod_UnknownMethod()
        {
            if (!TestByDynObject)
                Assert.Inconclusive("Skip! ComDynObject tests are switched Off.");

            Type comObjectType = Type.GetTypeFromProgID("Component.Manager", true);
            dynamic comDynObject = Activator.CreateInstance(comObjectType);
            int res = comDynObject.AssignAccessCode(Secret.ACC[UseACC]);
            Assert.IsTrue(res >= 0, "Błąd przypisywania ACC do komponentu");

            var rm_params = new object[] { "/XXX=1", null, null };
            object _params = rm_params;
            res = comDynObject.RunMethod("NO_METHOD", ref _params);
            //rm_params = _params as object[];

            Assert.IsTrue(res < 0, "Nieznana metoda powinna zwrócić błąd");
        }

        /// <summary>
        /// Test unregistered COM class
        /// </summary>
        [TestMethod("ComClass - Test uruchomienia nieznanej metody przez RunMethod")]
        public void ComClass_RunMethod_UnknownMethod()
        {
            if (!TestByComClass)
                Assert.Inconclusive("Skip! ComClass tests are switched Off.");

            if (typeof(Manager).IsAssignableFrom(typeof(ServicedComponent)))
                Assert.Inconclusive("Skip! Can't run test over ServicedComponent.");

            var comClass = new Manager();
            int res = comClass.AssignAccessCode(Secret.ACC[UseACC]);
            Assert.IsTrue(res >= 0, "Błąd przypisywania ACC do komponentu");

            var rm_params = new object[] { "/XXX=1", null, null };
            object _params = rm_params;
            res = comClass.RunMethod("NO_METHOD", ref _params);
            //rm_params = _params as object[];

            Assert.IsTrue(res < 0, "Nieznana metoda powinna zwrócić błąd");
        }
    }
}
