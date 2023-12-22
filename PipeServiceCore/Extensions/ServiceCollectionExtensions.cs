using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PipeManagementCore.Interfaces;

namespace PipeServiceCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService<THostedService>(this IServiceCollection services)
            where THostedService : class, IHostedService
        {
            services.AddHostedService<THostedService>();

            return services;
        }

        public static IServiceCollection AddModule<TModule>(this IServiceCollection services)
            where TModule : class, IModule
        {
            services.AddTransient<TModule>();

            return services;
        }

        public static IServiceCollection AddDependency<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation>? implementationFactory = null)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory is null)
            {
                services.AddTransient<TService, TImplementation>();
            }
            else
            {
                services.AddTransient<TService, TImplementation>(implementationFactory);
            }

            return services;
        }
    }
}
