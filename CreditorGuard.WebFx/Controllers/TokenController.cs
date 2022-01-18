using System.Threading.Tasks;
using Application.WebFx.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.WebFx.Controllers
{
    public class TokenController : Controller
    {
        [NotNull]
        private IService Service { get; }

        [NotNull]
        private ILogger<TokenController> Logger { get; }

        public TokenController(
            [NotNull] IService service,
            [NotNull] ILogger<TokenController> logger
        )
        {
            Service = service;
            Logger = logger;
        }

        [HttpGet]
        [Route("token")]
        public async Task<IActionResult> Token()
        {
            Logger.LogInformation("Controller Action begin");

            await Service.ExecuteAsync();

            Logger.LogInformation("Controller Action end");

            return Ok();
        }
    }
}