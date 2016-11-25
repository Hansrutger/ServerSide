using RealBusinessPage.App_Start;
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

                using (var db = new ServerSideEntities2())
                {
                    var user = (from a in db.BORROWERSet where a.Username == username select a).SingleOrDefault();
                    if (user != null && hiddenSecrets.hashPassword( password )== user.Password)
                    {
                        var borrower = (from b in db.BORROWSet where b.BORROWERPersonId == user.PersonId select b).ToList();
                        
                        List<BORROWSet> loanList = new List<BORROWSet>();
                        if (borrower != null)
                        {
                            foreach (var obj in borrower)
                            {
                                loanList.Add(obj);
                            }
                            ViewBag.LoanList = loanList;
                        }

                        Session["username"] = username;
                        Session["personId"] = user.PersonId;
                        Session["level"] = user.Level;

                    }
                }



                return RedirectToAction("Index", "main");
            }
            catch
            {
                return RedirectToAction("Index","Error" );
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "login");
        }
    }
}