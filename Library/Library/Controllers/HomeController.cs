using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            try
            {
                var classEntity = DAL.Act.getClassEntity();
                var lib = Session["Library"] as DAL.Model.Library;
                if (lib == null)
                    return RedirectToAction("../Login");
                var bookList = classEntity.getBookListbyLibId(lib.Id);
                List<Library.Models.BookViewModel> bvm = new List<Models.BookViewModel>();

                foreach (var item in bookList)
                {

                    bvm.Add(new Models.BookViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        AuthorName = classEntity.GetAuthorbyBookid(item.Id),
                        Image = item.ImageALT,
                        IsActive = Convert.ToBoolean(item.IsActive.ToString()),
                        RenterName = classEntity.getBookRenterbyBookid(item.Id),
                        Location=classEntity.GetBookLocationbyBookid(item.Id),
                        Time=classEntity.getBookTimebyBookid(item.Id)

                    });
                
                   
                }
                
                
                return View(bvm);
            }
            catch (Exception ex)
            {

                throw;
            }
        
        }
    }
}