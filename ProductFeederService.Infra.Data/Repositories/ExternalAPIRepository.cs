using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductFeederService.Domain.Interfaces;
using Newtonsoft;
using Newtonsoft.Json;
using ProductFeederService.Infra.Data.Request;
using ProductFeederService.Infra.Data.Response;

namespace ProductFeederService.Infra.Data.Repositories
{
    
    public class ExternalAPIRepository : IExternalAPIRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ExternalAPIRepository> _logger;

        public ExternalAPIRepository(IConfiguration configuration,
                                     ILogger<ExternalAPIRepository> logger)
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

                    string url = _configuration["API:UrlApi"];
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

        public async Task PostCategory()
        {
            string token = await Login();
            throw new NotImplementedException();
        }

        public async Task<int> GetCategory(string name)
        {
            string token = await Login();
            throw new NotImplementedException();
        }

        public async Task PostProduct()
        {
            string token = await Login();
            throw new NotImplementedException();
        }        

        public async Task<int> GetProduct(string name)
        {
            string token = await Login();
            throw new NotImplementedException();
        }

    }
}