namespace ProductFeederService.Application.Interfaces
{
    public interface IExternalAPIService
    {
        Task<int> GetCategory(string name);
        Task PostCategory();
        Task<int> GetProduct(string name);
        Task PostProduct();
    }
}