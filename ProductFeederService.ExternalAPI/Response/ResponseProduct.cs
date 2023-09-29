namespace ProductFeederService.ExternalAPI.Response
{
    public class ResponseProduct
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string image { get; set; }
        public int categoryId { get; set; }
    }
}