using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductFeederService.Application.Services;
using ProductFeederService.Application.Interfaces;
using ProductFeederService.Application.Mappings;
using ProductFeederService.Domain.Interfaces;
using ProductFeederService.Infra.Data.Context;
using ProductFeederService.Infra.Data.Repositories;


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

            // Repository
            services.AddScoped<IProductRepository, ProductRepository>();

            // Service
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductFeederAppService, ProductFeederAppService>();

            // Mapper
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));            

            return services;
        }
    }
}
