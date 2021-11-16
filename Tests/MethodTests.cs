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
    public class MethodTests
    {
        private static readonly dynamic _instance;
        private static readonly Type _instanceType;

        static MethodTests()
        {
            var weavingTask = new ModuleWeaver();
            var testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll", runPeVerify: false);
            _instance = testResult.GetInstance("AssemblyToProcess.MethodTestObject");
            _instanceType = _instance.GetType();
        }

        [Fact]
        public void ShouldPublicExist()
        {
            var methods = _instanceType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            Assert.Contains(methods, x => x.Name == "PublicFunc");

            try
            {
                _instance.PublicFunc();
                Assert.True(false);
            }
            catch (SDKExportException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void ShouldPublicNotThrowException()
        {
            var methods = _instanceType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            Assert.Contains(methods, x => x.Name == "PublicFunc_NotCleared");

            //must not throw exception
            _instance.PublicFunc_NotCleared();
        }

        [Fact]
        public void ShouldPublicWithParamsExist()
        {
            var methods = _instanceType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            Assert.Contains(methods, x => x.Name == "PublicFuncWithParams");

            try
            {
                _instance.PublicFuncWithParams(2, "A");
                Assert.True(false);
            }
            catch (SDKExportException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void ShouldProtectedInternalNotExist()
        {
            var methods = _instanceType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.DoesNotContain(methods, x => x.Name == "ProtectedAndInternalFunc");
            Assert.Contains(methods, x => x.Name == "ProtectedAndInternalFunc_Visible");
        }

        [Fact]
        public void ShouldProtectedVirtualExist()
        {
            var methods = _instanceType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.Contains(methods, x => x.Name == "ProtectedAndVirtualFunc");
        }

        [Fact]
        public void ShouldInternalNotExist()
        {
            var methods = _instanceType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.DoesNotContain(methods, x => x.Name == "InternalFunc");
            Assert.Contains(methods, x => x.Name == "InternalFunc_Visible");
        }

        [Fact]
        public void ShouldPrivateNotExist()
        {
            var methods = _instanceType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.DoesNotContain(methods, x => x.Name == "PrivateFunc");
            Assert.Contains(methods, x => x.Name == "PrivateFunc_Visible");
        }
    }
}
