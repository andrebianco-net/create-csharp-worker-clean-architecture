using ProductFeederService.Application.DTOs;

namespace ProductFeederService.Application.Interfaces
{
    public interface IExternalAPIService
    {
        Task<IEnumerable<CategoryAPIDTO>> GetCategories();
        Task<int> PostCategory(CategoryAPIDTO category);
        Task<IEnumerable<ProductAPIDTO>> GetProducts();
        Task<int> PostProduct(ProductAPIDTO product);
    }
}