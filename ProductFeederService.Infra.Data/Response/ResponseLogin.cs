namespace ProductFeederService.Infra.Data.Response
{
    public class ResponseLogin
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}