using ProductFeederService.Application.DTOs;

namespace ProductFeederService.Application.Interfaces
{
    public interface IProductFeederAppService
    {
        Task ProductFeederRun();
    }
}