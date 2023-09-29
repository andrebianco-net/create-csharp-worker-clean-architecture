using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ProductFeederService.ExternalAPI.Request;
using ProductFeederService.ExternalAPI.Response;
using ProductFeederService.ExternalAPI.Interface;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Net.Http.Headers;

namespace ProductFeederService.ExternalAPI
{
    public class ProductRegistrationAPI : IProductRegistrationAPI
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductRegistrationAPI> _logger;

        public ProductRegistrationAPI(IConfiguration configuration,
                                     ILogger<ProductRegistrationAPI> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        
        private async Task<string> Login()
        {
            string token = string.Empty;

            try
            {

                RequestLogin requestLogin = new RequestLogin() {
                    email = _configuration["API:User"],
                    password = _configuration["API:Password"]
                };

                using (HttpClient client = new HttpClient())
                {

                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    string url = _configuration["API:UrlLoginUser"];
                    StringContent content = new StringContent(JsonConvert.SerializeObject(requestLogin), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    
                    if (response != null)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        ResponseLogin responseLogin = JsonConvert.DeserializeObject<ResponseLogin>(jsonString);
                        token = responseLogin.token;
                    }

                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"ExternalAPIRepository -> {ex.Message}");
            }

            return token;
        }

        public async Task<IEnumerable<ResponseCategory>> GetCategories()
        {            
            IEnumerable<ResponseCategory> categories = new List<ResponseCategory>();

            string token = await Login();

            try
            {

                using (HttpClient client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    string url = _configuration["API:UrlCategories"];
                    var response = await client.GetAsync(url);
                    
                    if (response != null)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        var responseCategories = JsonConvert.DeserializeObject<IEnumerable<ResponseCategory>>(jsonString);
                        categories = (IEnumerable<ResponseCategory>) responseCategories;
                    }

                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"ExternalAPIRepository -> {ex.Message}");
            }

            return categories;
        }

        public async Task<int> PostCategory(RequestCategory category)
        {
            int idNewCategory = 0;
            
            string token = await Login();

            try
            {

                using (HttpClient client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    string url = _configuration["API:UrlCategories"];
                    StringContent content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    
                    if (response != null)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        var responseCategory = JsonConvert.DeserializeObject<ResponseCategory>(jsonString);
                        ResponseCategory newCategory = (ResponseCategory) responseCategory;
                        idNewCategory = newCategory.id;
                    }

                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"ExternalAPIRepository -> {ex.Message}");
            }          

            return idNewCategory;  
        }

        public async Task<IEnumerable<ResponseProduct>> GetProducts()
        { 
            IEnumerable<ResponseProduct> products = new List<ResponseProduct>();

            string token = await Login();

            try
            {

                using (HttpClient client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    string url = _configuration["API:UrlProducts"];
                    var response = await client.GetAsync(url);
                    
                    if (response != null)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        var responseProducts = JsonConvert.DeserializeObject<IEnumerable<ResponseProduct>>(jsonString);
                        products = (IEnumerable<ResponseProduct>) responseProducts;
                    }

                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"ExternalAPIRepository -> {ex.Message}");
            }

            return products;
        }

        public async Task<int> PostProduct(RequestProduct product)
        {
            int idNewProduct = 0;
            
            string token = await Login();

            try
            {

                using (HttpClient client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    string url = _configuration["API:UrlProducts"];
                    StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    
                    if (response != null)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        var responseProduct = JsonConvert.DeserializeObject<ResponseProduct>(jsonString);
                        ResponseProduct newProduct = (ResponseProduct) responseProduct;
                        idNewProduct = newProduct.id;
                    }

                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError($"ExternalAPIRepository -> {ex.Message}");
            }          

            return idNewProduct;  
        }
    }
}