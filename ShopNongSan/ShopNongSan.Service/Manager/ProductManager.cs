using MongoRepo.Manager;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Data.Collection;
using ShopNongSan.Service.Repository;

namespace ShopNongSan.Service.Manager
{
    public class ProductManager : CommonManager<Product>, IProductManager
    {
        public ProductManager(): base(new ProductRepository())
        {
            
        }

        public List<Product> GetProductsByCatId(string id)
        {
            List<Product> products = GetAll(p => p.Id.Equals(id)).ToList();

            return products;

        }
    }
}
