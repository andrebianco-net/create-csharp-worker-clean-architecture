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
            if (string.IsNullOrEmpty(category.name))
            {
                throw new InvalidOperationException("The name is required.");
            }

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
            if (string.IsNullOrEmpty(product.name))
            {
                throw new InvalidOperationException("The name is required.");
            }

            if (string.IsNullOrEmpty(product.description))
            {
                throw new InvalidOperationException("The description is required.");
            }

            if (product.price <= 0)
            {
                throw new InvalidOperationException("The price is required.");
            }

            if (product.stock <= 0)
            {
                throw new InvalidOperationException("The stock is required.");
            }
            
            if (string.IsNullOrEmpty(product.image))
            {
                throw new InvalidOperationException("The image is required.");
            }
            
            if (product.categoryId <= 0)
            {
                throw new InvalidOperationException("The category is required.");
            }

            ProductAPI newProduct = _mapper.Map<ProductAPI>(product);
            return await _externalAPIRepository.PostProduct(newProduct);
        }
    }
}