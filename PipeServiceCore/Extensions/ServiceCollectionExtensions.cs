using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PipeServiceCore.Models;
using System.Net;

namespace PipeServiceCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService<THostedService>(this IServiceCollection services, IHostApplicationBuilder builder)
            where THostedService : class, IHostedService
        {
            var configuration = builder.Configuration;

            configuration
                .AddJsonFile("globalsettings.json", true, true)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddJsonFile("local.settings.json", true, true);

            // GlobalStop überwachen
            services.Configure<GlobalStopMonitor>(configuration);

            services.AddHostedService<THostedService>();

            return services;
        }
    }
}
