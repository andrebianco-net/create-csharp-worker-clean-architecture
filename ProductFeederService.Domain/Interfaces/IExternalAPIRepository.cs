namespace ProductFeederService.Domain.Interfaces
{
    public interface IExternalAPIRepository
    {
        Task<int> GetCategory(string name);
        Task PostCategory();
        Task<int> GetProduct(string name);
        Task PostProduct();
    }
}