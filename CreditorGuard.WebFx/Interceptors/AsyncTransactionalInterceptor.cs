using System.Threading.Tasks;
using Application.WebFx.Services;
using JetBrains.Annotations;
using LightInject.Interception;
using Microsoft.Extensions.Logging;

namespace Application.WebFx.Interceptors
{
    internal class AsyncTransactionalInterceptor : AbstractAsyncInterceptor
    {
        [NotNull]
        private readonly ILogger<AsyncTransactionalInterceptor> _logger;

        [NotNull]
        private IContext Context { get; }

        public AsyncTransactionalInterceptor(
            [NotNull] IContext context,
            [NotNull] IInvocationInfo invocationInfo,
            [NotNull] ILogger<AsyncTransactionalInterceptor> logger
        ) : base(invocationInfo)
        {
            _logger = logger;
            Context = context;
        }

        public override object Proceed()
        {
            return ProceedAsync();
        }

        private async Task ProceedAsync()
        {
            await Context.BeginAsync();

            _logger.LogDebug("Start transaction");

            try
            {
                await (Task)InvocationInfo.Proceed();
            }
            finally
            {
                _logger.LogDebug("End transaction");

                await Context.EndAsync();
            }
        }
    }

    internal class AsyncTransactionalInterceptor<T> : AbstractAsyncInterceptor
    {
        [NotNull]
        private readonly ILogger<AsyncTransactionalInterceptor<T>> _logger;

        [NotNull]
        private IContext Context { get; }


        public AsyncTransactionalInterceptor(
            [NotNull] IContext context,
            [NotNull] IInvocationInfo invocationInfo,
            [NotNull] ILogger<AsyncTransactionalInterceptor<T>> logger
        ) : base(invocationInfo)
        {
            _logger = logger;
            Context = context;
        }

        public override object Proceed()
        {
            return ProceedAsync();
        }

        private async Task<T> ProceedAsync()
        {
            T result;

            await Context.BeginAsync();

            _logger.LogDebug("Start transaction");

            try
            {
                result = await (Task<T>)InvocationInfo.Proceed();
            }
            finally
            {
                _logger.LogDebug("End transaction");

                await Context.EndAsync();
            }

            return result;
        }
    }
}