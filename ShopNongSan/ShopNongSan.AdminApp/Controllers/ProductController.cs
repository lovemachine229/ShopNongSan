using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using ShopNongSan.Data.Collection;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Service.Manager;

namespace ShopNongSan.AdminApp.Controllers
{
    [Route("admin/[controller]/[action]")]
    public class ProductController : Controller
    {
        private IProductManager _productManager = new ProductManager();
        private ICategoryManager _categoryManager = new CategoryManager();
        private IUserManager _userManager = new UserManager();

        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> products = _productManager.GetAll().ToList();


            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(string id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            List<string> list = _categoryManager.getCategoryList();
            SelectList categoryList = new SelectList(list);
            ViewBag.CategoryList = categoryList;

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product,IFormFile image)
        {
            try
            {
                List<string> list = _categoryManager.getCategoryList();
                SelectList categoryList = new SelectList(list);
                ViewBag.CategoryList = categoryList;

                //check phone numb is exist
                User userByPhoneNumb = _userManager.GetByPhoneNumb(product.PhoneNumb);

                if(userByPhoneNumb == null)
                {
                    ModelState.AddModelError("", "số điện thoại người dùng không đúng");
                    return View(product);
                }
                else
                    product.User = userByPhoneNumb;

                //assign value for variable 
                product.Id = ObjectId.GenerateNewId().ToString();
                product.Demand = Request.Form["Demand"];
                //product.Created_At = DateTime.Parse(DateTimeOffset.Now.ToString());
                product.Created_At = DateTime.Now;
                product.Category = _categoryManager.GetById(ObjectId.Parse(product.CategoryId));
                if (image != null)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    image.OpenReadStream().CopyTo(memoryStream);

                    product.Image = Convert.ToBase64String(memoryStream.ToArray());
                }
                else
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(@"C:\Users\ADMIN\OneDrive\Máy tính\Đồ án\ShopNongSan\ShopNongSan\ShopNongSan\ShopNongSan.AdminApp\wwwroot\assets\img\default-avatar.png");
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    product.Image = base64ImageRepresentation;
                }

                //save and return 
                bool isSaved = _productManager.Add(product);

                if (isSaved)
                {
                    ModelState.AddModelError("", "Tạo thành côngs");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra");
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(product);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(string id)
        {
            List<string> list = _categoryManager.getCategoryList();
            SelectList categoryList = new SelectList(list);
            ViewBag.CategoryList = categoryList;

            var product = _productManager.GetById(new ObjectId(id));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, IFormFile image)
        {
            try
            {
                List<string> list = _categoryManager.getCategoryList();
                SelectList categoryList = new SelectList(list);
                ViewBag.CategoryList = categoryList;

                Product oldProduct = _productManager.GetById(ObjectId.Parse(product.Id));
                product.User = oldProduct.User;
                product.Category = oldProduct.Category;

                if (image != null)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    image.OpenReadStream().CopyTo(memoryStream);

                    product.Image = Convert.ToBase64String(memoryStream.ToArray());
                }
                else
                {
                    string oldImage = oldProduct.Image;

                    if (oldImage == null)
                    {
                        byte[] imageArray = System.IO.File.ReadAllBytes(@"C:\Users\ADMIN\OneDrive\Máy tính\Đồ án\ShopNongSan\ShopNongSan\ShopNongSan\ShopNongSan.AdminApp\wwwroot\assets\img\no-image.png");
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                        product.Image = base64ImageRepresentation;
                    }
                    else
                        product.Image = oldImage;
                }

                bool isSaved = _productManager.Add(product);

                if (isSaved)
                {
                    ModelState.AddModelError("", "Cập nhật thành công");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirnm(string id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
