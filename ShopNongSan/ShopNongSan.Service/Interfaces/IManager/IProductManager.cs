using MongoRepo.Interfaces.Manager;
using ShopNongSan.Data.Collection;

namespace ShopNongSan.Service.Interfaces.IManager
{
    public interface IProductManager : ICommonManager<Product>
    {
        List<Product> GetProductsByCatId(object id);

        List<Product> GetTop20(string demand);
    }
}
