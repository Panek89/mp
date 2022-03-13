using Machines.DataAccess.EfCore.Services.DB;
using Machines.Domain.Configuration.Options;
using Machines.EventServiceBus.Services.RabbitMQ;
using Microsoft.Extensions.Options;

namespace Machines.Api.Startup
{
    public static class AppStartup
    {
        public static async Task ApplicationStartup(IServiceScope serviceScope)
        {
            var services = serviceScope.ServiceProvider;
            await Task.Delay(TimeSpan.FromMinutes(1)); // TODO: Change the logic for try multiple times connect to RabbitMQ via Docker
            

            var rabbitMQ = services.GetRequiredService<IRabbitMqConnection>();
            var rabbitMqOptions = services.GetRequiredService<IOptions<RabbitMqOptions>>();
            var connectionCreated = rabbitMQ.CreateConnection(
                    rabbitMqOptions.Value.HostName, rabbitMqOptions.Value.UserName, rabbitMqOptions.Value.Password);
            var checkif = rabbitMQ.CheckIfConnectionExists();
            if (connectionCreated)
            {   
                foreach (var queueName in rabbitMqOptions.Value.QueuesNames)
                {
                    rabbitMQ.CreateQueue(queueName);
                }
            }

            var appContext = services.GetRequiredService<IDbInitialize>();
            var dbExists = appContext.Initialize();

            if (dbExists)
            {
                var dbSeed = services.GetRequiredService<IDbSeed>();
                var seedOptions = services.GetRequiredService<IOptions<SeedOptions>>();
                if (seedOptions.Value.DoSeed)
                {
                    await dbSeed.Seed();
                }
            }
        }
    }
}