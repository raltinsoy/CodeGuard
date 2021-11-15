using CodeGuard;
using System;

namespace SmokeTest
{
    public class Person
    {
        public string Name { get; set; }

        private string PrivateProperty { get; set; }

        internal string InternalProperty { get; }

        protected string ProtectedProperty { get; }

        private readonly int _id;

        public Person(int id, string name)
        {
            _id = id;
            Name = name;
        }

        public string GetKey()
        {
            return Name + " #" + _id;
        }

        [MakeVisible]
        private string DummyPrivateFunc_Ignored()
        {
            return "Person.DummyPrivateFunc.2";
        }

        private string DummyPrivateFunc()
        {
            return "Person.DummyPrivateFunc.1";
        }

        [DoNotClearBody]
        protected string DummyProtectedFunc_NotCleared()
        {
            return "Person.DummyProtectedFunc.2";
        }

        protected string DummyProtectedFunc()
        {
            return "Person.DummyProtectedFunc.1";
        }

        internal string DummyInternalFunc()
        {
            return "Person.DummyInternalFunc.1";
        }

        protected internal string DummyProtectedAndInternalFunc()
        {
            return "Person.DummyProtectedAndInternalFunc.1";
        }

        protected virtual string DummyProtectedAndVirtualFunc()
        {
            return "Person.DummyProtectedAndVirtualFunc.1";
        }
    }
}
