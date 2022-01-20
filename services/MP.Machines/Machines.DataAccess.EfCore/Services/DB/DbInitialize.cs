using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Machines.DataAccess.EfCore.Services.DB
{
    public class DbInitialize : IDbInitialize
    {
        private readonly ILogger<DbInitialize> _logger;
        private readonly ApplicationContext _applicationContext;

        public DbInitialize(ILogger<DbInitialize> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        public bool CheckIfExists()
        {
            return _applicationContext.Database.GetService<IRelationalDatabaseCreator>().Exists();
        }

        public bool Initialize()
        {
            var dbExists = _applicationContext.Database.GetService<IRelationalDatabaseCreator>().Exists(); 
            if (dbExists)
            {
                _logger.LogInformation("DB Already exists");
                checkPendingAndApplyMigrations();
            }
            else 
            {
                _logger.LogInformation("DB Not Exists, try to create DB");
                try
                {
                    _applicationContext.Database.Migrate();
                    dbExists = true;
                    _logger.LogInformation("DB Created");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occurs when trying to create a database, {ex}");
                }
            }
            
            return dbExists;
        }

        private void checkPendingAndApplyMigrations()
        {
            _logger.LogInformation("Check if there is another migrations to applied");
            var isMigrationNeeded = _applicationContext.Database.GetPendingMigrations().Any();
            if (isMigrationNeeded)
            {
                _applicationContext.Database.Migrate();
                _logger.LogInformation("Pending migration applied");
            }
            else
            {
                _logger.LogInformation("No migration needed");
            }
        }
    }
}