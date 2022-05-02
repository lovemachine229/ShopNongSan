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

        public int AdminLogin(string phoneNumb, string password)
        {
            var result =  GetFirstOrDefault(p => p.PhoneNumb.Equals(phoneNumb) && p.Password.Equals(password) && p.Role.Equals("admin"));

            if(result != null)
            {
                return 1;
            }
            return 0;
        }

        public bool isNotExist(string phoneNumb)
        {
            var result = GetFirstOrDefault(p => p.PhoneNumb.Equals(phoneNumb));
            if( result != null)
                return false;
            else 
                return true;
        }

        public int UserLogin(string phoneNumb, string password)
        {
            var result = GetFirstOrDefault(p => p.PhoneNumb.Equals(phoneNumb) && p.Password.Equals(password) && p.Role.Equals("user"));

            if (result != null)
            {
                return 1;
            }
            return 0;
        }
    }
}
