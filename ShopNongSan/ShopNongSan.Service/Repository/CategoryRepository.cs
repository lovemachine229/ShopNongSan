using MongoRepo.Context;
using MongoRepo.Repository;
using ShopNongSan.Data.Database;
using ShopNongSan.Service.Interfaces.IRepository;
using ShopNongSan.Data.Collection;

namespace ShopNongSan.Service.Repository
{
    public class CategoryRepository : CommonRepository<Category>, ICategoryRepository
    {
        public CategoryRepository() : base(new ApplicationDbContext(DbConnection.ConnectionString, DbConnection.DatabaseName))
        {

        }
    }
}
