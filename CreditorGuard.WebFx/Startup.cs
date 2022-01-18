using Application.WebFx.Extensions;
using Application.WebFx.Interceptors;
using Application.WebFx.Services;
using JetBrains.Annotations;
using LightInject;
using LightInject.Interception;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.WebFx
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Startup
    {
        public void ConfigureServices([NotNull] IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices();

            services.AddSwaggerDocument();
        }

        public void ConfigureContainer([NotNull] IServiceContainer container)
        {
            // async scope manager that make it work, default is per logical call
            // container.ScopeManagerProvider = new AsyncLocalScopeManagerProvider();

            container.Register<IInterceptor, AttributeInterceptor>();
            container.Decorate<IInterceptor, AsyncAttributeInterceptor>();

            container.Register<IService, Service>(new PerScopeLifetime());
            container.Register<IContext, Context>(new PerScopeLifetime());
            container.RegisterInstance(container);
            container.InterceptByAttributes<IInterceptor>(typeof(MarkerAttribute));
        }

        public void Configure([NotNull] IApplicationBuilder app, [NotNull] IHostingEnvironment env)
        {
            // async scope manager that make it work, default is per logical call
            // app.UseAsyncScopeManager();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMvcWithDefaultRoute();
        }
    }
}