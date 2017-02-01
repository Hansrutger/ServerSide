using RealBusinessPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealBusinessPage.Interfaces;
using RealBusinessPage.Services;

namespace RealBusinessPage.Controllers
{
    public class BookController : Controller
    {
        BookServices _bookInterface;
        public BookController()
        {
            _bookInterface = new BookServices();
        }

        // book
        public ActionResult Index()
        {
            
            if (Session["username"] == null || Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "Main");
            }

            List<BOOKSet> bookList = new List<BOOKSet>();
            bookList = _bookInterface.List();
            ViewBag.BookList = bookList;
            

                return View();
        }

        // GET: book/create - admin
        public ActionResult Create()
        {
            List<AUTHORSet> AuthorList = new List<AUTHORSet>();
            if (Session["level"].ToString() == "2")
            {
                
                try
                {
                    using (var db = new ServerSideEntities2())
                    {
                        var dbauthor = (from a in db.AUTHORSet select a).ToList();
                        if (dbauthor!=null)
                        {
                            foreach (var authorObj in dbauthor )
                            {
                                AuthorList.Add(authorObj);
                            }
                        }
                        ViewBag.AuthorList = AuthorList;
                        return View();
                    }
                }
                catch
                {
                    return RedirectToAction("Index", "Error");
                }
                
            }
            
            return RedirectToAction("Index", "main");
        }

        // POST: book/create - admin
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            char delimiterChars = ',';
            try
            {
                //int isbnInput = Convert.ToInt32(collection["ISBN"].ToString());
                String TitleInput = collection["Title"].ToString();
                string PublicationYearInput = collection["PublicationYear"].ToString();
                String DescriptionInput = collection["Description"].ToString();
                int PagesInput = Convert.ToInt32(collection["Pages"].ToString());                
                String authorIdList = collection["Authors"].ToString();

                String[] aIdList = authorIdList.Split(delimiterChars);
                using (var db = new ServerSideEntities2())
                {
                 


                        try
                        {
                            BOOKSet bookObj = new BOOKSet();                            
                            bookObj.Title = TitleInput;
                            bookObj.PublicationYear = PublicationYearInput;
                            bookObj.PublicationInfo = DescriptionInput;
                            bookObj.Pages = PagesInput;

                            foreach (string s in aIdList)
                            {
                                int i = Convert.ToInt32(s);

                                AUTHORBOOK aubo = new AUTHORBOOK();
                                aubo.AUTHORSetAId = i;
                                aubo.BOOKSetISBN = bookObj.ISBN;

                                db.AUTHORBOOKSet.Add(aubo);
                            }


                            db.BOOKSet.Add(bookObj);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return RedirectToAction("Error");
                        }
                    }



                

            }
            catch
            {
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index");
        }

        // book/details/123456789
        public ActionResult Details(int bookId)
        {
            List<AUTHORBOOK> auboList = new List<AUTHORBOOK>();
        using (var db = new ServerSideEntities2())
            {
                var dbBook = (from b in db.BOOKSet/*.Include("BOOKSet")*/ where b.ISBN == bookId select b).SingleOrDefault();
                var dbAuth = (from c in db.AUTHORBOOKSet.Include("AUTHORSet") where c.BOOKSetISBN == bookId select c).ToList<AUTHORBOOK>();
                //var dbauthtotList = (from d in db.AUTHORSet select d);
                if (dbBook != null && dbAuth != null)
                {
                    auboList = dbAuth;
                    ViewBag.BookInfo = dbBook;
                    ViewBag.AUBOInfo = dbAuth;
                    var dbLoan = (from i in db.BORROWSet.Include("BORROWERSet") where i.COPYBarcode == bookId select i).SingleOrDefault();
                    if (dbLoan != null)
                    {
                        ViewBag.BookLoan = "true";
                        ViewBag.LoanInfo = dbLoan;
                        ViewBag.BorrowerInfo = dbLoan.BORROWERSet;
                    }
                    else
                    {
                        ViewBag.BookLoan = "false";
                        ViewBag.LoanInfo = null;
                        ViewBag.AccountInfo = null;
                    }
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }

            return View();
        }

        // book/edit/123456789 - admin
        public ActionResult Edit(int id)
        {
        if (Session["level"].ToString() != "2")
        {
            return RedirectToAction("Index", "main");
        }
        using (var db = new ServerSideEntities2())
            {
                var dbBook = (from b in db.BOOKSet where b.ISBN == id select b).SingleOrDefault();
                if (dbBook != null)
                {
                    ViewBag.BookInfo = dbBook;
                }
            }

            List<AUTHORSet> AuthorList = new List<AUTHORSet>();
            using (var db = new ServerSideEntities2())
            {
                var dbauthor = (from a in db.AUTHORSet select a).ToList();
                if (dbauthor != null)
                {
                    foreach (var authorObj in dbauthor)
                    {
                        AuthorList.Add(authorObj);
                    }
                }
                ViewBag.AuthorList = AuthorList;
            }

            return View();
        }

        // book/edit/123456789 - admin
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "main");
            }
            char delimiterChars = ',';
            String[] aIdList = authorIdList.Split(delimiterChars);
            try
            {
                
                String TitleInput = collection["Title"].ToString();
                String PublicationYearInput = collection["PublicationYear"].ToString();
                String DescriptionInput = collection["Description"].ToString();
                int PagesInput = Convert.ToInt32(collection["Pages"].ToString());
                //int AuthorId = Convert.ToInt32(collection["AuthorId"].ToString());

                using (var db = new ServerSideEntities2())
                {
                    var dbBook = (from b in db.BOOKSet where b.ISBN == id select b).SingleOrDefault();
                   // var dbAuthor = (from a in db.AUTHORSet where a.AId == dbBook select a).SingleOrDefault();

                    if (dbBook != null)
                    {
                        try
                        {
                            
                            dbBook.Title = TitleInput;
                            dbBook.PublicationYear = PublicationYearInput;
                            dbBook.PublicationInfo = DescriptionInput;
                            dbBook.Pages = PagesInput;
                            //dbBook.AuthorId = dbAuthor.AuthorId;

                            db.SaveChanges();
                        }
                        catch
                        {
                            return RedirectToAction("Error");
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // book/remove/123456789 - admin
        public ActionResult Remove(int id)
        {
        if (Session["level"].ToString() != "2")
        {
            return RedirectToAction("Index", "main");
        }
        return View();
        }

        // book/remove/123456789 - admin
        [HttpPost]
        public ActionResult Remove(int id, FormCollection collection)
        {
            try
            {
                using (var db = new ServerSideEntities2())
                {
                    var dbBook = (from b in db.BOOKSet where b.ISBN == id select b).SingleOrDefault();
                    if (dbBook != null)
                    {
                        var dbLoan = (from i in db.BORROWSet from j in db.COPYSet where i.COPYBarcode == j.Barcode select i).ToList();
                        if (dbLoan != null)
                        {
                            
                            foreach ( var obj in dbLoan)
                            {
                                db.BORROWSet.Remove(obj);
                            }
                            
                            db.BOOKSet.Remove(dbBook);

                             var dbAuthorBook = (from i in db.AUTHORBOOKSet where i.BOOKSetISBN == dbBook.ISBN select i).ToList();
                            foreach(var obj in dbAuthorBook)
                            {
                                db.AUTHORBOOKSet.Remove(obj);
                            }


                            var BookCopy = (from i in db.COPYSet where dbBook.ISBN == i.BOOKISBN select i).ToList();
                            foreach(var obj in BookCopy)
                            {
                                db.COPYSet.Remove(obj);
                            }

                            db.SaveChanges();
                        }
                       

                        else
                        {
                            db.BOOKSet.Remove(dbBook);
                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Console.Write(e.ToString());
                return RedirectToAction("Error");
            }
        }

        //public ActionResult addAuthor(List<AUTHORSet> aList, AUTHORSet aperson)
        //{
        //    aList.Add(aperson);
        //    ViewBag.
        //}
    }
    
}
