namespace ProductFeederService.ExternalAPI.Response
{
    public class ResponseLogin
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}