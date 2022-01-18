using System;
using System.Threading.Tasks;
using Application.WebFx.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Application.WebFx.Middleware
{
    [UsedImplicitly]
    internal sealed class AsyncScopeManagerMiddleware
    {
        [NotNull]
        private readonly RequestDelegate _next;

        public AsyncScopeManagerMiddleware(
            [NotNull] RequestDelegate next
        )
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            AsyncLocalScopeManager.AsyncLocal.Value = new AsyncScopeHolder();

            await _next.Invoke(httpContext);
        }
    }
}