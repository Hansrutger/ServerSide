using RealBusinessPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBusinessPage.Controllers
{
    public class LoanController : Controller
    {
        // GET: loan
        public ActionResult Index()
        {
             

            return View();
        }

        // GET: loan/Details/hasse - om admin så vilken som helst användare, annars bara sig själv
        public ActionResult Details(string username)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "login");
            }

            if(Session["level"].ToString() == "2")
            {
                using (var db = new Model())
                {
                    var dbAccounts = db.Accounts.ToList();
                    List<Accounts> accountList = new List<Accounts>();
                    foreach(var obj in dbAccounts)
                    {
                        accountList.Add(obj);
                    }
                    ViewBag.selectedUser = username;
                    ViewBag.AccountList = accountList;
                }


            }



            using (var db = new Model())
            {
                var dbLoan = (from i in db.Loans.Include("Accounts").Include("Books") where i.Accounts.Username == username select i).ToList();
                List<Loans> loanList = new List<Loans>();
                foreach (var obj in dbLoan)
                {
                    loanList.Add(obj);
                }
                ViewBag.LoanList = loanList;
            }
            return View();
        }

        // POST: loan/renew/loanid
        public ActionResult Renew(int loanId)
        {
            DateTime newDate = DateTime.Now.AddDays(20);
            using (var db = new Model())
            {
                var dbLoan = (from i in db.Loans where i.LoanId == loanId select i).SingleOrDefault();
                if (dbLoan != null)
                {
                    dbLoan.Date = newDate.ToString();
                    db.SaveChanges();
                }
            }
            String username = Session["username"].ToString();
            return RedirectToAction("Details", "loan", new { username = username });
        }

        // GET: loan/Create
        public ActionResult Create(int bookId, string username)
        {
            using (var db = new Model())
            {
                var dbBook = (from b in db.Books where b.BookId == bookId select b).SingleOrDefault();
                if (dbBook != null)
                {
                    var dbLoan = (from i in db.Loans where i.BookId == bookId select i).SingleOrDefault();
                    if (dbLoan != null)
                    {
                        DateTime previousDate = Convert.ToDateTime(dbLoan.Date);
                        DateTime todayDate = DateTime.Now;

                        Boolean dateExpression = Convert.ToBoolean(DateTime.Compare(previousDate, todayDate));

                        if (!dateExpression)
                        {
                            db.Loans.Remove(dbLoan);
                            db.SaveChanges();

                            var dbAccount = (from a in db.Accounts where a.Username == username select a).SingleOrDefault();
                            if (dbAccount != null)
                            {
                                DateTime date = DateTime.Now.AddDays(20);

                                Loans loanObj = new Loans();
                                loanObj.Date = date.ToString();
                                loanObj.BookId = bookId;
                                loanObj.AccId = dbAccount.AccId;

                                db.Loans.Add(loanObj);
                                db.SaveChanges();

                                ViewBag.BookInfo = dbBook;

                                return View();
                            }
                            else
                            {
                                return RedirectToAction("Error");
                            }
                        }
                        else
                        {
                            return View("failCreate");
                        }

                       
                    }
                    else
                    {
                        var dbAccount = (from a in db.Accounts where a.Username == username select a).SingleOrDefault();
                        if (dbAccount != null)
                        {
                            DateTime date = DateTime.Now.AddDays(20);

                            Loans loanObj = new Loans();
                            loanObj.Date = date.ToString();
                            loanObj.BookId = bookId;
                            loanObj.AccId = dbAccount.AccId;

                            db.Loans.Add(loanObj);
                            db.SaveChanges();

                            ViewBag.BookInfo = dbBook;

                            return View();
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
        }

        

        // GET: loan/Delete/5
        public ActionResult Delete(int id)
        {
            using (var db = new Model())
            {
                var dbLoan = (from i in db.Loans where i.LoanId == id select i).SingleOrDefault();
                if (dbLoan != null)
                {
                    db.Loans.Remove(dbLoan);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Details", "loan", new { username = Session["username"].ToString() });
        }

        public ActionResult ForceLoan(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForceLoan(int id, FormCollection collection)
        {
            try
            {
                String username = collection["username"].ToString();
                using (var db = new Model())
                {
                    var dbUser = (from u in db.Accounts where u.Username == username select u).SingleOrDefault();
                    if (dbUser != null)
                    {
                        return RedirectToAction("Create", new { bookId = id, username = username });
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }
    }
}