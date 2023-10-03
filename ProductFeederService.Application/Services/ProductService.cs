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

        public async Task ProductUpdatedAt(ProductDTO productDTO)
        {
            if (string.IsNullOrEmpty(productDTO.productUpdatedAt))
            {
                throw new InvalidOperationException("The updated date is required.");
            }

            Product product = _mapper.Map<Product>(productDTO);
            await _productRepository.ProductUpdatedAt(product);
        }

        public async Task ProductUpdatedAdmissionResult(ProductDTO productDTO)
        {
            if (string.IsNullOrEmpty(productDTO.admissionResult))
            {
                throw new InvalidOperationException("The updated admission is required.");
            }

            Product product = _mapper.Map<Product>(productDTO);
            await _productRepository.ProductUpdatedAdmissionResult(product);
        }
    }
}