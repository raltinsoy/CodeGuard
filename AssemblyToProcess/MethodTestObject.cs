using CodeGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyToProcess
{
    public class MethodTestObject
    {
        [DoNotThrowException]
        public MethodTestObject()
        {
        }

        public void PublicFunc()
        {
        }
        
        [DoNotThrowException]
        public void PublicFunc_NotCleared()
        {
        }

        public void PublicFuncWithParams(int id, string name)
        {
        }

        protected string ProtectedFunc()
        {
            return "Person.ProtectedFunc.1";
        }

        protected internal string ProtectedAndInternalFunc()
        {
            return "Person.ProtectedAndInternalFunc.1";
        }

        [MakeVisible]
        protected internal string ProtectedAndInternalFunc_Visible()
        {
            return "Person.ProtectedAndInternalFunc.2";
        }

        protected virtual string ProtectedAndVirtualFunc()
        {
            return "Person.ProtectedAndVirtualFunc.1";
        }

        internal string InternalFunc()
        {
            return "Person.InternalFunc.1";
        }

        [MakeVisible]
        internal string InternalFunc_Visible()
        {
            return "Person.InternalFunc.2";
        }

        private string PrivateFunc()
        {
            return "Person.PrivateFunc.1";
        }

        [MakeVisible]
        private string PrivateFunc_Visible()
        {
            return "Person.PrivateFunc.2";
        }
    }
}
