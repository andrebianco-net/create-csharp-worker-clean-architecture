using ProductFeederService.Application.DTOs;

namespace ProductFeederService.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();

        Task ProductUpdatedAt(ProductDTO product);

        Task ProductUpdatedAdmissionResult(ProductDTO productDTO);
    }
}