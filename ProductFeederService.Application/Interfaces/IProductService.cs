using ProductFeederService.Application.DTOs;

namespace ProductFeederService.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();

        Task UpdateProduct(ProductDTO product);

    }
}