using MachinesWorker.Models;
using Microsoft.EntityFrameworkCore;

namespace MachinesWorker.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Parameter> Parameters { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
    }
}