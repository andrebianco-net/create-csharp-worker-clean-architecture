using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductFeederService.Application.Services;
using ProductFeederService.Application.Interfaces;
using ProductFeederService.Application.Mappings;
using ProductFeederService.Domain.Interfaces;
using ProductFeederService.Infra.Data.Repositories;
using ProductFeederService.Infra.Data.Settings;


namespace ProductFeederService.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            // MongoDB
            services.Configure<MongoDBSettings>(
                options => {
                    options.ConnectionURI = configuration["MongoDB:ConnectionURI"];
                    options.DatabaseName = configuration["MongoDB:DatabaseName"];
                    options.CollectionName = configuration["MongoDB:CollectionName"];
                }
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
