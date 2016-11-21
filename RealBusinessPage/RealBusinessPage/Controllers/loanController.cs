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
           int _username =Convert.ToInt32( username);
            
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
                    
                    ViewBag.AccountList = accountList;
                }
                

            }
            //else
            //{
            //    using (var db = new ServerSideEntities2())
            //    {
            //        var dbLoans = (from a in db.BORROWSet where a.Borrowid == username select a);
            //        List<BORROWSet> LoanList = new List<BORROWSet>();

            //    }
            //}


            //using (var db = new ServerSideEntities2())
            //{
            //    //var dbLoan = (from i in db.BORROWSet.Include("BORROWERSet").Include("BOOKSet") where i.Borrowid == username select i).ToList();
            //    var dbLoan = (from i in db.BORROWSet where i.Borrowid == username select i).ToList();
            //    List<BORROWSet> loanList = new List<BORROWSet>();
            //    foreach (var obj in dbLoan)
            //    {
            //        loanList.Add(obj);
            //    }
            //    ViewBag.LoanList = loanList;
            //}
            List<BORROWSet> borrowList = new List<BORROWSet>();
            List<BOOKSet> bookList = new List<BOOKSet>();
            using (var db = new ServerSideEntities2())
            {
                //int personId = int.Parse(Session["personid"].ToString());

                var slectedUser = (from a in db.BORROWERSet where a.PersonId == _username select a).SingleOrDefault();
                //var dbUser = (from u in db.BOOKSet where u. == Session["personid"].ToString() select u);
                var dbLoan = (from a in db.BORROWSet where a.BORROWERPersonId == slectedUser.PersonId select a).ToList();
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
                ViewBag.selectedUser = slectedUser.Username;
            }
                return View();
        }

        // POST: loan/renew/loanid
        public ActionResult Renew(int bookISBN, string _username)
        {
            DateTime newDate = DateTime.Now.AddDays(20);
            int borrowerID = Convert.ToInt32(_username);
            using (var db = new ServerSideEntities2())
            {
                var dbLoan = (from i in db.BORROWSet where i.BORROWERPersonId == borrowerID && i.COPYBarcode == bookISBN select i).SingleOrDefault();
                if (dbLoan != null)
                {
                    dbLoan.ToBeReturnedDate = newDate.ToString();
                    db.SaveChanges();
                }
            }
            //String username = Session["username"].ToString();
            return RedirectToAction("Index", "main");
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
                    var dbLoan = (from i in db.BORROWSet where i.BORROWERPersonId == _username && i.COPYBarcode== dbBook.Barcode select i).SingleOrDefault();


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
        public ActionResult Delete(int bookISBN, string _username)
        {
            int borrowerID = Convert.ToInt32(_username);
            using (var db = new ServerSideEntities2())
            {
                //var dbLoan = (from i in db.BORROWSet where i.BORROWERPersonId == borrowerID select i).SingleOrDefault();
                //var dbCopy = (from a in db.COPYSet where a.Barcode == dbLoan.COPYBarcode select a).SingleOrDefault();
                var loan = (from i in db.BORROWSet where i.BORROWERPersonId == borrowerID select i).ToList();
                var obj = (from k in loan where k.COPYBarcode == bookISBN select k).SingleOrDefault();
                
                if (obj != null)
                {
                    obj.COPYSet.STATUSStatusId = 1;
                    db.BORROWSet.Remove(obj);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Error");
                }
              
            }
            return RedirectToAction("Index", "main");
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