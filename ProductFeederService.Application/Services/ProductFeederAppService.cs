using Microsoft.Extensions.Logging;
using ProductFeederService.Application.DTOs;
using ProductFeederService.Application.Interfaces;

namespace ProductFeederService.Application.Services
{
    public class ProductFeederAppService : IProductFeederAppService
    {

        private readonly IProductService _productService;
        private readonly IExternalAPIService _externalAPIService;
        private readonly ILogger<ProductFeederAppService> _logger;        

        public ProductFeederAppService(IProductService productService,
                                       IExternalAPIService externalAPIService,
                                       ILogger<ProductFeederAppService> logger)
        {
            _productService = productService;
            _externalAPIService = externalAPIService;
            _logger = logger;
        }

        public async Task ProductFeederRun()
        {

            try
            {
                
                IEnumerable<ProductDTO> productsQueue = await _productService.GetProducts();
                IEnumerable<ProductDTO> productsForUpdate = productsQueue.Where(x => x.productUpdatedAt == null).ToList();


                CategoryAPIDTO newCategory = new CategoryAPIDTO()
                {
                    id = 0,
                    name = "TestNOW3"
                };

                int idNewCategory = await _externalAPIService.PostCategory(newCategory);

                IEnumerable<CategoryAPIDTO> categoriesForValidation = (IEnumerable<CategoryAPIDTO>) await _externalAPIService.GetCategories();

                ProductAPIDTO newProduct = new ProductAPIDTO()
                {
                    id = 0,
                    name = "Product 1",
                    description = "Product 1",
                    price = 1,
                    stock = 1,
                    image = "unavailable",
                    categoryId = 1020
                };

                int idNewProduct = await _externalAPIService.PostProduct(newProduct);

                IEnumerable<ProductAPIDTO> productsAPIForValidation = (IEnumerable<ProductAPIDTO>) await _externalAPIService.GetProducts();

                //Test
                
                // decimal search = decimal.Parse("50.0");
                // if(productsForUpdate.Where(x => x.price == search).Any())
                // {
                //     ProductDTO product = productsForUpdate.SingleOrDefault(x => x.price == search);
                //     if(product != null)
                //     {
                //         product.productUpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //         product.price = decimal.Parse("55.10");
                //         await _productService.UpdateProduct(product);
                //     }
                // }
                
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"ProductFeederAppService -> {ex.Message}");
            }
            
        }
    }
}