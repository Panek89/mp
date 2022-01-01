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

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}