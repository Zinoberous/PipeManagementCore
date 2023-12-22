using Microsoft.Extensions.Logging;
using PipeManagementCore;
using PipeModuleCore.Implements;
using PM.Prototype.Interfaces;
using System.Text;

namespace PM.Prototype.Modules
{
    public class Test1Module(ILogger<Test1Module> logger, IDependency dependency) : ModuleCore<Test1Module>(logger)
    {
        private readonly IDependency _dependency = dependency;

        protected override async Task ExecuteAsync()
        {
            _logger.LogInformation($"{nameof(Test1Module)}");

            _logger.LogWarning("{password}", Passwords.Decrypt(_dependency.Password));

            await _dependency.SendAsyc();
        }
    }
}
