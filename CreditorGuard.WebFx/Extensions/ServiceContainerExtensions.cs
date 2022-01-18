using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using LightInject;
using LightInject.Interception;

namespace Application.WebFx.Extensions
{
    public static class ServiceContainerExtensions
    {
        public static void InterceptByAttributes([NotNull] this IServiceRegistry container, [NotNull] Func<IServiceFactory, IInterceptor> factory, params Type[] attributeTypes)
        {
            container.Decorate(new DecoratorRegistration
            {
                CanDecorate = registration =>
                {
                    var type = registration.ImplementingType ?? registration.ServiceType;
                    if (type != null)
                    {
                        var publicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        var annotatedMethods = publicMethods.Where(m => m.GetCustomAttributes().Any(a => attributeTypes.Contains(a.GetType()))).ToArray();

                        //decorate?
                        return annotatedMethods.Length > 0;
                    }

                    return false;
                },
                ImplementingTypeFactory = (serviceFactory, registration) =>
                {
                    var proxyBuilder = new ProxyBuilder { MethodSelector = new MethodSelector() };

                    var definition = new ProxyDefinition(registration.ImplementingType, useLazyTarget: false);

                    definition.Implement(() => factory.Invoke(serviceFactory), m => m.GetCustomAttributes().Any(a => attributeTypes.Contains(a.GetType())));

                    return proxyBuilder.GetProxyType(definition);
                }
            });
        }

        public static void InterceptByAttributes<TInterceptor>([NotNull] this IServiceRegistry container, [NotNull] params Type[] attributeTypes) where TInterceptor : IInterceptor
        {
            container.InterceptByAttributes(factory => factory.GetInstance<TInterceptor>(), attributeTypes);
        }
    }
}