using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductFeederService.Domain.Entities;
using ProductFeederService.Domain.Interfaces;
using ProductFeederService.Infra.Data.Context;

namespace ProductFeederService.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly IMongoCollection<Product> _productsRepository;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IOptions<MongoDBSettings> mongoDBSettings,
                                 ILogger<ProductRepository> logger)
        {
            _logger = logger;

            try
            {

                MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
                IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
                _productsRepository = database.GetCollection<Product>(mongoDBSettings.Value.CollectionName);

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"ProductRepository -> {ex.Message}");
            }
        }

        public async Task CreateProductAsync(Product product)
        {
            await _productsRepository.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(Product product)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", product.Id);
            await _productsRepository.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productsRepository.Find(new BsonDocument()).ToListAsync();
        }

        public async Task ProductUpdatedAt(Product product)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", product.Id);

            var update = Builders<Product>.Update
                            .Set(p => p.productUpdatedAt, product.productUpdatedAt);

            await _productsRepository.UpdateOneAsync(filter, update);
        }
    }
}