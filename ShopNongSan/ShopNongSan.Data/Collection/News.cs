using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopNongSan.Data.Collection
{
    public class News
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Hãy nhập tiêu đề")]
        [Display(Name ="Tiêu đề")]
        public string? Title { get; set; }

        [Column(TypeName = "text")]
        [Display(Name = "Ảnh mô tả")]
        public string Image { get; set; }


        [Column(TypeName = "text")]
        [Display(Name = "Nội dung")]
        public string? Content { get; set; }

        [Display(Name = "Thời gian tạo")]
        public DateTime Created_At { get; set; }
    }
}
