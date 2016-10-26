using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBusinessPage.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult UserNotFound()
        {
            return View();
        }

        public ActionResult UsernameAlreadyTaken()
        {
            return View();
        }

        public ActionResult NoAuthrization()
        {
            return View();
        }


    }
}
