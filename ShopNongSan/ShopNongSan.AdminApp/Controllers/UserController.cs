using Microsoft.AspNetCore.Mvc;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Service.Manager;
using ShopNongSan.Data.Collection;

namespace ShopNongSan.AdminApp.Controllers
{
    public class UserController : Controller
    {
        private IUserManager _userManager = new UserManager();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(FormCollection collection)
        {
            return View();
        }
    }
}
