using Microsoft.Extensions.Logging;
using PipeManagementCore.Interfaces;

namespace PipeModuleCore.Implements
{
    public abstract class ModuleCore<T>(ILogger<T> logger) : IModule where T : class, IModule
    {
        protected readonly ILogger<T> _logger = logger;

        public void Invoke()
        {
            Execute();
        }

        protected abstract void Execute();
    }
}
