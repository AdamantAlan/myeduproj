using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestContextSamples
{
    [TestClass]
    public class MyTestSample
    {
        // TestContext используется для хранения информации о текущем юнит тесте
        // При тестировании веб сервисов хранит URL
        // При тестировании ASP.NET приложений - предоставляет доступ к ASP странице
        // При использовании Data Driven тестов предоставляет доступ к источнику данных
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            TestContext.WriteLine("TestRunDirectory {0}", TestContext.TestRunDirectory);
            TestContext.WriteLine("TestName {0}", TestContext.TestName);
            TestContext.WriteLine("CurrentTestOutcome {0}", TestContext.CurrentTestOutcome);
        }

        [TestCleanup]
        public void CleanUp()
        {
            TestContext.WriteLine("TestName (CleanUp) {0}", TestContext.TestName);
            TestContext.WriteLine("CurrentTestOutcome (Cleanup) {0}", TestContext.CurrentTestOutcome);
        }
    }
}
