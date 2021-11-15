using System;

namespace CodeGuard
{
    /// <summary>
    /// Ignores to clear the body.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class IgnoreAttribute : Attribute
    {
    }
}
