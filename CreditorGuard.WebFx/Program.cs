using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Application.WebFx
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            CreateWebHost(args).Run();

            return 0;
        }

        public static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseLightInject()
                .UseStartup<Startup>()
                .Build();
    }
}