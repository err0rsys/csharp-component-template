using DomConsult.Components;
using DomConsult.GlobalShared.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DomConsult.ComponentTest
{
    public class DevelSectet
    {
        public Dictionary<string, string> ACC { get; set; } = new Dictionary<string, string>();
    }

    [TestClass]
    public class ComUnitTest
    {
        private readonly DevelSectet secret;
        private readonly string UseACC = "ACC_1";
        //private ComWrapper comObject = null;
        private readonly dynamic comObject;
        private readonly Manager comClass = null;

        public ComUnitTest()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string secretPath = Path.Combine(appDataFolder, "devel_secret.json");

            var jsonsecreet =  File.ReadAllText(secretPath);
            secret = JsonConvert.DeserializeObject<DevelSectet>(jsonsecreet);
            Assert.IsTrue(secret != null);
            Assert.IsTrue(secret.ACC.ContainsKey(UseACC));
            Debug.WriteLine(secret.ACC[UseACC]);

#if !TEST
            //comObject = new ComWrapper();
            //comObject.Connect("Component.Manager");
            //comObject.AssignAccessCode(ACC);

            Type comObjectType = Type.GetTypeFromProgID("Component.Manager", true);
            comObject = Activator.CreateInstance(comObjectType);
#else
            comClass = new Manager();
#endif
        }

        /// <summary>
        /// Test registered COM object
        /// </summary>
        [TestMethod("ComObj - Test uruchomienia nieznanej metody przez RunMethod")]
        public void ComObject_RunMethod_UnknownMethod()
        {
            if (comObject == null)
                return;

            //Wersja ComWrapper
            //var rm_params = new object[] { "/XXX=1", null, null };
            //int res = ComUtils.RunMethod(comObject, "NO_METHOD", ref rm_params);

            int res = comObject.AssignAccessCode(secret.ACC[UseACC]);
            Assert.IsTrue(res >= 0, "Błąd przypisywania ACC do komponentu");

            var rm_params = new object[] { "/XXX=1", null, null };
            object _params = rm_params;
            res = comObject.RunMethod("NO_METHOD", ref _params);

            Assert.IsTrue(res < 0, "Nieznana metoda powinna zwrócić błąd");
        }

        /// <summary>
        /// Test unregistered COM class
        /// </summary>
        [TestMethod("ComClass - Test uruchomienia nieznanej metody przez RunMethod")]
        public void ComClass_RunMethod_UnknownMethod()
        {
            if (comClass == null)
                return;

            int res = comClass.AssignAccessCode(secret.ACC[UseACC]);
            Assert.IsTrue(res >= 0, "Błąd przypisywania ACC do komponentu");

            var rm_params = new object[] { "/XXX=1", null, null };
            object _params = rm_params;
            res = comClass.RunMethod("NO_METHOD", ref _params);
            //rm_params = _params as object[];

            Assert.IsTrue(res < 0, "Nieznana metoda powinna zwrócić błąd");
        }
    }
}
