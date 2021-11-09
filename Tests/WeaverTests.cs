using Fody;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDKGenerator.Fody;
using System;

namespace Tests
{
    [TestClass]
    public class WeaverTests
    {
        private static readonly Fody.TestResult _testResult;

        static WeaverTests()
        {
            var weavingTask = new ModuleWeaver();
            _testResult = weavingTask.ExecuteTestRun("ActualCodeLibrary.dll");
        }

        [TestMethod]
        public void TestMethod1()
        {
            var type = _testResult.Assembly.GetType("TheNamespace.Hello");
            Assert.IsTrue(false);
        }
    }
}
