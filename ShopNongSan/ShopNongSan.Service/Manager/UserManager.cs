using MongoRepo.Manager;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Data.Collection;
using ShopNongSan.Service.Repository;

namespace ShopNongSan.Service.Manager
{
    public class UserManager : CommonManager<User>, IUserManager
    {
        public UserManager() : base(new UserRepository())
        {
            
        }

        public int AdminLogin(string phoneNumb, string password)
        {
            var result =  GetFirstOrDefault(p => p.PhoneNumb.Equals(phoneNumb) && p.Password.Equals(password) && p.Role.Equals("admin"));

            if(result != null)
            {
                if (result.Is_Active)
                {
                    return 1; //login success
                }
                return -1; //account dissable
            }
            return 0; //login fail
        }

        private List<User> DoSort (List<User> users, string sortProperty, SortOrder sortOrder)
        {

            switch (sortProperty.ToLower())
            {
                case "name":
                    switch (sortOrder)
                    {
                        case SortOrder.Ascending:
                            users = users.OrderBy(p => p.Name).ToList();
                            break;
                        default:
                            users = users.OrderByDescending(p => p.Name).ToList();
                            break;
                    }
                    break;
                case "time":
                    switch (sortOrder)
                    {
                        case SortOrder.Ascending:
                            users = users.OrderBy(s => s.Created_At).ToList();
                            break;
                        default:
                            users = users.OrderByDescending(s => s.Created_At).ToList();
                            break;
                    }
                    break;
            }

            return users;
        }

        public List<User> GetUsers(string sortProperty, SortOrder sortOrder, string searchText="")
        {
            List<User> users = GetAll().ToList();

            if (searchText != "" && searchText != null)
            {
                users = GetAll().Where(p => p.Name.ToLower().Contains(searchText.ToLower())).ToList();
            }
            else users = GetAll().ToList();

            users = DoSort(users, sortProperty, sortOrder);

            return users;
        }

        public bool isNotExist(string phoneNumb)
        {
            var result = GetFirstOrDefault(p => p.PhoneNumb.Equals(phoneNumb));
            if( result != null)
                return false;
            else 
                return true;
        }

        public int UserLogin(string phoneNumb, string password)
        {
            var result = GetFirstOrDefault(p => p.PhoneNumb.Equals(phoneNumb) && p.Password.Equals(password) && p.Role.Equals("user"));

            if (result != null)
            {
                if (result.Is_Active)
                {
                    return 1; //login success
                }
                return -1; //account dissable
            }
            return 0; //login fail
        }

        public User GetByPhoneNumb(string phoneNumb)
        {
            User user = GetFirstOrDefault(p => p.PhoneNumb.Equals(phoneNumb));

            return user;
        }
    }
}
