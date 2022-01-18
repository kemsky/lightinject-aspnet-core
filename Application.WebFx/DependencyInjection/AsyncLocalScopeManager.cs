using System.Threading;
using JetBrains.Annotations;
using LightInject;

namespace Application.WebFx.DependencyInjection
{
    public class AsyncLocalScopeManager : IScopeManager
    {
        [NotNull]
        public static readonly AsyncLocal<AsyncScopeHolder> AsyncLocal = new AsyncLocal<AsyncScopeHolder>();

        public IServiceFactory ServiceFactory { get; }

        public AsyncLocalScopeManager(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        public Scope CurrentScope
        {
            get => AsyncLocal.Value?.Scope;
            set
            {
                if (AsyncLocal.Value != null && AsyncLocal.Value.Scope == null)
                {
                    // Lazy initialization
                    AsyncLocal.Value.Scope = value;
                }
                else
                {
                    // Eager initialization
                    AsyncLocal.Value = new AsyncScopeHolder
                    {
                        Scope = value
                    };
                }
            }
        }

        public Scope BeginScope()
        {
            var currentScope = CurrentScope;

            var scope = new ScopeDebuggable(this, currentScope);

            CurrentScope = scope;

            return scope;
        }

        public void EndScope(Scope scope)
        {
            AsyncLocal.Value = null;
        }
    }
}