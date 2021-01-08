using EHiHiBook.Repository;
using EHiHiBook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;


namespace EHiHiBook.Controllers
{
    public class AdminController : Controller
    {
        SachEntities _db = new SachEntities();
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryID.ToString(), Text = item.CateoryName });
            }
            return list;
        }

        public ActionResult Index()
        {
            return View();
        }


        //Categories:
        public ActionResult Categories()
        {
            List<Category> allcategories = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
            if (categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Category>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);

        }

        public ActionResult EditCategory(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Category>().GetFirstorDefault(catId));
        }
        [HttpPost]
        public ActionResult EditCategory(Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Category>().Update(tbl);
            return RedirectToAction("Categories");
        }

        //----------------------

        //Product:
        public ActionResult Book()
        {
            return View(_unitOfWork.GetRepositoryInstance<Book>().GetBook());
        }
        public ActionResult EditBook(int bookId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Book>().GetFirstorDefault(bookId));
        }
        [HttpPost]
        public ActionResult EditBook(Book tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Contents/images/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.Images = file != null ? pic : tbl.Images;
            _unitOfWork.GetRepositoryInstance<Book>().Update(tbl);
            return RedirectToAction("Book");
        }
        public ActionResult AddBook()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult AddBook(Book tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images/"), pic);
                file.SaveAs(path);
            }
            tbl.Images = pic;
            _unitOfWork.GetRepositoryInstance<Book>().Add(tbl);
            return RedirectToAction("Book");
        }
        public ActionResult DeleteBook(int id)
        {
            return View(_db.Books.Where(s => s.BookID == id).FirstOrDefault());
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Book book)
        {
            try
            {
                book = _db.Books.Where(s => s.BookID == id).FirstOrDefault();
                _db.Books.Remove(book);
                _db.SaveChanges();
                return RedirectToAction("Book");
            }
            catch
            {
                return View();
            }
        }
    }
}