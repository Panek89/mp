namespace MachinesWorker.Context.Seed
{
    public class DbSeed : IDbSeed
    {
        private readonly ILogger<DbSeed> _logger;

        public DbSeed(ILogger<DbSeed> logger)
        {
            _logger = logger;
        }

        public void Seed()
        {
            _logger.LogInformation("I am here");
        }
    }
}