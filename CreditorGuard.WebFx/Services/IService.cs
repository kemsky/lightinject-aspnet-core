using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Application.WebFx.Services
{
    public interface IService
    {
        [NotNull]
        Task ExecuteAsync();
    }
}