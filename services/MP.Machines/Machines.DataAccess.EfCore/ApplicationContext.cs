using Microsoft.EntityFrameworkCore;
using MP.MachinesApi.Models;

namespace Machines.DataAccess.EfCore;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Machine> Machines { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
}
