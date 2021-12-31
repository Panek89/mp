using MachinesWorker;
using MachinesWorker.Context;
using MachinesWorker.Context.Initializer;
using MachinesWorker.Context.Seed;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("SQLDEVELOPER2016")));
        services.AddScoped<IDbInitializer, DbInitialize>();
        services.AddScoped<IDbSeed, DbSeed>();
        services.AddHostedService<Worker>();
    })
    .Build();

using(var scope = host.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
    var dbSeed = scope.ServiceProvider.GetService<IDbSeed>();
    var dbExists = dbInitializer.DbExists();
    if (dbExists)
    {
        dbSeed.Seed();
    }
}

await host.RunAsync();
