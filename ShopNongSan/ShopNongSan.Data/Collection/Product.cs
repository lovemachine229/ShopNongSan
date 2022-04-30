using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ShopNongSan.Data.Collection;
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please enter product name")]
        public string? ProductName { get; set; }

        public string? CatId { get; set; }

        public bool IsBuying { get; set; }

        public decimal Price { get; set; }

        public string? Quantity { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public int ViewCount { get; set; }

        public DateTime CreateAt { get; set; }

        public string? UserId { get; set; }

        public int Status { get; set; }

}
