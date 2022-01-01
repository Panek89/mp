using Microsoft.EntityFrameworkCore;
using MP.MachinesApi.Models;

namespace Machines.DataAccess.EfCore;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Machine> Machines { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
}
