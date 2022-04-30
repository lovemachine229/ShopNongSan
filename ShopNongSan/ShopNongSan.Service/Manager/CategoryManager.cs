using MongoRepo.Manager;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Data.Collection;
using ShopNongSan.Service.Repository;

namespace ShopNongSan.Service.Manager
{
    public class CategoryManager : CommonManager<Category>, ICategoryManager
    {
        public CategoryManager() : base(new CategoryRepository())
        {
        }
    }
}
