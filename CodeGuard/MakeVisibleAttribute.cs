using System;

namespace CodeGuard
{
    /// <summary>
    /// Ignores to remove the definition (private, internal)(method, property, field etc.).
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class MakeVisibleAttribute : Attribute
    {
        
    }
}
