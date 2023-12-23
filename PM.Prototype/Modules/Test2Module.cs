using Microsoft.Extensions.Logging;
using PipeModuleCore.Implements;

namespace PM.Prototype.Modules
{
    public class Test2Module(ILogger<Test2Module> logger) : ModuleCore<Test2Module>(logger)
    {
        protected override void Execute()
        {
            _logger.LogInformation($"{nameof(Test2Module)}");

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}
