namespace ProductFeederService.Application.DTOs
{
    public class ProductDTO
    {
        public string? Id { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string image { get; set; }
        public string createdAt { get; set; }
    }
}