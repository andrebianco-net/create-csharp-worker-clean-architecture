using AutoMapper;
using ProductFeederService.Application.DTOs;
using ProductFeederService.Application.Interfaces;
using ProductFeederService.Domain.Entities;
using ProductFeederService.Domain.Interfaces;

namespace ProductFeederService.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }        

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsEntity = await _productRepository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task UpdateProduct(ProductDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            await _productRepository.UpdateProductAsync(product);
        }
    }
}