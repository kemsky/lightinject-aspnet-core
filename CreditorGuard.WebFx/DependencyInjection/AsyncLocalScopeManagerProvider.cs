using LightInject;

namespace Application.WebFx.DependencyInjection
{
    public class AsyncLocalScopeManagerProvider : ScopeManagerProvider
    {
        protected override IScopeManager CreateScopeManager(IServiceFactory serviceFactory)
        {
            return new AsyncLocalScopeManager(serviceFactory);
        }
    }
}