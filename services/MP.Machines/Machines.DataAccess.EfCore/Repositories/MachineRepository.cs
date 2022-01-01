using Machines.Domain.Interfaces;
using MP.MachinesApi.Models;

namespace Machines.DataAccess.EfCore.Repositories
{
    public class MachineRepository: GenericRepository<Machine>, IMachineRepository
    {
        public MachineRepository(ApplicationContext context):base(context)
        {
        }
    }
}
