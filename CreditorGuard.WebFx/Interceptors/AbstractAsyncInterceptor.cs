using System;
using System.Reflection;
using JetBrains.Annotations;
using LightInject.Interception;

namespace Application.WebFx.Interceptors
{
    internal abstract class AbstractAsyncInterceptor : IInvocationInfo
    {
        [NotNull]
        public MethodInfo Method { get; }

        [NotNull]
        public IProxy Proxy { get; }

        [NotNull]
        public object[] Arguments { get; }

        [NotNull]
        public MethodInfo TargetMethod { get; }

        [NotNull]
        protected Type Type => InvocationInfo.Proxy.Target.GetType();

        [NotNull]
        protected readonly IInvocationInfo InvocationInfo;

        protected AbstractAsyncInterceptor([NotNull] IInvocationInfo invocationInfo)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Method = invocationInfo.Method;
            // ReSharper disable once AssignNullToNotNullAttribute
            Proxy = invocationInfo.Proxy;
            // ReSharper disable once AssignNullToNotNullAttribute
            Arguments = invocationInfo.Arguments;
            // ReSharper disable once AssignNullToNotNullAttribute
            TargetMethod = invocationInfo.TargetMethod;
            InvocationInfo = invocationInfo;
        }

        public abstract object Proceed();
    }
}