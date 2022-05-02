using MongoRepo.Context;
using MongoRepo.Repository;
using ShopNongSan.Data.Database;
using ShopNongSan.Service.Interfaces.IRepository;
using ShopNongSan.Data.Collection;

namespace ShopNongSan.Service.Repository
{
    public class RoleRepository : CommonRepository<Role>, IRoleRepository
    {
        public RoleRepository() : base(new ApplicationDbContext(DbConnection.ConnectionString, DbConnection.DatabaseName))
        {

        }
    }
}
