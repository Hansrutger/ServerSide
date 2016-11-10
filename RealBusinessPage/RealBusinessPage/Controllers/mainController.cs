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

            else
            {
                List<BORROWSet> borrowList = new List<BORROWSet>();
                List<BOOKSet> bookList = new List<BOOKSet>();
                using (var db = new ServerSideEntities2())
                {
                    int personId = int.Parse(Session["personid"].ToString());
                    //var dbUser = (from u in db.BOOKSet where u. == Session["personid"].ToString() select u);
                    var dbLoan = (from a in db.BORROWSet where a.BORROWERPersonId == personId select a).ToList();
                    //var dbUser = (from u in db.BOOKSet where u.ISBN == dbLoan select u);
                    //var dbBooks = (from b in db.BOOKSet select b).ToList();
                    //inget smidigt sätt !!
                    foreach (var obj in dbLoan)
                    {
                        borrowList.Add(obj);
                        var dbUser = (from u in db.BOOKSet where obj.COPYSet.BOOKISBN == u.ISBN select u).SingleOrDefault();
                        bookList.Add(dbUser);
                    }
                    ViewBag.BookList = bookList;
                    ViewBag.LoanList = borrowList;

                }

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