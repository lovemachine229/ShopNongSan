using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ShopNongSan.Data.Collection
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please enter product name")]
        public string? CategoryName { get; set; }
    }
}
