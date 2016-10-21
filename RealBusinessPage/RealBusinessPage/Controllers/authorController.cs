using RealBusinessPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBusinessPage.Controllers
{
    public class AuthorController : Controller
    {
            // GET: author
            public ActionResult Index()
            {
                   if (Session["username"] == null)
                {
                    return RedirectToAction("Index", "login");
                }

            return View();
            }

            // GET: author/Details/5
            public ActionResult Details(int aid)
            {
           
            using (var db = new Model())
                {
                    var dbAuthor = (from a in db.Authors where a.AuthorId == aid select a).SingleOrDefault();
                    if (dbAuthor != null)
                    {
                        ViewBag.AuthorInfo = dbAuthor;
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                return View();
            }

            // GET: author/Create - admin
            public ActionResult Create()
            {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "main");
            }
            return View();
            }

            // POST: author/Create - admin
            [HttpPost]
            public ActionResult Create(FormCollection collection)
            {
                try
                {
                    String FirstNameInput = collection["FirstName"].ToString();
                    String LastNameInput = collection["LastName"].ToString();
                    int BirthYearInput = Convert.ToInt32(collection["BirthYear"].ToString());

                    using (var db = new Model())
                    {
                        Authors authorObj = new Authors();
                        authorObj.FirstName = FirstNameInput;
                        authorObj.LastName = LastNameInput;
                        authorObj.BirthYear = BirthYearInput;

                        db.Authors.Add(authorObj);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "book");
                }
                catch
                {
                    return View();
                }
            }

            // GET: author/Edit/5 - admin
            public ActionResult Edit(int id)
            {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "main");
            }
            using (var db = new Model())
                {
                    var dbAuthor = (from a in db.Authors where a.AuthorId == id select a).SingleOrDefault();
                    if (dbAuthor != null)
                    {
                        ViewBag.AuthorInfo = dbAuthor;
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                return View();
            }

            // POST: author/Edit/5 - admin
            [HttpPost]
            public ActionResult Edit(int id, FormCollection collection)
            {
                try
                {
                    String FirstNameInput = collection["FirstName"].ToString();
                    String LastNameInput = collection["LastName"].ToString();
                    int BirthYearInput = Convert.ToInt32(collection["BirthYear"].ToString());

                    using (var db = new Model())
                    {
                        var dbAuthor = (from a in db.Authors where a.AuthorId == id select a).SingleOrDefault();
                        dbAuthor.FirstName = FirstNameInput;
                        dbAuthor.LastName = LastNameInput;
                        dbAuthor.BirthYear = BirthYearInput;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Details", "book", new { bookId = id });
                 }
                  catch (Exception e)
                  {
                      return RedirectToAction("Error");
                  }
              }

            // GET: author/Delete/5 - admin
            public ActionResult Remove(int id)
            {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("Index", "main");
            }
            return View();
            }

            // POST: author/Delete/5 - admin
            [HttpPost]
            public ActionResult Remove(int id, FormCollection collection)
            {
                try
                {
                    using (var db = new Model())
                    {
                        var dbAuthor = (from a in db.AUTHORs where a.Aid == id select a).SingleOrDefault();
                        if (dbAuthor != null)
                        {
                            var dbBook = (from b in db.BOOKs where b.ISBN == id select b).ToList();
                            if (dbBook != null)
                            {
                                foreach (var book in dbBook)
                                {
                                    var dbLoan = (from i in db.Loans where i.BookId == book.BookId select i).SingleOrDefault();
                                    if (dbLoan != null)
                                    {
                                        db.Loans.Remove(dbLoan);
                                    }
                                    db.Books.Remove(book);
                                }
                            }
                            db.Authors.Remove(dbAuthor);
                            db.SaveChanges();
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }
                    return RedirectToAction("Index", "book");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }
        }
    

}
