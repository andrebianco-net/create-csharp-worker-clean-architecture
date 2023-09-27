using ProductFeederService.Infra.IoC;
using ProductFeederService.Worker;
using Serilog;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddInfrastructure(hostContext.Configuration);
        services.AddInfrastructureSerilog(hostContext.Configuration);
        services.AddHostedService<ProductFeederServiceWorker>();        
    })  
    .UseSerilog()
    .UseWindowsService()
    .Build();

host.Run();
