using System.Threading.Tasks;
using Application.WebFx.Extensions;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Application.WebFx.Services
{
    [UsedImplicitly]
    internal class Service : IService
    {
        [NotNull]
        private IContext Context { get; }

        [NotNull]
        private ILogger<Service> Logger { get; }

        public Service(
            [NotNull] IContext context,
            [NotNull] ILogger<Service> logger
        )
        {
            Context = context;
            Logger = logger;
        }

        [Marker]
        public virtual Task ExecuteAsync()
        {
            Logger.LogInformation("Service#{HashCode}: ExecuteAsync", GetHashCode());

            return Task.CompletedTask;
        }
    }
}