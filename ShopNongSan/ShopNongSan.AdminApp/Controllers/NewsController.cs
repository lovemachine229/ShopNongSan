using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ShopNongSan.Data.Collection;
using ShopNongSan.Data.Models;
using ShopNongSan.Service.Interfaces.IManager;
using ShopNongSan.Service.Manager;
using ShopNongSan.Service.Tools;

namespace ShopNongSan.AdminApp.Controllers
{
    [Route("admin/[controller]/[action]")]
    public class NewsController : Controller
    {
        private INewsManager _newsManager = new NewsManager();
        // GET: NewsController
        public ActionResult Index(string sortExpression="")
        {
            SortModel sortModel = new SortModel();

            sortModel.AddColumn("title");
            sortModel.AddColumn("time");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;

            //List<News> news = _newsManager.GetUsers(sortModel.SortedProperty, sortModel.SortedOrder);
            var result = _newsManager.GetAll();

            return View(result);
        }

        // GET: NewsController/Details/5
        public ActionResult Details(string id)
        {
            var result = _newsManager.GetById(ObjectId.Parse(id));

            return View(result);
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    news.Id = ObjectId.GenerateNewId().ToString();
                    news.Created_At = DateTime.Parse(DateTimeOffset.Now.ToString());

                    byte[] imageArray = System.IO.File.ReadAllBytes(@"C:\Users\ADMIN\OneDrive\Máy tính\Đồ án\ShopNongSan\ShopNongSan\ShopNongSan\ShopNongSan.AdminApp\wwwroot\assets\img\default-avatar.png");
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);


                    if (image != null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        image.OpenReadStream().CopyTo(memoryStream);

                        news.Thumbnail = Convert.ToBase64String(memoryStream.ToArray());
                    }
                    else
                    {
                        news.Thumbnail = base64ImageRepresentation;
                    }


                    bool isSaved = _newsManager.Add(news);

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

                return View(news);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
                return View();
            }
        }

        // GET: NewsController/Edit/5
        public ActionResult Edit(string id)
        {
            var result = _newsManager.GetById(ObjectId.Parse(id));

            if(result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news, IFormFile thumbnail)
        {
            try
            {
                News oldeNews = _newsManager.GetById(ObjectId.Parse(news.Id));
                if (thumbnail != null)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    thumbnail.OpenReadStream().CopyTo(memoryStream);

                    news.Thumbnail = Convert.ToBase64String(memoryStream.ToArray());
                }
                else
                {
                    string oldImage = oldeNews.Thumbnail;

                    if (oldImage == null)
                    {
                        byte[] imageArray = System.IO.File.ReadAllBytes(@"C:\Users\ADMIN\OneDrive\Máy tính\Đồ án\ShopNongSan\ShopNongSan\ShopNongSan\ShopNongSan.AdminApp\wwwroot\assets\img\no-image.png");
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                        news.Thumbnail = base64ImageRepresentation;
                    }
                    else
                        news.Thumbnail = oldImage;
                }

                bool isUpdated = _newsManager.Update(ObjectId.Parse(news.Id), news);

                if (isUpdated)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi cập nhật");
                }
                return View(news);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(news);
            }
        }

        // GET: NewsController/Delete/5
        public ActionResult Delete(string id)
        {
            var result = _newsManager.GetById(ObjectId.Parse(id));

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: NewsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(string Id)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    bool isDeleted = _newsManager.Delete(ObjectId.Parse(Id));

                    if (isDeleted)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Xóa thất bại");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
