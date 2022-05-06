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
    public class CategoryController : Controller
    {
        private ICategoryManager _categoryManager = new CategoryManager();
        // GET: CategoryController
        public IActionResult Index(int pg = 1)
        {
            List<Category> categories = _categoryManager.GetAll().ToList();

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = categories.Count;

            var pager = new PagerModel(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = categories.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(string id)
        {
            var result = _categoryManager.GetById(ObjectId.Parse(id));

            return View(result);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_categoryManager.isNotExist(category.CategoryName))
                    {
                        bool isCreated = _categoryManager.Add(category);

                        if (isCreated)
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
                        ModelState.AddModelError("", "Tên danh mục đã tồn tại");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(string id)
        {
            var result = _categoryManager.GetById(ObjectId.Parse(id));

            return View(result);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isUpdated = _categoryManager.Update(ObjectId.Parse(category.Id),category);

                    if (isUpdated)
                    {
                        ModelState.AddModelError("", "Cập nhật thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật thất bại");
                    }
                }

                return View(category);
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
