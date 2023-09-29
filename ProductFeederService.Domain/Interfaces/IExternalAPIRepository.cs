using ProductFeederService.Domain.Entities;

namespace ProductFeederService.Domain.Interfaces
{
    public interface IExternalAPIRepository
    {
        Task<IEnumerable<CategoryAPI>> GetCategories();
        Task<int> PostCategory(CategoryAPI category);
        Task<IEnumerable<ProductAPI>> GetProducts();
        Task<int> PostProduct(ProductAPI product);
    }
}