using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class RentController : Controller
    {
    
        [HttpPost]
        public ActionResult Index(int id)
        {
            try
            {
                var classEntity = DAL.Act.getClassEntity();
                var Book = classEntity.getBook(id);
                return View(Book);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public JsonResult RentBook(int id,string name,string surname)
        {
            try
            {
                var classEntity = DAL.Act.getClassEntity();
                var rentBook = classEntity.RentBook(name, surname, id);
                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult giveBackbyBookId(int id)
        {
            try
            {
                var classEntity = DAL.Act.getClassEntity();
                var book = classEntity.givebackBook(id);
                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}