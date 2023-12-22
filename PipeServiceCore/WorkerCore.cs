using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PipeManagementCore;
using PipeManagementCore.Interfaces;
using System.Text;

namespace PipeServiceCore
{
    public interface IWorker
    {
    }

    public abstract class WorkerCore<T>(ILogger<T> logger, IConfiguration config, IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        : BackgroundService, IWorker where T : class, IWorker
    {
        protected readonly ILogger<T> _logger = logger;
        protected readonly IConfiguration _config = config;
        protected readonly IServiceProvider _serviceProvider = serviceProvider;

        private readonly IHostApplicationLifetime _applicationLifetime = applicationLifetime;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Service gestartet");

            // TODO: load passwords
            Passwords.Set("MyPassword", Convert.ToBase64String(Encoding.UTF8.GetBytes("Pa55w0r7")));

            while (!stoppingToken.IsCancellationRequested)
            {
                var moduleTypes = _config.GetSection("Modules").GetChildren().Select(x => x.Value!);

                foreach (var moduleType in moduleTypes)
                {
                    var module = (IModule)_serviceProvider.GetRequiredService(Type.GetType(moduleType)!);

                    await module.InvokeAsync();
                }

                _applicationLifetime.StopApplication();

                break;
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);

            _logger.LogInformation("Service beendet");
        }
    }
}
