using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ShopNongSan.Data.Collection;
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [Display(Name = "Tên sản phẩm")]
    [Required(ErrorMessage = "Please enter product name")]
    public string? ProductName { get; set; }

    public string CategoryId { get; set; }
    [Display(Name = "Danh mục")]
    public Category Category { get; set; }

    [Display(Name = "Nhu cầu")]
    public string Demand { get; set; }

    [Display(Name = "Giá")]
    public string Price { get; set; }

    [Display(Name = "Số lượng")]
    public string? Quantity { get; set; }

    [Display(Name = "Ảnh hàng")]
    public string? Image { get; set; }

    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Display(Name = "Lượt xem")]
    public int ViewCount { get; set; } = 0;

    [Display(Name = "Ngày đăng")]
    public DateTime Created_At { get; set; }

    //[Display(Name = "SĐT người đăng")]
    //public string PhoneNumb { get; set; }
    [Display(Name = "SĐT người đăng")]
    public string PhoneNumb { get; set; }

    public User User { get; set; }

}
