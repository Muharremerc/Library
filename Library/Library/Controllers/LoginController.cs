using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Log(string lname,string password)
        {
            try
            {
                var classEntity = DAL.Act.getClassEntity();
                var l = classEntity.Login(lname, password);
                Session["Library"] = l;
                Session["Name"] = l.Name;
                if (l == null)
                    return Json(false);
                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult LogOut()
        {

            try
            {
                Session["Library"] = null;
                Session["Name"] = null;
                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}