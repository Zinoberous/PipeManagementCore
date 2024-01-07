using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PipeServiceCore.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using Serilog.Templates;
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

            #region logging

            var logging = configuration.GetSection("Logging").Get<Logging>()!;

            var loggerConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Verbose();

            if (logging.Console)
            {
                loggerConfig.WriteTo.Console();
            }

            var workfolder = logging.File?.Workfolder ?? Directory.GetCurrentDirectory();

            loggerConfig
                .WriteTo.Logger
                (
                    lc => lc
                        .Filter.ByIncludingOnly(evt => !evt.Properties.ContainsKey("SessionId"))
                        .WriteTo.File(Path.Combine(workfolder, "logs", $"PS_{configuration.GetValue<int>("Id")}_.log"), rollingInterval: RollingInterval.Day)
                )
                .WriteTo.Logger
                (
                    lc => lc
                        .Filter.ByIncludingOnly(evt => evt.Properties.ContainsKey("SessionId") && evt.Properties.ContainsKey("Timestamp"))
                        //.Enrich.FromLogContext()
                        // new ExpressionTemplate() ???
                        .WriteTo.File(Path.Combine(workfolder, "sessions", "{SessionId}_{Timestamp}", "PSS_{SessionId}.log"))
                );

            //loggerConfig.WriteTo.File($"{workfolder}/logs/PS_{{service_id}}_{{Date}}.log");
            //loggerConfig.WriteTo.File($"{workfolder}/sessions/{{session_id}}_{{Date}}/PSS_{{session_id}}.log");

            //if (logging.Mail is not null)
            //{
            //    EmailConnectionInfo info = new()
            //    {
            //        MailServer = logging.Mail.Host,
            //        Port = logging.Mail.Port,
            //        NetworkCredentials = new NetworkCredential(logging.Mail.Username, logging.Mail.Password),
            //        EnableSsl = true,
            //        FromEmail = logging.Mail.From,
            //        ToEmail = string.Join(", ", logging.Mail.To),
            //        EmailSubject = "Test",
            //        IsBodyHtml = true
            //    };

            //    loggerConfig.WriteTo.Email(info, restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(logging.Mail.LogLevel ?? "Error"));
            //}

            var logger = loggerConfig.CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            #endregion

            services.AddHostedService<THostedService>();

            return services;
        }
    }
}
