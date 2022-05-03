using Microsoft.AspNetCore.Mvc;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Service.Manager;
using ShopNongSan.Data.Collection;
using ShopNongSan.Data.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopNongSan.AdminApp.Controllers
{
    [Route("admin/[controller]/[action]")]
    public class UserController : Controller
    {
        private IUserManager _userManager = new UserManager();
        private IRoleManager _roleManager = new RoleManager();

        
        public ActionResult Index(string sortOrder, string searchString,
            string currentFilter, int pg=1)
        {

            ViewBag.CurrentSort = sortOrder; //Biến lấy yêu cầu sắp xếp hiện tại

            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            //Lấy giá trị của bộ lọc hiên tại
            if (searchString != null)
            {
                pg = 1; //Trang đầu tiên
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            //Lấy danh sách user
            var users = _userManager.GetAll().ToList();

            //Lọc theo tên user
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.FindAll(p => p.Name.ToLower().Contains(searchString));
            }
            //Sắp xếp
            switch (sortOrder)
            {
                case "ten_desc":
                    users = users.OrderByDescending(p => p.Name).ToList();
                    break;
                default:
                    users = users.OrderBy(s => s.Name).ToList();
                    break;
            }

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = users.Count;

            var pager = new Pager(recsCount,pg, pageSize);

            int recSkip = (pg - 1) * pageSize;
            
            var data = users.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<string> list = _roleManager.getRoleList();
            SelectList roleList = new SelectList(list);
            ViewBag.RoleList = roleList;

            return View();
        }


        [HttpPost]
        public ActionResult Create(User user, IFormFile avatar)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.isNotExist(user.PhoneNumb))
                {
                    user.Id = ObjectId.GenerateNewId().ToString();
                    user.Gender = Request.Form["Gender"];
                    user.Created_At = DateTime.Parse(DateTimeOffset.Now.ToString());

                    byte[] imageArray = System.IO.File.ReadAllBytes(@"C:\Users\ADMIN\OneDrive\Máy tính\Đồ án\ShopNongSan\ShopNongSan\ShopNongSan\ShopNongSan.AdminApp\wwwroot\assets\img\default-avatar.png");
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);


                    if (avatar != null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        avatar.OpenReadStream().CopyTo(memoryStream);

                        user.Avatar = Convert.ToBase64String(memoryStream.ToArray());
                    }
                    else
                    {
                        user.Avatar = base64ImageRepresentation;
                    }

                    bool isSaved = _userManager.Add(user);

                    if (isSaved)
                    {
                        ModelState.AddModelError("", "Create Success");
                        return RedirectToAction(nameof(Index));
                    }
                    else 
                    {
                        ModelState.AddModelError("", "Create Fail");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Số điện thoại đã được đăng ký");
                }
            }
            List<string> list = _roleManager.getRoleList();
            SelectList roleList = new SelectList(list);
            ViewBag.RoleList = roleList;

            return View(user);
        }

        public ActionResult Edit(string? id)
        {
            var user = _userManager.GetById(ObjectId.Parse(id));
            List<string> list = _roleManager.getRoleList();
            SelectList roleList = new SelectList(list);
            
            ViewBag.RoleList = roleList;

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = _userManager.Update(ObjectId.Parse(user.Id), user);

                if (isUpdated)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi cập nhật");
                }
            }

            return View(user);
        }

        public ActionResult Details(string id)
        {
            var result = _userManager.GetById(ObjectId.Parse(id));

            return View(result);
        }

        public ActionResult Delete(string id)
        {
            var result = _userManager.GetById(ObjectId.Parse(id));

            return View(result);
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(string Id)
        {
            if (ModelState.IsValid)
            {

                bool isDeleted = _userManager.Delete(ObjectId.Parse(Id));

                if (isDeleted)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Xóa thất bại");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost, ActionName("Login")]
        public ActionResult Login(LoginViewModel loginVM)
        {

            int login = _userManager.AdminLogin(loginVM.PhoneNumb, loginVM.Password);

            if(login == 1)
            {
                return RedirectToAction("Index", "AdminHome");
            }

            if(login == -1)
            {
                ModelState.AddModelError("","Tài khoản của bạn đã bị khóa, vui lòng liên hệ admin");
            }

            if(login == 0)
            {
                ModelState.AddModelError("", "Số điện thoại hoặc mật khẩu không khớp với bất kỳ tài khoản nào, vui lòng kiểm tra lại");
            }

            return View();
        }
    }
}
