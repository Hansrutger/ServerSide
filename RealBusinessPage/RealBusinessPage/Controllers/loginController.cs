using RealBusinessPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBusinessPage.Controllers
{
    public class LoginController : Controller
    {
        // GET: /login/
        public ActionResult Index()
        {


            return View();
        }
       

        public ActionResult Login(FormCollection collection)
        {
            try
            {
                string username = collection["Username"].ToString();
                string password = collection["Password"].ToString();

                using (var db = new Model())
                {
                    var user = (from a in db.Accounts where a.Username == username select a).SingleOrDefault();
                    if (user != null)
                    {
                        var userLoanList = (from a in db.Loans where a.AccId == user.AccId select a).ToList();
                        List<Loans> loanList = new List<Loans>();
                        if (userLoanList != null)
                        {
                            foreach(var obj in userLoanList)
                            {
                                loanList.Add(obj);
                            }
                            ViewBag.LoanList = loanList;
                        }
                        
                        Session["username"] = username;
                        Session["level"] = user.Level;
                        
                    }
                }



                return RedirectToAction("Index", "main");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "login");
        }
    }
}