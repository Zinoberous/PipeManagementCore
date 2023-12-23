using Microsoft.Extensions.Options;
using PipeServiceCore;
using PipeServiceCore.Models;

namespace PS.Prototype
{
    public class Worker(ILogger<Worker> logger, IConfiguration config, IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime, IOptionsMonitor<GlobalStopMonitor> globalStopMonitor)
        : WorkerCore<Worker>(logger, config, serviceProvider, applicationLifetime, globalStopMonitor)
    {
    }
}
