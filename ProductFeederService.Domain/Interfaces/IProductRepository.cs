using ProductFeederService.Domain.Entities;

namespace ProductFeederService.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product);        
        Task<IEnumerable<Product>> GetProductsAsync();
        Task ProductUpdatedAt(Product product);
        Task DeleteProductAsync(Product product);
    }
}