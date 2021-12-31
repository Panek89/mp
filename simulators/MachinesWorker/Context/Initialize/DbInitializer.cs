using Microsoft.EntityFrameworkCore;

namespace MachinesWorker.Context.Initializer
{
    public class DbInitialize : IDbInitializer
    {
        private readonly ILogger<DbInitialize> _logger;
        private readonly AppDbContext _context;

        public DbInitialize(ILogger<DbInitialize> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        }

        public bool DbExists()
        {
            _context.Database.Migrate();

            return !_context.Database.EnsureCreated();
        }
    }
}