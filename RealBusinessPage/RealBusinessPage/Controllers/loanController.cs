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
        public ActionResult Details(int username)
        {
            //if (Session["level"].ToString() != "2")
            //{
            //    return RedirectToAction("NoAuthrization", "Error");
            //}

            if (Session["level"].ToString() == "2")
            {
                using (var db = new ServerSideEntities2())
                {
                    var dbAccounts = db.BORROWERSet.ToList();
                    List<BORROWERSet> accountList = new List<BORROWERSet>();
                    foreach (var obj in dbAccounts)
                    {
                        accountList.Add(obj);
                    }
                    ViewBag.selectedUser = username;
                    ViewBag.AccountList = accountList;
                }
                return RedirectToAction("NoAuthrization", "Error");

            }
            //else
            //{
            //    using (var db = new ServerSideEntities2())
            //    {
            //        var dbLoans = (from a in db.BORROWSet where a.Borrowid == username select a);
            //        List<BORROWSet> LoanList = new List<BORROWSet>();

            //    }
            //}


            using (var db = new ServerSideEntities2())
            {
                //var dbLoan = (from i in db.BORROWSet.Include("BORROWERSet").Include("BOOKSet") where i.Borrowid == username select i).ToList();
                var dbLoan = (from i in db.BORROWSet where i.Borrowid == username select i).ToList();
                List<BORROWSet> loanList = new List<BORROWSet>();
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
            using (var db = new ServerSideEntities2())
            {
                var dbLoan = (from i in db.BORROWSet where i.BORROWERPersonId == loanId select i).SingleOrDefault();
                if (dbLoan != null)
                {
                    dbLoan.ToBeReturnedDate = newDate.ToString();
                    db.SaveChanges();
                }
            }
            String username = Session["username"].ToString();
            return RedirectToAction("Details", "loan", new { username = username });
        }

        // GET: loan/Create
        public ActionResult Create(int ISBN, string username)
        {
            int _username = Convert.ToInt32(username);
            using (var db = new ServerSideEntities2())
            {
                var dbBook = (from b in db.COPYSet where (b.BOOKISBN == ISBN && b.STATUSSet.status == "In Stock") select b).FirstOrDefault();

                //DateTime todaysDate = DateTime.Now;
                //DateTime returnDate = DateTime.Now.AddDays(20);


                if (dbBook != null)
                {                    
                    DateTime todayDate = DateTime.Now;
                    var dbLoan = (from i in db.BORROWSet where i.BORROWERPersonId == _username select i).SingleOrDefault();


                    if (dbLoan != null)
                    {
                        DateTime previousDate = Convert.ToDateTime(dbLoan.ToBeReturnedDate);
                        

                        Boolean dateExpression = Convert.ToBoolean(DateTime.Compare(previousDate, todayDate));

                        if (!dateExpression)
                        {
                            db.BORROWSet.Remove(dbLoan);
                            db.SaveChanges();

                            var dbAccount = (from a in db.BORROWERSet where a.PersonId == _username select a).SingleOrDefault();
                            if (dbAccount != null)
                            {
                                DateTime date = DateTime.Now.AddDays(20);
                                string NewDate = date.ToString();
                                BORROWSet loanObj = new BORROWSet();
                                loanObj.ToBeReturnedDate = date.Date.ToString();
                                loanObj.BorrowDate = todayDate.Date.ToString();
                                loanObj.BORROWERPersonId = _username;
                                //loanObj.COPYBarcode = ISBN;

                                db.BORROWSet.Add(loanObj);
                                
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
                        var dbBorrower = (from a in db.BORROWERSet where a.PersonId == _username select a).SingleOrDefault();
                        if (dbBorrower != null)
                        {
                            //DateTime date = DateTime.Now.AddDays(20);                           
                            BORROWSet loanObj = new BORROWSet();
                            String todaysDate = DateTime.Now.Date.ToString();
                            String returnDate = DateTime.Now.AddDays(20).Date.ToString();
                            loanObj.ToBeReturnedDate = returnDate;
                            loanObj.BorrowDate = todaysDate;
                            loanObj.BORROWERPersonId = _username;
                            //loanObj.Borrowid = (dbBook.BOOKISBN + dbBorrower.PersonId);
                            loanObj.BORROWERSet = dbBorrower;
                            loanObj.COPYBarcode =dbBook.Barcode ;

                            db.BORROWSet.Add(loanObj);
                            dbBook.STATUSStatusId = 2;
                            db.SaveChanges();

                            ViewBag.BookInfo = dbBook.BOOKSet;

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
                    return RedirectToAction("BookNotInStock", "Error");
                }
            }
        }



        // GET: loan/Delete/5
        public ActionResult Delete(int id)
        {
            using (var db = new ServerSideEntities2())
            {
                var dbLoan = (from i in db.BORROWSet where i.BORROWERPersonId == id select i).SingleOrDefault();
                var dbCopy = (from a in db.COPYSet where a.Barcode == dbLoan.COPYBarcode select a).SingleOrDefault();
                if (dbLoan != null && dbCopy != null)
                {
                    db.BORROWSet.Remove(dbLoan);
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
                using (var db = new ServerSideEntities2())
                {
                    var dbUser = (from u in db.BORROWERSet where u.Username == username select u).SingleOrDefault();
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