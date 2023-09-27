using ProductFeederService.Application.Interfaces;

namespace ProductFeederService.Worker;

public class ProductFeederServiceWorker : BackgroundService
{
    private readonly ILogger<ProductFeederServiceWorker> _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;

    public ProductFeederServiceWorker(ILogger<ProductFeederServiceWorker> logger,
                                      IConfiguration configuration,
                                      IServiceProvider serviceProvider)
    {
        _logger = logger;
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"ProductFeederServiceWorker -> Worker running at: {DateTimeOffset.Now}");
            await ProductFeederRun(stoppingToken);
            int interval = int.Parse(_configuration["Worker:Interval"].ToString());
            await Task.Delay(interval, stoppingToken);
        }
    }

    private async Task ProductFeederRun(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IProductFeederAppService>();
            await scopedProcessingService.ProductFeederRun();
        }
    }
}
