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

        public bool Initialize()
        {
            var dbExists = _applicationContext.Database.GetService<IRelationalDatabaseCreator>().Exists(); 
            if (dbExists)
            {
                _logger.LogInformation("DB Already exists");
            }
            else 
            {
                _logger.LogInformation("DB Not Exists, try to create DB");
                try
                {
                    var dbCreated = _applicationContext.Database.EnsureCreated();
                    if(dbCreated) 
                    {
                        _logger.LogInformation("DB Created");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occurs when trying to create a database, {ex}");
                }
            }
            
            return dbExists;
        }
    }
}