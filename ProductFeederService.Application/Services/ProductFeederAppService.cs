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
            var products = await _productService.GetProducts();
                
            }
            catch (System.Exception ex)
            {
                
                var m = ex.Message;
            }
            
        }
    }
}