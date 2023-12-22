using PipeServiceCore;

namespace PS.Prototype
{
    public class Worker(ILogger<Worker> logger, IConfiguration config, IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime)
        : WorkerCore<Worker>(logger, config, serviceProvider, applicationLifetime)
    {
    }
}
