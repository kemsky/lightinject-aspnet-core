using System;
using JetBrains.Annotations;
using LightInject.Interception;

namespace Application.WebFx.Interceptors
{
    [UsedImplicitly]
    internal class AttributeInterceptor : IInterceptor
    {
        public object Invoke([NotNull] IInvocationInfo invocationInfo)
        {
            throw new InvalidOperationException($"Only async methods are allowed: {invocationInfo.Method.DeclaringType}::{invocationInfo.Method.Name}");
        }
    }
}