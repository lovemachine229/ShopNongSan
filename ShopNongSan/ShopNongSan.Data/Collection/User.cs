using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ShopNongSan.Data.Collection
{

    public enum SortOrder { Ascending=0, Descending=1}
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [Display(Name = "Điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]

        public string PhoneNumb { get; set; }

        [Required]
        [Display(Name = "Mật khẩu")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Họ tên")]
        [StringLength(50)]
        public string? Name { get; set; }

        [Display(Name ="Ảnh đại diện")]
        public string? Avatar { get; set; }

        [Required]
        [Display(Name = "Địa chỉ")]
        [StringLength(200)]
        public string? Address { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "Lượng sp đã đăng")]
        public int PostCount { get; set; }

        [Display(Name = "Vai trò")]
        public string? Role { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool Is_Active { get; set; }

        [Display(Name = "Thời gian tạo")]
        public DateTime Created_At { get; set; }
    }
}
