namespace ProductFeederService.Worker;

public class ProductFeederServiceWorker : BackgroundService
{
    private readonly ILogger<ProductFeederServiceWorker> _logger;

    public ProductFeederServiceWorker(ILogger<ProductFeederServiceWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
