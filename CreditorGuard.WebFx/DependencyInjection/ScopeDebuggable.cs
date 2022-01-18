using System.Diagnostics;
using LightInject;

namespace Application.WebFx.DependencyInjection
{
    [DebuggerDisplay("Scope#{HashCode}")]
    public class ScopeDebuggable : Scope
    {
        public int HashCode => GetHashCode();

        public ScopeDebuggable(IScopeManager scopeManager, Scope parentScope) : base(scopeManager, parentScope)
        {
        }

        public ScopeDebuggable(ServiceContainer serviceFactory) : base(serviceFactory)
        {
        }
    }
}