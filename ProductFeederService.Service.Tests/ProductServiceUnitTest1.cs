using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using ProductFeederService.Application.DTOs;
using ProductFeederService.Application.Interfaces;
using ProductFeederService.Application.Mappings;
using ProductFeederService.Application.Services;
using ProductFeederService.Domain.Entities;
using ProductFeederService.Domain.Interfaces;
using ProductFeederService.Infra.Data.Context;
using ProductFeederService.Infra.Data.Repositories;

namespace ProductFeederService.Service.Tests
{
    public class DependenceInjectionProductServiceFixture
    {
        private readonly IServiceScope _scope;

        public DependenceInjectionProductServiceFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IProductRepository, ProductRepository>();
            serviceCollection.AddTransient<IProductService, ProductService>();
            serviceCollection.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            var configuration = new ConfigurationBuilder()
            .SetBasePath($"{Directory.GetCurrentDirectory()}/../../../")
            .AddJsonFile(
                path: "appsettings.json",
                optional: false,
                reloadOnChange: true)
            .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration); 

            serviceCollection.Configure<MongoDBSettings>(
                options => {
                    options.ConnectionURI = configuration["MongoDB:ConnectionURI"];
                    options.DatabaseName = configuration["MongoDB:DatabaseName"];
                    options.CollectionName = configuration["MongoDB:CollectionName"];
                }
            );
            
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
        private ProductService _productService;
        private ProductRepository _productRepository;

        public ProductServiceUnitTest1(DependenceInjectionProductServiceFixture serviceFixture)
        {
            var loggerMockRepository = Mock.Of<ILogger<ProductRepository>>();

            _productRepository = new ProductRepository(
                serviceFixture.GetService<IOptions<MongoDBSettings>>(),
                loggerMockRepository
            );

            _productService = new ProductService(
                _productRepository,
                serviceFixture.GetService<IMapper>()
            );
        }

        [Fact]
        public async void GetProducts_IsZeroOrMoreThanOneItem_ResultValidOperation()
        {
            //Act
            IEnumerable<ProductDTO> productsQueue = await _productService.GetProducts();
            
            //Assert
            Assert.True(productsQueue.Count() >= 0);
        }

        [Theory]
        [InlineData("651c14833fb09e2a6a583645")]
        public async void ProductUpdatedAt_IsStringEmpty_ResultInvalidOperation_WithTheUpdatedDateIsRequiredMessage(string id)
        {
            //Arrange
            ProductDTO newProduct = new ProductDTO()
            {
                Id = id,
                productUpdatedAt = ""
            };

            //Act
            Func<Task> action = () => _productService.ProductUpdatedAt(newProduct);

            //Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(action);
            
            Assert.Equal("The updated date is required.", exception.Message);
        }

        [Theory]
        [InlineData("651c14833fb09e2a6a583645")]
        public async void ProductUpdatedAt_IsStringFilled_ResultValidOperation(string id)
        {
            //Arrange
            ProductDTO newProduct = new ProductDTO()
            {
                Id = id,
                productUpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            //Act
            await _productService.ProductUpdatedAt(newProduct);

            IEnumerable<ProductDTO> productsQueue = await _productService.GetProducts();

            string productUpdatedAt = productsQueue.SingleOrDefault(x => x.Id == id).productUpdatedAt;

            //Assert
            Assert.Equal(newProduct.productUpdatedAt, productUpdatedAt);
        }

        [Theory]
        [InlineData("651c14833fb09e2a6a583645")]
        public async void ProductUpdatedAdmissionResult_IsStringEmpty_ResultInvalidOperation_WithTheUpdatedAdmissionIsRequiredMessage(string id)
        {
            //Arrange
            ProductDTO newProduct = new ProductDTO()
            {
                Id = id,
                productUpdatedAt = ""
            };

            //Act
            Func<Task> action = () => _productService.ProductUpdatedAdmissionResult(newProduct);

            //Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(action);
            
            Assert.Equal("The updated admission is required.", exception.Message);
        }

        [Theory]
        [InlineData("651c14833fb09e2a6a583645")]
        public async void ProductUpdatedAdmissionResult_IsStringFilled_ResultValidOperation(string id)
        {
            //Arrange
            ProductDTO newProduct = new ProductDTO()
            {
                Id = id,
                admissionResult = "NOK"
            };

            //Act
            await _productService.ProductUpdatedAdmissionResult(newProduct);

            IEnumerable<ProductDTO> productsQueue = await _productService.GetProducts();

            string admissionResult = productsQueue.SingleOrDefault(x => x.Id == id).admissionResult;

            //Assert
            Assert.Equal(newProduct.admissionResult, admissionResult);
        }
    }
}