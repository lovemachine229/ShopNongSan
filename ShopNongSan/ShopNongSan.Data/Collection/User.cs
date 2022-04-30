using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ShopNongSan.Data.Collection
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Điện thoại")]
        [StringLength(13)]
        [Phone(ErrorMessage = "Vui lòng nhập đúng định dạng")]
        public string PhoneNumb { get; set; }

        [Required]
        [Display(Name = "Mật khẩu")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Họ tên")]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Địa chỉ")]
        [StringLength(200)]
        public string? Address { get; set; }

        [Display(Name = "Email")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng")]
        public string? Email { get; set; }

        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }

        public string? Role { get; set; }

        public bool Is_Active { get; set; }
    }
}
