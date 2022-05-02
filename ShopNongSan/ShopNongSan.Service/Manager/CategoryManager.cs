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

        public bool isNotExist(string categoryName)
        {
            var result = GetFirstOrDefault(p => p.CategoryName.Equals(categoryName));
            if (result != null)
                return false;
            else
                return true;
        }
    }
}
