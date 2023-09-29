using Microsoft.Extensions.Logging;
using ProductFeederService.Domain.Entities;
using ProductFeederService.Domain.Interfaces;
using ProductFeederService.ExternalAPI.Interface;
using AutoMapper;
using ProductFeederService.ExternalAPI.Request;

namespace ProductFeederService.Infra.Data.Repositories
{
    
    public class ExternalAPIRepository : IExternalAPIRepository
    {
        private readonly IProductRegistrationAPI _productRegistrationAPI;
        private readonly IMapper _mapper;
        private readonly ILogger<ExternalAPIRepository> _logger;

        public ExternalAPIRepository(IProductRegistrationAPI productRegistrationAPI,
                                     IMapper mapper,
                                     ILogger<ExternalAPIRepository> logger)
        {
            _productRegistrationAPI = productRegistrationAPI;
            _mapper = mapper;
            _logger = logger;
        }
 
        public async Task<int> PostCategory(CategoryAPI category)
        {
            RequestCategory newCategory = _mapper.Map<RequestCategory>(category);
            return await _productRegistrationAPI.PostCategory(newCategory);
        }

        public async Task<IEnumerable<CategoryAPI>> GetCategories()
        {
            var categories = await _productRegistrationAPI.GetCategories();
            return _mapper.Map<IEnumerable<CategoryAPI>>(categories);
        }

        public async Task<int> PostProduct(ProductAPI product)
        {
            RequestProduct newProduct = _mapper.Map<RequestProduct>(product);
            return await _productRegistrationAPI.PostProduct(newProduct);
        }        

        public async Task<IEnumerable<ProductAPI>> GetProducts()
        {
            var products = await _productRegistrationAPI.GetProducts();
            return _mapper.Map<IEnumerable<ProductAPI>>(products);
        }

    }
}