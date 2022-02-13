using MachineParameters.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MachineParameters.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ParameterSimulation> ParameterSimulations { get; set; }
    }
}
