using CodeGuard.Fody;
using Fody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class PropertyTests
    {
        private static readonly dynamic _instance;
        private static readonly Type _instanceType;

        static PropertyTests()
        {
            var weavingTask = new ModuleWeaver();
            var testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll", runPeVerify: false);
            _instance = testResult.GetInstance("AssemblyToProcess.PropertyTestObject");
            _instanceType = _instance.GetType();
        }

        [Fact]
        public void ShouldPublicExist()
        {
            var properties = _instanceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Assert.Contains(properties, x => x.Name == "PublicProperty");

            Assert.Throws<SDKExportException>(() => _instance.PublicProperty);
        }

        [Fact]
        public void ShouldProtectedExist()
        {
            var properties = _instanceType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.Contains(properties, x => x.Name == "ProtectedProperty");
        }

        [Fact]
        public void ShouldInternalNotExist()
        {
            var properties = _instanceType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.DoesNotContain(properties, x => x.Name == "InternalProperty");
            Assert.Contains(properties, x => x.Name == "InternalProperty_Visible");
        }

        [Fact]
        public void ShouldPrivateNotExist()
        {
            var properties = _instanceType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.DoesNotContain(properties, x => x.Name == "PrivateProperty");
            Assert.Contains(properties, x => x.Name == "PrivateProperty_Visible");
        }
    }
}
