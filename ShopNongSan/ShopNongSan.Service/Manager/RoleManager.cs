using MongoRepo.Manager;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Data.Collection;
using ShopNongSan.Service.Repository;

namespace ShopNongSan.Service.Manager
{
    public class RoleManager : CommonManager<Role>, IRoleManager
    {
        public RoleManager() : base(new RoleRepository())
        {

        }

        public List<string> getRoleList()
        {
            var roleList = GetAll();

            List<string> list = new List<string>();
            foreach (var role in roleList)
            {
                if(role.RoleName != null)
                    list.Add(role.RoleName);
            }
            return list;
        }
    }
}
