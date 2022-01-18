using Microsoft.AspNetCore.Builder;

namespace Application.WebFx.Middleware
{
    public static class AsyncScopeManagerMiddlewareExtension
    {
        public static IApplicationBuilder UseAsyncScopeManager(this IApplicationBuilder app)
        {
            app.UseMiddleware<AsyncScopeManagerMiddleware>();

            return app;
        }
    }
}