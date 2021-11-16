using CodeGuard.Fody;
using Fody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

//only one definition is enough for this namespace
[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Tests
{
    public class FieldTests
    {
        private static readonly dynamic _instance;
        private static readonly Type _instanceType;

        static FieldTests()
        {
            var weavingTask = new ModuleWeaver();
            var testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll", runPeVerify: false);
            _instance = testResult.GetInstance("AssemblyToProcess.FieldTestObject");
            _instanceType = _instance.GetType();
        }

        [Fact]
        public void ShouldPublicExist()
        {
            var fields = _instanceType.GetFields(BindingFlags.Instance | BindingFlags.Public);
            Assert.Contains(fields, x => x.Name == "PublicField");
        }

        [Fact]
        public void ShouldProtectedExist()
        {
            var fields = _instanceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.Contains(fields, x => x.Name == "ProtectedField");
        }

        [Fact]
        public void ShouldInternalNotExist()
        {
            var fields = _instanceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.DoesNotContain(fields, x => x.Name == "InternalField");
            Assert.Contains(fields, x => x.Name == "InternalField_Visible");
        }

        [Fact]
        public void ShouldPrivateNotExist()
        {
            var fields = _instanceType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.DoesNotContain(fields, x => x.Name == "PrivateField");
            Assert.Contains(fields, x => x.Name == "PrivateField_Visible");
        }
    }
}
