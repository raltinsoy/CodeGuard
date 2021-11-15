using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGuard.Fody
{
    public class SDKExportException : Exception
    {
        public SDKExportException() { }

        public SDKExportException(string message) : base(message) { }

        public SDKExportException(string message, Exception inner) : base(message, inner) { }

        protected SDKExportException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
