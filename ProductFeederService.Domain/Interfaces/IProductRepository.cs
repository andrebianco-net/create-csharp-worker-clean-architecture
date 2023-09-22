using ProductFeederService.Domain.Entities;

namespace ProductFeederService.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}