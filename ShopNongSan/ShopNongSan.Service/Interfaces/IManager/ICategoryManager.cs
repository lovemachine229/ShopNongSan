using MongoRepo.Interfaces.Manager;
using ShopNongSan.Data.Collection;

namespace ShopNongSan.Service.Interfaces.IManager
{
    public interface ICategoryManager : ICommonManager<Category>
    {
        bool isNotExist(string categoryName);

        List<string> getCategoryList();
    }
}
