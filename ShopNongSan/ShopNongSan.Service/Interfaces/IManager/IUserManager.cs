using MongoRepo.Interfaces.Manager;
using ShopNongSan.Data.Collection;

namespace ShopNongSan.Service.Interfaces.IManager
{
    public interface IUserManager : ICommonManager<User>
    {
        int AdminLogin(string phoneNumb, string password);

        int UserLogin(string phoneNumb, string password);

        bool isNotExist(string phoneNumb);
    }
}
