using RealBusinessPage.Models;
using System;
using System.Linq;
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
               //vad gör detta?
                using (var db = new ServerSideEntities2())
                {
                    var dbUser = (from u in db.BORROWERSet select u).ToList();
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
            if (Session["level"].ToString() != "2"  && Username!= Session["username"].ToString())
            {
                return RedirectToAction("NoAuthrization", "Error");
            }

           
            using (var db = new ServerSideEntities2())
                {
                    var dbUser = (from a in db.BORROWERSet where a.Username == Username select a).SingleOrDefault();
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
                   
                    using (var db = new ServerSideEntities2())
                    {
                        var dbUser = (from u in db.BORROWERSet where u.Username == UsernameInput select u).SingleOrDefault();
                        if (dbUser == null)
                        {

                        CATEGORYSet newCat = new CATEGORYSet();
                        newCat.Category = "adult";
                        newCat.Period = 0;
                        newCat.PenaltyPerDay = 100;
                        
                            BORROWERSet userObj = new BORROWERSet();                            
                            userObj.Username = UsernameInput;
                            userObj.Password = PasswordInput;
                            userObj.Level = LevelInput;
                            
                            //BORROWERSet userBorrower = new BORROWERSet();
                            userObj.FirstName = FirstNameInput;
                            userObj.LastName = LastNameInput;
                            userObj.Address = AddressInput;
                            userObj.TelNo = TelNoInput;
                            userObj.CATEGORYSet = newCat;


                            db.BORROWERSet.Add(userObj);
                           // db.Entry(userObj).State = System.Data.Entity.EntityState.Added;
                            db.SaveChanges();
                        }
                        else
                        {
                            return View("Error","failCreate");
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    ViewBag.Error = e;
                    return RedirectToAction("Index","Error");
                }
            }

            // GET: account/Edit/5 - admin
            public ActionResult Edit(int id)
            {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "main");
            }
            using (var db = new ServerSideEntities2())
                {
                    var dbAccount = (from a in db.BORROWERSet where a.PersonId == id select a).SingleOrDefault();
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
                return RedirectToAction("NoAuthrization", "Error");
            }
            try
                {
                    String UsernameInput = collection["Username"].ToString();
                    String PasswordInput = collection["Password"].ToString();
                    String FirstNameInput = collection["FirstName"].ToString();
                    String LastNameInput = collection["LastName"].ToString();
                    String AddressInput = collection["Address"].ToString();
                    int TelNoInput = Convert.ToInt32(collection["TelNo"].ToString());
                    int LevelInput = Convert.ToInt16(collection["Level"].ToString());

                    using (var db = new ServerSideEntities2())
                    {
                        var dbAccount = (from a in db.BORROWERSet where a.PersonId == id select a).SingleOrDefault();
                        if (dbAccount != null)
                        {
                            dbAccount.Username = UsernameInput;
                            dbAccount.Password = PasswordInput;
                            
                            //dbAccount.FirstName = FirstNameInput;
                            //dbAccount.LastName = LastNameInput;
                            //dbAccount.Address = AddressInput;
                            //dbAccount.TelNo = TelNoInput;
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
                return RedirectToAction("NoAuthrization", "Error");
            }
            return View();
            }

            // POST: account/Delete/5 - admin
            [HttpPost]
            public ActionResult Remove(int id, FormCollection collection)
            {
                try
                {
                    using (var db = new ServerSideEntities2())
                    {
                        var dbAccount = (from a in db.BORROWERSet where a.PersonId == id select a).SingleOrDefault();
                        if (dbAccount != null)
                        {
                            db.BORROWERSet.Remove(dbAccount);
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
