using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductFeederService.Application.Services;
using ProductFeederService.Domain.Interfaces;
using ProductFeederService.Infra.Data.Repositories;
using Moq;
using ProductFeederService.Application.Mappings;
using ProductFeederService.ExternalAPI.Interface;
using ProductFeederService.ExternalAPI;
using Microsoft.Extensions.Configuration;
using ProductFeederService.Application.DTOs;


namespace ProductFeederService.Service.Tests
{
    public class DependenceInjectionExternalAPIFixture
    {

        private readonly IServiceScope _scope;

        public DependenceInjectionExternalAPIFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IExternalAPIRepository, ExternalAPIRepository>();
            serviceCollection.AddTransient<IProductRegistrationAPI, ProductRegistrationAPI>();
            serviceCollection.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            var configuration = new ConfigurationBuilder()
            .SetBasePath($"{Directory.GetCurrentDirectory()}/../../../")
            .AddJsonFile(
                path: "appsettings.json",
                optional: false,
                reloadOnChange: true)
            .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _scope = serviceProvider.CreateScope();
        }

        public T GetService<T>()
        {
            return _scope.ServiceProvider.GetRequiredService<T>();
        }
    }

    public class ExternalAPIServiceUnitTest1 : IClassFixture<DependenceInjectionExternalAPIFixture>
    {
        private ExternalAPIService _apiService;
        private ExternalAPIRepository _apiRepository;
        private ProductRegistrationAPI  _apiRegistrationAPI;

        public ExternalAPIServiceUnitTest1(DependenceInjectionExternalAPIFixture serviceFixture)
        {

            var loggerMockRegistrationAPI = Mock.Of<ILogger<ProductRegistrationAPI>>();

            _apiRegistrationAPI = new ProductRegistrationAPI(
                serviceFixture.GetService<IConfiguration>(),
                loggerMockRegistrationAPI
            );            

            var loggerMockRepository = Mock.Of<ILogger<ExternalAPIRepository>>();

            _apiRepository = new ExternalAPIRepository(
                _apiRegistrationAPI,
                serviceFixture.GetService<IMapper>(),
                loggerMockRepository
            );

            var loggerMockService = Mock.Of<ILogger<ExternalAPIService>>();

            _apiService = new ExternalAPIService(
                loggerMockService,
                serviceFixture.GetService<IMapper>(),
                _apiRepository
            );
        }

        [Fact]
        public async void GetCategories_IsZeroOrMoreThanOneItem_ResultValidOperation()
        {
            IEnumerable<CategoryAPIDTO> categoriesForValidation = (IEnumerable<CategoryAPIDTO>) await _apiService.GetCategories();

            Assert.True(categoriesForValidation.Count() >= 0);
        }

        [Fact]
        public async void PostCategory_WithInvalidName_ResultInvalidOperation_WithTheNameIsRequiredMessage()
        {
            //Arrange
            CategoryAPIDTO newCategory = new CategoryAPIDTO() 
            {
                id = 0,
                name = ""
            };

            //Act
            Func<Task> action = () => _apiService.PostCategory(newCategory);
            
            //assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(action);
            
            Assert.Equal("The name is required.", exception.Message);
        }

        [Fact]
        public async void GetProducts_IsZeroOrMoreThanOneItem_ResultValidOperation()
        {
            IEnumerable<ProductAPIDTO> productsForValidation = (IEnumerable<ProductAPIDTO>) await _apiService.GetProducts();

            Assert.True(productsForValidation.Count() >= 0);
        }

        [Fact]
        public async void PostProduct_WithInvalidName_ResultInvalidOperation_WithTheNameIsRequiredMessage()
        {
            //Arrange
            ProductAPIDTO newProduct = new ProductAPIDTO() 
            {
                id = 0,
                name = ""
            };

            //Act
            Func<Task> action = () => _apiService.PostProduct(newProduct);
            
            //Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(action);
            
            Assert.Equal("The name is required.", exception.Message);
        }

        [Fact]
        public async void PostProduct_WithInvalidName_ResultInvalidOperation_WithTheDescriptionIsRequiredMessage()
        {
            //Arrange
            ProductAPIDTO newProduct = new ProductAPIDTO() 
            {
                id = 0,
                name = "Product 1",
                description = ""
            };

            //Act
            Func<Task> action = () => _apiService.PostProduct(newProduct);
            
            //Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(action);
            
            Assert.Equal("The description is required.", exception.Message);
        }

        [Fact]
        public async void PostProduct_WithInvalidName_ResultInvalidOperation_WithThePriceIsRequiredMessage()
        {
            //Arrange
            ProductAPIDTO newProduct = new ProductAPIDTO() 
            {
                id = 0,
                name = "Name",
                description = "Description",
                price = 0
            };

            //Act
            Func<Task> action = () => _apiService.PostProduct(newProduct);
            
            //Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(action);
            
            Assert.Equal("The price is required.", exception.Message);
        }

        [Fact]
        public async void PostProduct_WithInvalidName_ResultInvalidOperation_WithTheStockIsRequiredMessage()
        {
            //Arrange
            ProductAPIDTO newProduct = new ProductAPIDTO() 
            {
                id = 0,
                name = "Name",
                description = "Description",
                price = 1,
                stock = 0
            };

            //Act
            Func<Task> action = () => _apiService.PostProduct(newProduct);
            
            //Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(action);
            
            Assert.Equal("The stock is required.", exception.Message);
        }

        [Fact]
        public async void PostProduct_WithInvalidName_ResultInvalidOperation_WithTheImageIsRequiredMessage()
        {
            //Arrange
            ProductAPIDTO newProduct = new ProductAPIDTO() 
            {
                id = 0,
                name = "Name",
                description = "Description",
                price = 1,
                stock = 1,
                image = ""
            };

            //Act
            Func<Task> action = () => _apiService.PostProduct(newProduct);
            
            //Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(action);
            
            Assert.Equal("The image is required.", exception.Message);
        }

        [Fact]
        public async void PostProduct_WithInvalidName_ResultInvalidOperation_WithTheCategoryIsRequiredMessage()
        {
            //Arrange
            ProductAPIDTO newProduct = new ProductAPIDTO() 
            {
                id = 0,
                name = "Name",
                description = "Description",
                price = 1,
                stock = 1,
                image = "unavailable",
                categoryId = 0
            };

            //Act
            Func<Task> action = () => _apiService.PostProduct(newProduct);
            
            //Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(action);
            
            Assert.Equal("The category is required.", exception.Message);
        }

        [Fact]
        public async void PostProduct_WithInvalidName_ResultValidOperation()
        {
            //Arrange
            ProductAPIDTO newProduct = new ProductAPIDTO() 
            {
                id = 0,
                name = "Name",
                description = "Description ABC",
                price = 1,
                stock = 1,
                image = "unavailable",
                categoryId = 2
            };

            //Act
            await _apiService.PostProduct(newProduct);
            IEnumerable<ProductAPIDTO> productsForValidation = (IEnumerable<ProductAPIDTO>) await _apiService.GetProducts();
            
            ProductAPIDTO productAPIDTO = productsForValidation.FirstOrDefault(x => x.description == "Description ABC");
            
            //Assert
            Assert.Equal(newProduct.description, productAPIDTO.description);
        }
    }
}