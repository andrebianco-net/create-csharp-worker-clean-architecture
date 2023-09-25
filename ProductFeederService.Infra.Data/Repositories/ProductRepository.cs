using Microsoft.EntityFrameworkCore;
using ProductFeederService.Domain.Entities;
using ProductFeederService.Domain.Interfaces;
using ProductFeederService.Infra.Data.Context;

namespace ProductFeederService.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        ApplicationDbContext _productRepository;

        public ProductRepository(ApplicationDbContext context)
        {
            _productRepository = context;
        }
                
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.Products.ToListAsync();
        }

    }
}