using Microsoft.AspNetCore.Mvc;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Service.Manager;
using ShopNongSan.Data.Collection;
using ShopNongSan.Data.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopNongSan.Service.Tools;

namespace ShopNongSan.AdminApp.Controllers
{
    [Route("admin/[controller]/[action]")]
    public class UserController : Controller
    {
        private IUserManager _userManager = new UserManager();
        private IRoleManager _roleManager = new RoleManager();

        
        
        
        public ActionResult Index(string sortExpression="", string searchText="",int pg = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();

            sortModel.AddColumn("name");
            sortModel.AddColumn("time");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            ViewBag.SearchText = searchText;

            List<User> users = _userManager.GetUsers(sortModel.SortedProperty, sortModel.SortedOrder, searchText);

            var pager = new PagerModel(users.Count, pg, pageSize);
            pager.SortExpression = sortExpression;

            this.ViewBag.Pager = pager;


            return View(users);
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
            
                if (_userManager.isNotExist(user.PhoneNumb))
                {
                    user.Id = ObjectId.GenerateNewId().ToString();
                    user.Gender = Request.Form["Gender"];
                    //user.Created_At = DateTime.Parse(DateTimeOffset.Now.ToString());
                    user.Created_At = DateTime.Now;

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
        public ActionResult Edit(User user, IFormFile avatar)
        {
            try
            {
                User oldeUser = _userManager.GetById(ObjectId.Parse(user.Id));
                if (avatar != null)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    avatar.OpenReadStream().CopyTo(memoryStream);

                    user.Avatar = Convert.ToBase64String(memoryStream.ToArray());
                }
                else
                {
                    string oldImage = oldeUser.Avatar;

                    if (oldImage == null)
                    {
                        byte[] imageArray = System.IO.File.ReadAllBytes(@"C:\Users\ADMIN\OneDrive\Máy tính\Đồ án\ShopNongSan\ShopNongSan\ShopNongSan\ShopNongSan.AdminApp\wwwroot\assets\img\no-image.png");
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                        user.Avatar = base64ImageRepresentation;
                    }
                    else
                        user.Avatar = oldImage;
                }

                bool isUpdated = _userManager.Update(ObjectId.Parse(user.Id), user);

                if (isUpdated)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi cập nhật");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
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
