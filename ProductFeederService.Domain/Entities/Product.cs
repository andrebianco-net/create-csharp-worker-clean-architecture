namespace ProductFeederService.Domain.Entities
{
    public class Product
    {
        public int _id { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string image { get; set; }
        public DateTime createdAt { get; set; }
    }
}