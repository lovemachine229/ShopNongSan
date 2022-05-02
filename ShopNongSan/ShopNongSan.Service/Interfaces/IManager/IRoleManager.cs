using MongoRepo.Interfaces.Manager;
using ShopNongSan.Data.Collection;

namespace ShopNongSan.Service.Interfaces.IManager
{
    public interface IRoleManager : ICommonManager<Role>
    {
        List<string> getRoleList();
    }
}
