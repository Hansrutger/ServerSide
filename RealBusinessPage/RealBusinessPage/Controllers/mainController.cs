using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBusinessPage.Controllers
{
    public class MainController : Controller
    {
        
        // GET: /main/
        public ActionResult Index()
        {
            //Session["loggedIn"] = "true";
            //Session["username"] = "hans";
            //Session["admin"] = "true";
            //Session["username"] = "nope";
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "login");
            }
            
            return View();
        }

        /*

        --- I Razor används följande för att separera olika saker ---
        Session["loggedIn"] = "true";
        Session["admin"] = "true";
        Session["username"] = username;
        */
    }
}