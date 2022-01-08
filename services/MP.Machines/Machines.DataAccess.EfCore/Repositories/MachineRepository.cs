using Machines.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using MP.MachinesApi.Models;

namespace Machines.DataAccess.EfCore.Repositories
{
    public class MachineRepository: GenericRepository<Machine>, IMachineRepository
    {
        public MachineRepository(ApplicationContext context):base(context)
        {
        }

        public void AssignParameterToMachine(Machine machine, Parameter parameter)
        {
            if (machine.Parameters == null)
            {
                List<Parameter> parameters = new List<Parameter>{ parameter };
                machine.Parameters = parameters;
            }
            else 
            {
                machine.Parameters.Add(parameter);
            }
        }
    }
}
