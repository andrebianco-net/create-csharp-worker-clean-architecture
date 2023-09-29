using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductFeederService.Application.DTOs
{
    public class ProductDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string category { get; set; } = null;
        public string name { get; set; } = null;
        public string description { get; set; } = null;
        
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal price { get; set; } = 0;
        public int stock { get; set; } = 0;
        public string image { get; set; } = null;
        public string createdAt { get; set; } = null;
        public string productUpdatedAt { get; set; } = null;

        public string admissionResult { get; set; } = null;
    }
}