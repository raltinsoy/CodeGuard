using AssemblyToProcess;
using CodeGuard.Fody;
using Fody;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Tests
{
    public class VisibilityTests
    {
        private static readonly Fody.TestResult _testResult;

        static VisibilityTests()
        {
            var weavingTask = new ModuleWeaver();
            _testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll", runPeVerify: false);
        }

        [Fact]
        public void ShouldPrivatePropertyNotExist()
        {
            var instance = _testResult.Assembly.CreateInstance("AssemblyToProcess.Person");
            var type = instance.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
            
            Assert.Empty(properties.Where(x => x.Name == "PrivateProperty"));
            Assert.NotEmpty(properties.Where(x => x.Name == "PrivateProperty_Visible"));
        }
    }
}
