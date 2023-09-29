using Microsoft.Extensions.Logging;
using ProductFeederService.Application.DTOs;
using ProductFeederService.Application.Interfaces;
using ProductFeederService.Domain.Entities;
using ProductFeederService.Domain.Interfaces;
using AutoMapper;

namespace ProductFeederService.Application.Services
{
    public class ExternalAPIService : IExternalAPIService
    {
        private readonly IExternalAPIRepository _externalAPIRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ExternalAPIService> _logger;

        public ExternalAPIService(ILogger<ExternalAPIService> logger,
                                  IMapper mapper,
                                  IExternalAPIRepository externalAPIRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _externalAPIRepository = externalAPIRepository;
        }

        public async Task<IEnumerable<CategoryAPIDTO>> GetCategories()
        {
            var categories = await _externalAPIRepository.GetCategories();
            return _mapper.Map<IEnumerable<CategoryAPIDTO>>(categories);
        }

        public async Task<int> PostCategory(CategoryAPIDTO category)
        {
            CategoryAPI newCategory = _mapper.Map<CategoryAPI>(category);
            return await _externalAPIRepository.PostCategory(newCategory);
        }

        public async Task<IEnumerable<ProductAPIDTO>> GetProducts()
        {
            var products = await _externalAPIRepository.GetProducts();
            return _mapper.Map<IEnumerable<ProductAPIDTO>>(products);
        }

        public async Task<int> PostProduct(ProductAPIDTO product)
        {
            ProductAPI newProduct = _mapper.Map<ProductAPI>(product);
            return await _externalAPIRepository.PostProduct(newProduct);
        }
    }
}