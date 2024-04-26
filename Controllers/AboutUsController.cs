using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmileT.Controllers
{
    public class AboutUsController : Controller
    {
        public ActionResult AboutIndex()
        {
            return View();
        }
    }
}