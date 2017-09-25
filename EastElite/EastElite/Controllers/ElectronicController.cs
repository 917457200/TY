using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EastElite.Controllers
{
    public class ElectronicController : Controller
    {
        BLL.Cookie GetCookie = new BLL.Cookie();
        //
        // GET: /Electronic/
        public ActionResult Usage()
        {
           ViewBag.User  = GetCookie.GetUserCookie();
         
            return View();
        }
        public ActionResult Statistics()
        {
            ViewBag.User = GetCookie.GetUserCookie();

            return View();
        }
    }
}