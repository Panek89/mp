using MP.MachinesApi.Models;

namespace Machines.DataAccess.EfCore.Services.DB
{
    public interface IDbSeed
    {
        Task<int> Seed();
        IList<Parameter> GenerateParameters(int count);
        IList<Machine> GenerateMachines(int count, IList<Parameter> parameters);
    }
}