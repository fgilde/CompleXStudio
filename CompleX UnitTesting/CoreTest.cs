using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using CompleX.Executers;
using CompleX.Scripting;
using CompleX.ServiceModel;
using Complex_Designers;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompleX_UnitTesting
{
    /// <summary>
    /// Summary description for CoreTest
    /// </summary>
    [TestClass]
    public class CoreTest
    {
        
        public CoreTest()
        {
            Assert.IsNotNull(ApplicationHost.Host);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestHostService()
        {
            HostedService service1 = new HostedService();
            HostedService service2 = new HostedService();
            
            ApplicationHost.Host.AddService(service1);
            ApplicationHost.Host.AddService(service2);

            HTMLDesigner designer = new HTMLDesigner();

            ApplicationHost.Host.AddService(designer);

            IEnumerable<IHostedService> hostedServiceCount = ApplicationHost.Host.GetServices<IHostedService>();
            IEnumerable<IDesignable> designers = ApplicationHost.Host.GetServices<IDesignable>();
            IEnumerable<HostedService> hosted = ApplicationHost.Host.GetServices<HostedService>();

            Assert.AreEqual(1,designers.Count()); // 1 IDesignable
            Assert.AreEqual(2, hosted.Count());   // 2 HostedService 
            Assert.AreEqual(3, hostedServiceCount.Count()); // 3 IHosted (IDesignable implements IHostedService)

            ApplicationHost.Host.RemoveServices<HostedService>();
            Assert.AreEqual(1, ApplicationHost.Host.GetServices<IHostedService>().Count());

            ApplicationHost.Host.RemoveService(designer);

            Assert.AreEqual(0, ApplicationHost.Host.GetServices<IDesignable>().Count());
            Assert.AreEqual(0, ApplicationHost.Host.GetServices<IHostedService>().Count());

        }

        /// <summary>
        /// Prüft ob die mitgelieferten PlugIns korrekt geladen werden können
        /// </summary>
        [TestMethod]
        public void TestLoadIncludedPlugIns()
        {
            ApplicationHost.LoadAllPlugins();

            Assert.AreEqual(1, ApplicationHost.Host.GetServices<HTMLDesigner>().Count(),"on HTMLDesigner");
            Assert.AreEqual(true, ApplicationHost.Host.GetServices<IDesignable>().Count() >= 1, "on IDesignable");
            Assert.AreEqual(1, ApplicationHost.Host.GetServices<HtmlExecuter>().Count(), "on HtmlExecuter");
            Assert.AreEqual(true, ApplicationHost.Host.GetServices<IExecutable>().Count() >= 1, "on IExecutable");

        }

        [TestMethod]
        public void TestCompleXScripting()
        {
            const string testScript = "this.Context.UserName = \"NewUser\";";
            const ScriptLanguage language = ScriptLanguage.CSharp;
            var context = new CompleXScriptContext
            {
                UserName = "TestName",
            };
           
            var engine = new ScriptEngine(language, testScript, context);
            engine.Execute();
        }

    }
}
