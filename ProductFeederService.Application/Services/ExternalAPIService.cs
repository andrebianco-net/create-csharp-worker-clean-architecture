using Microsoft.Extensions.Logging;
using ProductFeederService.Application.Interfaces;
using ProductFeederService.Domain.Interfaces;

namespace ProductFeederService.Application.Services
{
    public class ExternalAPIService : IExternalAPIService
    {
        private readonly IExternalAPIRepository _externalAPIRepository;
        private readonly ILogger<ExternalAPIService> _logger;

        public ExternalAPIService(ILogger<ExternalAPIService> logger,
                                  IExternalAPIRepository externalAPIRepository)
        {
            _logger = logger;
            _externalAPIRepository = externalAPIRepository;
        }

        public async Task<int> GetCategory(string name)
        {
            throw new NotImplementedException();
        }

        public async Task PostCategory()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetProduct(string name)
        {
            throw new NotImplementedException();
        }

        public async Task PostProduct()
        {
            throw new NotImplementedException();
        }
    }
}