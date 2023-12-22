using Microsoft.Extensions.Logging;
using PipeModuleCore.Implements;

namespace PM.Prototype.Modules
{
    public class Test2Module(ILogger<Test2Module> logger) : ModuleCore<Test2Module>(logger)
    {
        protected override async Task ExecuteAsync()
        {
            _logger.LogInformation($"{nameof(Test2Module)}");

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
