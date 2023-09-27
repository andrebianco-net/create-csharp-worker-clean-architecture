using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


namespace ProductFeederService.Infra.IoC
{
    public static class DependencyInjectionSerilog
    {
        public static IServiceCollection AddInfrastructureSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            string folder = configuration["Serilog:Folder"];
            string file = configuration["Serilog:File"];

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
               .Enrich.FromLogContext()
               .WriteTo.File($"{folder}/{file}")
               .CreateLogger();

            services.AddSingleton(Log.Logger);      

            return services;
        }
    }
}
