using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MANAGMENT.Models;

using MANAGMENT.Models.ViewModels;

namespace MANAGMENT.Controllers
{
    
    public class CustomerController : Controller
    {
        // GET: Customer
        CompanyDBEntities db = new CompanyDBEntities();
        public ActionResult Index(string username)
        {
            ViewBag.customer = username;
            TempData["name"] = username;

            var orderDetails = db.OrderItems.Where(x => x.Customer.EmailID == username).ToList();
            ViewBag.orderItems = orderDetails;
            return View();
        }
        public ActionResult Details()
        {

            var model = db.Products.ToList();

            return View(model);
        }



    }
}