using Machines.DataAccess.EfCore.Repositories;
using Machines.Domain.Interfaces;

namespace Machines.DataAccess.EfCore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Parameters = new ParameterRepository(_context);
            Machines = new MachineRepository(_context);
        }

        public IParameterRepository Parameters { get; private set; }
        public IMachineRepository Machines { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}