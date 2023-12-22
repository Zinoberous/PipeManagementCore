using PM.Prototype.Interfaces;

namespace PM.Prototype.Implements
{
    public class Dependency(string password) : IDependency
    {
        public string Password { get; } = password;

        public async Task SendAsyc()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
