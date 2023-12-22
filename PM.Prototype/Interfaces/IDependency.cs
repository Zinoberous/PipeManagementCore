namespace PM.Prototype.Interfaces
{
    public interface IDependency
    {
        string Password { get; }

        Task SendAsyc();
    }
}
