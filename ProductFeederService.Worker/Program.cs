using ProductFeederService.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ProductFeederServiceWorker>();
    })
    .Build();

host.Run();
