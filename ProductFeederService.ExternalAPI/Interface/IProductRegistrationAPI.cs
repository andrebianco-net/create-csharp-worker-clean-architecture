using ProductFeederService.ExternalAPI.Request;
using ProductFeederService.ExternalAPI.Response;

namespace ProductFeederService.ExternalAPI.Interface
{
    public interface IProductRegistrationAPI
    {

        Task<int> PostCategory(RequestCategory category);

        Task<IEnumerable<ResponseCategory>> GetCategories();

        Task<int> PostProduct(RequestProduct product);

        Task<IEnumerable<ResponseProduct>> GetProducts();
    }
}