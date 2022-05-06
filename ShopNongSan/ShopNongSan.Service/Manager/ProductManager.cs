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

        public List<Product> GetProductsByCatId(object id)
        {
            List<Product> products = GetAll(p => p.Id.Equals(id)).ToList();

            return products;

        }

        public List<Product> GetTop20(string demand)
        {
            List<Product> products = GetAll(p => p.Demand.Equals(demand)).Take(20).ToList();
            return products;
        }
    }
}
