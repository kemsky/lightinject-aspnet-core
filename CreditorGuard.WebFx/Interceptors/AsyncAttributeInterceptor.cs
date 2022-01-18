using System.Threading.Tasks;
using Application.WebFx.Services;
using JetBrains.Annotations;
using LightInject;
using LightInject.Interception;
using Microsoft.Extensions.Logging;

namespace Application.WebFx.Interceptors
{
    [UsedImplicitly]
    internal sealed class AsyncAttributeInterceptor : AsyncInterceptor
    {
        [NotNull]
        private ILoggerFactory LoggerFactory { get; }

        [NotNull]
        private IServiceContainer Container { get; }

        public AsyncAttributeInterceptor(
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] IServiceContainer container,
            [NotNull] IInterceptor targetInterceptor
        ) : base(targetInterceptor)
        {
            LoggerFactory = loggerFactory;
            Container = container;
        }

        [NotNull]
        protected override async Task InvokeAsync([NotNull] IInvocationInfo invocationInfo)
        {
            // AsyncTransactionalInterceptor requires scoped service

            invocationInfo = new AsyncTransactionalInterceptor(Container.GetInstance<IContext>(), invocationInfo, LoggerFactory.CreateLogger<AsyncTransactionalInterceptor>());

            await base.InvokeAsync(invocationInfo);
        }

        [NotNull]
        protected override async Task<T> InvokeAsync<T>([NotNull] IInvocationInfo invocationInfo)
        {
            // AsyncTransactionalInterceptor requires scoped service

            invocationInfo = new AsyncTransactionalInterceptor<T>(Container.GetInstance<IContext>(), invocationInfo, LoggerFactory.CreateLogger<AsyncTransactionalInterceptor<T>>());

            return await base.InvokeAsync<T>(invocationInfo);
        }
    }
}