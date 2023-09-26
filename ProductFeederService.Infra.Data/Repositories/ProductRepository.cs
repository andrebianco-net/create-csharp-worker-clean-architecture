using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductFeederService.Domain.Entities;
using ProductFeederService.Domain.Interfaces;
using ProductFeederService.Infra.Data.Settings;

namespace ProductFeederService.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly IMongoCollection<Product> _productsRepository;

        public ProductRepository(IOptions<MongoDBSettings> mongoDBSettings)
        {
            
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _productsRepository = database.GetCollection<Product>(mongoDBSettings.Value.CollectionName);

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

        public async Task UpdateProductAsync(Product product)
        {
            
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", product.Id);
            
            await _productsRepository.ReplaceOneAsync(filter, product);
            
        }
    }
}