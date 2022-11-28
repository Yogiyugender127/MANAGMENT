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
            ViewBag.customername = db.Customers.Where(x => x.EmailID == username).FirstOrDefault().CustomerName;

            var orderDetails = db.OrderItems.Where(x => x.Customer.EmailID == username).ToList();
            ViewBag.orderItems = orderDetails;
            return View();
        }
        public ActionResult Details()
        {

            var model = db.Products.ToList();

            return View(model);
        }
        public ActionResult Deleteorder(int? id,int? customerid)

        {

            var Order = db.OrderItems.Find(id);


            db.OrderItems.Remove(Order);
            db.SaveChanges();


            var customername = db.Customers.Where(x => x.CustomerID == customerid).FirstOrDefault().EmailID;
            return RedirectToAction("Index", "Customer", new { username = customername });


        }



    }
}