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
        IBookServices _bookInterface;
        public BookController()
        {
            _bookInterface = new BookServices();
        }

        // book
        public ActionResult Index()
        {
            
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "login");
            }

            List<BOOK> bookList = _bookInterface.List();
            ViewBag.BookList = bookList;
             
            return View();
        }

        // GET: book/create - admin
        public ActionResult Create()
        {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "main");
            }
            return View();
        }

        // POST: book/create - admin
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                String isbnInput = collection["ISBN"].ToString();
                String TitleInput = collection["Title"].ToString();
                int PublicationYearInput = Convert.ToInt32(collection["PublicationYear"].ToString());
                String DescriptionInput = collection["Description"].ToString();
                int PagesInput = Convert.ToInt32(collection["Pages"].ToString());
                int AuthorId = Convert.ToInt32(collection["AuthorId"].ToString());

                using (var db = new LibraryDB())
                {
                    var dbAuthor = (from a in db.Authors where a.AuthorId == AuthorId select a).SingleOrDefault();
                    if (dbAuthor != null)
                    {
                        try
                        {
                            Books bookObj = new Books();
                            bookObj.ISBN = isbnInput;
                            bookObj.Title = TitleInput;
                            bookObj.PubYear = PublicationYearInput;
                            bookObj.Description = DescriptionInput;
                            bookObj.Pages = PagesInput;
                            bookObj.AuthorId = dbAuthor.AuthorId;

                            db.Books.Add(bookObj);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return RedirectToAction("Error");
                        }
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
            
        using (var db = new LibraryDB())
            {
                var dbBook = (from b in db.Books.Include("Authors") where b.BookId == bookId select b).SingleOrDefault();
                if (dbBook != null)
                {
                    ViewBag.BookInfo = dbBook;

                    var dbLoan = (from i in db.Loans.Include("Accounts") where i.BookId == bookId select i).SingleOrDefault();
                    if (dbLoan != null)
                    {
                        ViewBag.BookLoan = "true";
                        ViewBag.LoanInfo = dbLoan;
                        ViewBag.AccountInfo = dbLoan.Accounts;
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
        using (var db = new LibraryDB())
            {
                var dbBook = (from b in db.Books.Include("Authors") where b.BookId == id select b).SingleOrDefault();
                if (dbBook != null)
                {
                    ViewBag.BookInfo = dbBook;
                }
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

            try
            {
                String isbnInput = collection["ISBN"].ToString();
                String TitleInput = collection["Title"].ToString();
                int PublicationYearInput = Convert.ToInt32(collection["PublicationYear"].ToString());
                String DescriptionInput = collection["Description"].ToString();
                int PagesInput = Convert.ToInt32(collection["Pages"].ToString());
                int AuthorId = Convert.ToInt32(collection["AuthorId"].ToString());

                using (var db = new LibraryDB())
                {
                    var dbBook = (from b in db.Books where b.BookId == id select b).SingleOrDefault();
                    var dbAuthor = (from a in db.Authors where a.AuthorId == AuthorId select a).SingleOrDefault();

                    if (dbBook != null && dbAuthor != null)
                    {
                        try
                        {
                            dbBook.ISBN = isbnInput;
                            dbBook.Title = TitleInput;
                            dbBook.PubYear = PublicationYearInput;
                            dbBook.Description = DescriptionInput;
                            dbBook.Pages = PagesInput;
                            dbBook.AuthorId = dbAuthor.AuthorId;

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
                using (var db = new LibraryDB())
                {
                    var dbBook = (from b in db.Books where b.BookId == id select b).SingleOrDefault();
                    if (dbBook != null)
                    {
                        var dbLoan = (from i in db.Loans where i.BookId == id select i).SingleOrDefault();
                        if (dbLoan != null)
                        {
                            db.Loans.Remove(dbLoan);
                            db.Books.Remove(dbBook);
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Books.Remove(dbBook);
                            db.SaveChanges();
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
    }
    
}
