using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGuard
{
    /// <summary>
    /// Clear the body but not add exception
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class DoNotThrowExceptionAttribute : Attribute
    {
    }
}
