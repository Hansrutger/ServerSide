using RealBusinessPage.Models;
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
            
            //else
            //{
            //    List<BORROWSet> borrowList = new List<BORROWSet>();
            //    try
            //    {
            //        using (var db = new ServerSideEntities2())
            //        {
            //            var dbBorrower = (from a in db.BORROWSet where a.BORROWERPersonId == Convert.ToInt32(Session["personId"].ToString()) select a).ToList();

            //            foreach (var borrow in dbBorrower)
            //            {
                            
            //            }
            //        }
            //    }
                
            //    catch
            //    {

            //    }
            //}
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