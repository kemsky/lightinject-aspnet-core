// ReSharper disable once CheckNamespace

namespace System.Runtime.CompilerServices
{
    // https://stackoverflow.com/questions/64749385/predefined-type-system-runtime-compilerservices-isexternalinit-is-not-defined
    /// <summary>
    /// The IsExternalInit type is only included in the net5.0 (and future) target frameworks.
    /// When compiling against older target frameworks you will need to manually define this type.
    /// </summary>
    internal static class IsExternalInit
    {
    }
}