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

                // Data from MongoDB
                IEnumerable<ProductDTO> productsQueue = await _productService.GetProducts();
                IEnumerable<ProductDTO> productsForUpdate = productsQueue.Where(x => x.productUpdatedAt == null).ToList();

                // Data from External API
                IEnumerable<CategoryAPIDTO> productsCategoryAPI = await _externalAPIService.GetCategories();
                IEnumerable<ProductAPIDTO> productsExternalAPI = await _externalAPIService.GetProducts();

                int idCategory = 0;

                // For each Product which will be updated
                foreach (ProductDTO item in productsForUpdate)
                {

                    // If it doens't exist
                    if (!productsCategoryAPI.Where(x => x.name == item.category).Any())
                    {
                        CategoryAPIDTO newCategory = new CategoryAPIDTO()
                        {
                            id = 0,
                            name = item.category
                        };

                        idCategory = await _externalAPIService.PostCategory(newCategory);
                    }
                    else
                    {
                        idCategory = productsCategoryAPI.FirstOrDefault(x => x.name == item.category).id;
                    }

                    // Serialization failure
                    if (idCategory == 0)
                    {
                        ProductDTO serializationFailure = new ProductDTO()
                        {
                            Id = item.Id,
                            admissionResult = "NOK. It was not possible to serialize Category. Maybe External API was offline."
                        };

                        await _productService.ProductUpdatedAdmissionResult(serializationFailure);

                        continue;
                    }

                    if (!productsExternalAPI.Where(x => x.name == item.name).Any())
                    {
                        ProductAPIDTO newProduct = new ProductAPIDTO()
                        {
                            id = 0,
                            name = item.name,
                            description = item.description,
                            price = item.price,
                            stock = item.stock,
                            image = "unavailable",
                            categoryId = idCategory
                        };

                        int idNewProduct = await _externalAPIService.PostProduct(newProduct);

                        // Serialization failure
                        if (idNewProduct == 0)
                        {
                            ProductDTO serializationFailure = new ProductDTO()
                            {
                                Id = item.Id,
                                admissionResult = "NOK. It was not possible to serialize Product. Maybe External API was offline."
                            };

                            await _productService.ProductUpdatedAdmissionResult(serializationFailure);

                            continue;
                        }
                        else
                        {
                            ProductDTO serializationSucess = new ProductDTO()
                            {
                                Id = item.Id,
                                productUpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                admissionResult = "OK. Serialization was realized successfully."
                            };

                            await _productService.ProductUpdatedAt(serializationSucess);
                            await _productService.ProductUpdatedAdmissionResult(serializationSucess);
                        }

                    }

                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"ProductFeederAppService -> {ex.Message}");
            }

        }
    }
}