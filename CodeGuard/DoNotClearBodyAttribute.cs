using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGuard
{
    /// <summary>
    /// Ignores to clear the body.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class DoNotClearBodyAttribute : Attribute
    {
        //TODO: does not work on property
    }
}
