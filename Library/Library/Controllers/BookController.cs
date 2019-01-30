using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddNewBook(string bookName, string authorName, string authorSurname, HttpPostedFileBase image,string block, string floor)
        {
            try
            {
                var classEntity = DAL.Act.getClassEntity();
                var library = Session["Library"] as DAL.Model.Library;
                string path = Path.Combine(Server.MapPath("~/Image"),
                System.IO.Path.GetFileName(image.FileName));
                image.SaveAs(path);
                string[] words = path.Split('\\');
                var count = words.Count();
                var imagealt = words[count - 1];
                var b = classEntity.AddNewBook(bookName, authorName, authorSurname, imagealt, library.Id,block,floor);

                return RedirectToAction("../Home/Index");
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public JsonResult DeleteBook(int id)
        {
            try
            {
                var classEntity = DAL.Act.getClassEntity();
                var book = classEntity.DeletBook(id);
                return Json(book);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}