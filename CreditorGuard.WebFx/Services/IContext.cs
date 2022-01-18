using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Application.WebFx.Services
{
    public interface IContext
    {
        [NotNull]
        Task<bool> BeginAsync();

        [NotNull]
        Task<bool> EndAsync();
    }
}