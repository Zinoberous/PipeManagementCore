﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PipeManagementCore;
using PipeManagementCore.Interfaces;
using PipeServiceCore.Models;
using Serilog.Context;
using System.Text;

namespace PipeServiceCore
{
    public abstract class WorkerCore<T> : BackgroundService where T : class, IHostedService
    {
        protected readonly ILogger<T> _logger;
        protected readonly IConfiguration _config;
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IHostApplicationLifetime _applicationLifetime;

        private readonly IDisposable _globalStopMonitor;

        public WorkerCore
        (
            ILogger<T> logger,
            IConfiguration config,
            IServiceProvider serviceProvider,
            IHostApplicationLifetime applicationLifetime,
            IOptionsMonitor<GlobalStopMonitor> globalStopMonitor
        )
        {
            _logger = logger;
            _config = config;
            _serviceProvider = serviceProvider;
            _applicationLifetime = applicationLifetime;

            _globalStopMonitor = globalStopMonitor.OnChange(monitor =>
            {
                _logger.LogInformation("{globalStop}", monitor.GlobalStop);
            })!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("init");

            // TODO: load passwords
            Passwords.Set("MyPassword", Convert.ToBase64String(Encoding.UTF8.GetBytes("Pa55w0r7")));

            while (!stoppingToken.IsCancellationRequested)
            {
                using (LogContext.PushProperty("SessionId", Guid.NewGuid()))
                using (LogContext.PushProperty("Timestamp", DateTime.UtcNow.ToString("yyyyMMddHHmmss")))
                {
                    //var moduleTypes = _config.GetSection("Modules").GetChildren().Select(x => x.Value!);

                    //foreach (var moduleType in moduleTypes)
                    //{
                    //    var service = _serviceProvider.GetRequiredService(Type.GetType(moduleType)!);

                    //    if (service is IModule module)
                    //    {
                    //        module.Invoke();
                    //    }
                    //    else if (service is IModuleAsync moduleAsync)
                    //    {
                    //        await moduleAsync.InvokeAsync();
                    //    }
                    //}

                    _logger.LogInformation("run");

                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }

                // TODO: raus
                _applicationLifetime.StopApplication();
                break;
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _globalStopMonitor.Dispose();

            await base.StopAsync(cancellationToken);
        }
    }
}
