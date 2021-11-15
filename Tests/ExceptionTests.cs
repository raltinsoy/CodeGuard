using CodeGuard.Fody;
using Fody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ExceptionTests
    {
        private static readonly Fody.TestResult _testResult;

        static ExceptionTests()
        {
            var weavingTask = new ModuleWeaver();
            _testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll", runPeVerify: false);
        }

        [Fact]
        public void ShouldPublicMethodThrowException_GetName()
        {
            var instance = _testResult.GetInstance("AssemblyToProcess.Person");
            Assert.Throws<SDKExportException>(() => instance.Name);
        }

        [Fact]
        public void ShouldPublicMethodThrowException_GetKey()
        {
            var instance = _testResult.GetInstance("AssemblyToProcess.Person");
            Assert.Throws<SDKExportException>(() => instance.GetKey());
        }
    }
}
