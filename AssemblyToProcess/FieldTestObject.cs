using CodeGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyToProcess
{
    public class FieldTestObject
    {
        public string PublicField;

        protected string ProtectedField;

        internal string InternalField;

        [MakeVisible]
        internal string InternalField_Visible;

        private string PrivateField;

        [MakeVisible]
        private string PrivateField_Visible;

        [DoNotThrowException]
        public FieldTestObject()
        {
        }
    }
}
