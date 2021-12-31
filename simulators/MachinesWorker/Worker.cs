using MachinesWorker.Context;

namespace MachinesWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly AppDbContext _context;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
