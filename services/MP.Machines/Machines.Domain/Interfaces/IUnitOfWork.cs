namespace Machines.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IParameterRepository Parameters { get; }
        IMachineRepository Machines { get; }
        Task<int> CompleteAsync();
    }
}