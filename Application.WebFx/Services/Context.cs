using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Application.WebFx.Services
{
    [UsedImplicitly]
    internal class Context : IContext
    {
        [NotNull]
        private ILogger<Context> Logger { get; }

        public Context(
            [NotNull] ILogger<Context> logger
        )
        {
            Logger = logger;
        }

        public virtual Task<bool> BeginAsync()
        {
            Logger.LogInformation("Context#{HashCode}: BeginAsync", GetHashCode());

            return Task.FromResult(true);
        }

        public Task<bool> EndAsync()
        {
            Logger.LogInformation("Context#{HashCode}: EndAsync", GetHashCode());

            return Task.FromResult(true);
        }
    }
}