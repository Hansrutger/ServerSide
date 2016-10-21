using RealBusinessPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBusinessPage.Controllers
{
    public class AccountController : Controller
    {
        
            // GET: account
            public ActionResult Index()
            {
            
                if (Session["username"] == null)
                {
                    return RedirectToAction("Index", "main");
                }
               
                using (var db = new Model())
                {
                    var dbUser = (from u in db.ACCOUNTs select u).ToList();
                    if (dbUser != null)
                    {
                        ViewBag.UserList = dbUser;
                    }
                }
                return View();
            }

            // GET: account/Details/5 - admin
            public ActionResult Details(string Username)
            {

            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "main");
            }

            using (var db = new Model())
                {
                    var dbUser = (from a in db.ACCOUNTs where a.Username == Username select a).SingleOrDefault();
                    if (dbUser != null)
                    {
                        ViewBag.UserInfo = dbUser;
                    }
                }
                return View();
            }

            // GET: account/Create - admin
            public ActionResult Create()
            {
            //if (Session["level"].ToString() != "2")
            //{
            //    return RedirectToAction("Index", "main");
            //}
            return View();
            }

            // POST: account/Create - admin
            [HttpPost]
            public ActionResult Create(FormCollection collection)
            {
                try
                {
                    String UsernameInput = collection["Username"].ToString();
                    String PasswordInput = collection["Password"].ToString();
                    String FirstNameInput = collection["FirstName"].ToString();
                    String LastNameInput = collection["LastName"].ToString();
                    String AddressInput = collection["Address"].ToString();
                    int TelNoInput = Convert.ToInt32(collection["TelNo"].ToString());
                    int LevelInput = Convert.ToInt32(collection["Level"].ToString());

                    using (var db = new Model())
                    {
                        var dbUser = (from u in db.ACCOUNTs where u.Username == UsernameInput select u).SingleOrDefault();
                        if (dbUser == null)
                        {
                            Accounts userObj = new Accounts();
                            userObj.Username = UsernameInput;
                            userObj.Password = PasswordInput;
                            userObj.FirstName = FirstNameInput;
                            userObj.LastName = LastNameInput;
                            userObj.Address = AddressInput;
                            userObj.TelNo = TelNoInput;
                            userObj.Level = LevelInput;

                         //   db.ACCOUNTs.Add(userObj);
                            db.SaveChanges();
                        }
                        else
                        {
                            return View("failCreate");
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }

            // GET: account/Edit/5 - admin
            public ActionResult Edit(int id)
            {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "main");
            }
            using (var db = new Model())
                {
                    var dbAccount = (from a in db.ACCOUNTs where a.AccId == id select a).SingleOrDefault();
                    if (dbAccount != null)
                    {
                        ViewBag.AccountInfo = dbAccount;
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                return View();
            }

            // POST: account/Edit/5 - admin
            [HttpPost]
            public ActionResult Edit(int id, FormCollection collection)
            {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "main");
            }
            try
                {
                    String UsernameInput = collection["Username"].ToString();
                    String PasswordInput = collection["Password"].ToString();
                    String FirstNameInput = collection["FirstName"].ToString();
                    String LastNameInput = collection["LastName"].ToString();
                    String AddressInput = collection["Address"].ToString();
                    int TelNoInput = Convert.ToInt32(collection["TelNo"].ToString());
                    int LevelInput = Convert.ToInt32(collection["Level"].ToString());

                    using (var db = new Model())
                    {
                        var dbAccount = (from a in db.ACCOUNTs where a.AccId == id select a).SingleOrDefault();
                        if (dbAccount != null)
                        {
                            dbAccount.Username = UsernameInput;
                            dbAccount.Password = PasswordInput;
                            dbAccount.FirstName = FirstNameInput;
                            dbAccount.LastName = LastNameInput;
                            dbAccount.Address = AddressInput;
                            dbAccount.TelNo = TelNoInput;
                            dbAccount.Level = LevelInput;
                            db.SaveChanges();
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            // GET: account/Delete/5 - admin
            public ActionResult Remove(int id)
            {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "main");
            }
            return View();
            }

            // POST: account/Delete/5 - admin
            [HttpPost]
            public ActionResult Remove(int id, FormCollection collection)
            {
                try
                {
                    using (var db = new Model())
                    {
                        var dbAccount = (from a in db.ACCOUNTs where a.Id == id select a).SingleOrDefault();
                        if (dbAccount != null)
                        {
                            db.ACCOUNTs.Remove(dbAccount);
                            db.SaveChanges();
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
        }
    

}
