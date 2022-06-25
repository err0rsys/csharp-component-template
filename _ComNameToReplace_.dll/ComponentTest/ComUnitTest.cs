using DomConsult.Components;
using DomConsult.GlobalShared.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.EnterpriseServices;
using System.IO;

namespace DomConsult.ComponentTest
{
    [TestClass]
    public class ComUnitTest
    {
        private DevelSecret Secret { get; set; }
        private string UseAC { get; set; }  = "AC_GRANIT_MASTER_TEST";
        private bool TestByComObject { get; set; } = false;
        private bool TestByDynObject { get; set; } = false;
        private bool TestByComClass { get; set; } = true;

        public ComUnitTest()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string secretPath = Path.Combine(appDataFolder, "devel_secret.json");

            var jsonsecreet =  File.ReadAllText(secretPath);
            Secret = JsonConvert.DeserializeObject<DevelSecret>(jsonsecreet);
            Assert.IsTrue(Secret != null);
            Assert.IsTrue(Secret.AC.ContainsKey(UseAC));
            Debug.WriteLine(Secret.AC[UseAC]);

#if !TEST

#else

#endif
        }

        /// <summary>
        /// Test registered COM object with late binding using ComWrapper
        /// </summary>
        [TestMethod("ComObject_RunMethod_XXXXX - przyk³ad")]
        public void ComObject_RunMethod_XXXXX()
        {
            if (!TestByComObject)
                Assert.Inconclusive("Skip! ComObject tests are switched Off.");

            ComWrapper comObject = ComUtils.CreateComInStandardCom("_ComNameToReplace_.Manager", Secret.AC[UseAC]);

            var rm_params = new object[] { "/PAR_XXXX=1", null, null };
            int res = ComUtils.RunMethod(comObject, "RM_XXXXX", ref rm_params);
            //rm_params = _params as object[];

            AssertWMK.IsOK(res, "Spodziewany wynik: result > 0");
        }

        /// <summary>
        /// Test registered COM object using late binding without ComWrapper
        /// </summary>
        [TestMethod("ComDynObj - przyk³ad")]
        public void ComDynObject_RunMethod_UnknownMethod()
        {
            if (!TestByDynObject)
                Assert.Inconclusive("Skip! ComDynObject tests are switched Off.");

            Type comObjectType = Type.GetTypeFromProgID("_ComNameToReplace_.Manager", true);
            dynamic comDynObject = Activator.CreateInstance(comObjectType);
            int res = comDynObject.AssignAccessCode(Secret.AC[UseAC]);
            Assert.IsTrue(res >= 0, "B³¹d przypisywania AC do komponentu _ComNameToReplace_.Manager");

            var rm_params = new object[] { "/XXX=1", null, null };
            object _params = rm_params;
            res = comDynObject.RunMethod("NO_METHOD", ref _params);
            //rm_params = _params as object[];

            AssertWMK.IsOK(res, "Nieznana metoda powinna zwróciæ b³¹d");
        }

        /// <summary>
        /// Test unregistered COM class
        /// </summary>
        [TestMethod("ComClass - przyk³ad")]
        public void ComClass_RunMethod_XXXXX()
        {
            if (!TestByComClass)
                Assert.Inconclusive("Skip! ComClass tests are switched Off.");

            if (typeof(Manager).IsAssignableFrom(typeof(ServicedComponent)))
                Assert.Inconclusive("Skip! Can't run test over ServicedComponent.");

            var comClass = new Manager();
            int res = comClass.AssignAccessCode(Secret.AC[UseAC]);
            Assert.IsTrue(res >= 0, "B³¹d przypisywania AC do komponentu");

            var rm_params = new object[] { "/PAR_XXXXX=1", null, null };
            object _params = rm_params;
            res = comClass.RunMethod("RM_XXXXX", ref _params);
            //rm_params = _params as object[];

            AssertWMK.IsOK(res, "Spodziewany wynik: result > 0");
        }
    }
}