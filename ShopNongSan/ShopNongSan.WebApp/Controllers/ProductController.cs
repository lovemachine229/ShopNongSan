using Microsoft.AspNetCore.Mvc;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Service.Manager;
using ShopNongSan.Data.Collection;

namespace ShopNongSan.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductManager _productManager = new ProductManager();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProductsByCatId(string id)
        {
            List<Product> products = _productManager.GetProductsByCatId(id);
            return View(products);
        }
    }
}
