using Microsoft.Extensions.Logging;
using PipeManagementCore.Interfaces;

namespace PipeModuleCore.Implements
{
    public abstract class ModuleAsyncCore<T>(ILogger<T> logger) : IModuleAsync where T : class, IModuleAsync
    {
        protected readonly ILogger<T> _logger = logger;

        public async Task InvokeAsync()
        {
            await ExecuteAsync();
        }

        protected abstract Task ExecuteAsync();
    }
}
