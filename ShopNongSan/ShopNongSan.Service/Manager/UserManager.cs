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
                if (result.Is_Active)
                {
                    return 1; //login success
                }
                return -1; //account dissable
            }
            return 0; //login fail
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
                if (result.Is_Active)
                {
                    return 1; //login success
                }
                return -1; //account dissable
            }
            return 0; //login fail
        }
    }
}
