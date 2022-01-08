using MP.MachinesApi.Models;

namespace Machines.Domain.Interfaces
{
    public interface IMachineRepository : IGenericRepository<Machine>
    {
        void AssignParameterToMachine(Machine machine, Parameter parameter);
    }
}