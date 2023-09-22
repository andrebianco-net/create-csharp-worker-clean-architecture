using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductFeederService.Infra.Data.Context;


namespace ProductFeederService.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                options.UseSqlServer(
                            configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                        )
            );

            return services;
        }
    }
}
