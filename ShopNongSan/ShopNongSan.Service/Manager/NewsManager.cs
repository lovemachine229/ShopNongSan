using MongoRepo.Manager;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Data.Collection;
using ShopNongSan.Service.Repository;

namespace ShopNongSan.Service.Manager
{
    public class NewsManager : CommonManager<News>, INewsManager
    {
        public NewsManager() : base(new NewsRepository())
        {

        }

        public List<News> GetNews(string sortProperty, SortOrder sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
