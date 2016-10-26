using RealBusinessPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBusinessPage.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /search/

        public ActionResult Index()
        {


            List<BOOKSet> resultList = new List<BOOKSet>();
            try
            {
                using (var db = new ServerSideEntities2())
                {
                    var bookObj = (from b in db.BOOKSet select b).ToList();

                    if (bookObj != null)
                    {
                        foreach (var b in bookObj)
                        {
                            resultList.Add(b);
                        }

                        ViewBag.ResultSearch = resultList;
                        return View();
                    }
                    else
                    {
                        return View(); // lists == null
                    }
                }
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction("Error"); //s.length == 0
            }
        }

        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {

            List<BOOKSet> bookList = new List<BOOKSet>();
            try
            {
                String s = collection["searchText"].ToString();

                using (var db = new ServerSideEntities2())
                {
                    var authorObj = (from a in db.AUTHORSet where (a.FirstName.Contains(s) || a.LastName.Contains(s)) select a).ToList();
                    var bookObj = (from b in db.BOOKSet where b.Title.Contains(s) select b).ToList();

                    if (bookObj != null && bookObj.Count != 0)
                    {
                        foreach (var b in bookObj)
                        {
                            bookList.Add(b);
                        }
                        ViewBag.ResultSearch = bookList;
                        return View();
                    }

                    if (authorObj != null)
                    {
                        foreach (var a in authorObj)
                        {
                            var newBookSearch = (from b in db.AUTHORBOOKSet where b.AUTHORSetAId == a.AId select b).ToList();
                            if (newBookSearch != null)
                            {
                                foreach (var b in newBookSearch)
                                {
                                    bookList.Add(b.BOOKSet);
                                }
                            }
                        }
                        ViewBag.ResultSearch = bookList;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Error"); // lists == null
                    }
                }
            }
            catch (NullReferenceException e)
            {
                return View(); //s.length == 0
            }
        }
    }
}