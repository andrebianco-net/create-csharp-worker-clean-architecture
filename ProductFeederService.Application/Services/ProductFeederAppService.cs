using ProductFeederService.Application.DTOs;
using ProductFeederService.Application.Interfaces;

namespace ProductFeederService.Application.Services
{
    public class ProductFeederAppService : IProductFeederAppService
    {

        private readonly IProductService _productService;

        public ProductFeederAppService(IProductService productService)
        {
            _productService = productService;
        }

        public async Task ProductFeederRun()
        {

            try
            {
                
                IEnumerable<ProductDTO> products = await _productService.GetProducts();
                List<ProductDTO> productsForUpdate = products.Where(x => x.productUpdatedAt == null).ToList();

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
                
            }
            
        }
    }
}