using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZachT.Controllers
{
    public class RouteTestController : Controller
    {
        // GET: RouteTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2(string id)
        {
            return Content(String.Format("id的值為:{0}",id));

        }
    }
}