using MongoRepo.Manager;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Data.Collection;
using ShopNongSan.Service.Repository;

namespace ShopNongSan.Service.Manager
{
    public class UserManager : CommonManager<User>, IUserManager
    {
        public UserManager() : base(new UserRepository())
        {
            
        }

        public int Login(string phoneNumb, string password)
        {
            var result =  GetFirstOrDefault(p => p.PhoneNumb.Equals(phoneNumb) && p.Password.Equals(password));

            if(result != null)
            {
                if (result.Role.Equals("admin"))
                    return 2; //admin
                else if(result.Role.Equals("user"))
                    return 1; //user
                else return 0;
            }
            return 0;
        }
    }
}
