using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MANAGMENT.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(string username)
        {
            ViewBag.customer = username;
            return View();
        }
    }
}