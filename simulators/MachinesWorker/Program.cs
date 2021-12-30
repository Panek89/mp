using MachinesWorker;
using MachinesWorker.Context;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("")));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
