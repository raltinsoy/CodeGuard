using CodeGuard.Fody;
using Fody;
using System;
using System.Reflection;
using Xunit;

namespace Tests
{
    public class AttributesTests
    {
        private static readonly Fody.TestResult _testResult;

        static AttributesTests()
        {
            var weavingTask = new ModuleWeaver();
            _testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll", runPeVerify: false);
        }

        [Fact]
        public void ShouldThrowException()
        {
            //var type = _testResult.Assembly.GetType("TheNamespace.Hello");
            Assert.True(true);
            //TODO: fill
        }

        [Fact]
        public void ShouldAccessPublicFunction()
        {
            var type = _testResult.Assembly.GetType("TheNamespace.Hello");
            //TODO: fill
        }

        [Fact]
        public void ShouldNotAccessPrivateFunction()
        {
            var type = _testResult.Assembly.GetType("TheNamespace.Hello");
            //TODO: fill
        }
    }
}
