using Microsoft.Extensions.DependencyInjection;

namespace ProductFeederService.Service.Tests
{
    public class DependenceInjectionProductServiceFixture
    {
        private readonly IServiceScope _scope;

        public DependenceInjectionProductServiceFixture()
        {
            var serviceCollection = new ServiceCollection();
            
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _scope = serviceProvider.CreateScope();
        }

        public T GetService<T>()
        {
            return _scope.ServiceProvider.GetRequiredService<T>();
        }
    }

    public class ProductServiceUnitTest1 : IClassFixture<DependenceInjectionProductServiceFixture>
    {
        public ProductServiceUnitTest1(DependenceInjectionProductServiceFixture serviceFixture)
        {
            
        }
    }
}