using Machines.Domain.Interfaces;
using MP.MachinesApi.Models;

namespace Machines.DataAccess.EfCore.Repositories
{
    public class ParameterRepository : GenericRepository<Parameter>, IParameterRepository
    {
        public ParameterRepository(ApplicationContext context):base(context)
        {
        }
    }
}