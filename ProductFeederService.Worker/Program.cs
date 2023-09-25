using ProductFeederService.Infra.IoC;
using ProductFeederService.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddInfrastructure(hostContext.Configuration);        
        services.AddHostedService<ProductFeederServiceWorker>();        
    })  
    .UseWindowsService()
    .Build();

host.Run();
