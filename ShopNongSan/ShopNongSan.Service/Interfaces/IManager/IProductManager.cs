using MongoRepo.Interfaces.Manager;
using ShopNongSan.Data.Collection;

namespace ShopNongSan.Service.Interfaces.IManager
{
    public interface IProductManager : ICommonManager<Product>
    {
        List<Product> GetProductsByCatId(string id);
    }
}
