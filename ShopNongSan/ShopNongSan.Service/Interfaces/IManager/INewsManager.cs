using MongoRepo.Interfaces.Manager;
using ShopNongSan.Data.Collection;

namespace ShopNongSan.Service.Interfaces.IManager
{
    public interface INewsManager : ICommonManager<News>
    {
        List<News> GetNews(string sortProperty, SortOrder sortOrder);
    }
}
