using CodeGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyToProcess
{
    public class PropertyTestObject
    {
        public string PublicProperty { get; }

        protected string ProtectedProperty { get; }

        internal string InternalProperty { get; }

        [MakeVisible]
        internal string InternalProperty_Visible { get; }

        private string PrivateProperty { get; }

        [MakeVisible]
        private string PrivateProperty_Visible { get; }

        [DoNotThrowException]
        public PropertyTestObject()
        {
            PublicProperty = "Default";
        }
    }
}
