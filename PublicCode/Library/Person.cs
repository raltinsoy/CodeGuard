using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Person
    {
        public string Name { get; set; }

        public Person(int id, string name)
        {
        }

        public string GetKey()
        {
            return "Person.GetKey.2";
        }

        private string DummyPrivateFunc()
        {
            return "Person.DummyPrivateFunc.2";
        }

        protected string DummyProtectedFunc()
        {
            return "Person.DummyProtectedFunc.2";
        }
    }
}
