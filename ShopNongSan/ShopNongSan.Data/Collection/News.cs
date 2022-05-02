using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ShopNongSan.Data.Collection
{
    public class News
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public DateTime Created_At { get; set; }
    }
}
