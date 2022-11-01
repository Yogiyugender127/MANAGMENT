using MANAGMENT.Models;
using MANAGMENT.Models.ViewModels;
using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace MANAGMENT.Controllers
{
    public class HomeController : Controller
    {

        CompanyDBEntities db = new CompanyDBEntities();

        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            //try
            //{
            //    Helper.WriteDebug(null, "Debug ");
            //    Helper.WriteWarning(null, "Warning ");
            //    throw new NotImplementedException();
            //}
            //catch (Exception e)
            //{
            //    Helper.WriteError(e, "Error");
            //    Helper.WriteFatal(e, "Fatal");
            //    Helper.WriteVerbose(e, "Verbose");
            //    throw;
            //}


            //ViewBag.Message = "Your contact page.";

            return View();
        }
       public ActionResult ProductDetails()
        {
            
            var item = db.Products.ToList();
            return View(item);
        }
        public ActionResult ViewDetails(int? id)
        {

            TempData["productid"] = id;
            var item = db.Products.Find(id);
            return View(item);
        }
        public ActionResult carousel()
        {
            return View();
        }
        public ActionResult OrderPage(OrderItemsModel model )
     {

            
            var email = TempData["name"];
            var productid = TempData["productid"];
            model.CatergoryID = 111;
            model.ProductID = Convert.ToInt32(productid);
            model.ProductName = db.Products.Where(x => x.ProductID == model.ProductID).FirstOrDefault().DisplayName;
            model.OrderTotal = db.Products.Where(x => x.ProductID == model.ProductID).FirstOrDefault().price;
            model.Email = email.ToString();
            model.CustomerID = db.Customers.Where(x => x.EmailID == model.Email).FirstOrDefault().CustomerID;
            model.CustomerNAME = db.Customers.Where(x => x.EmailID == model.Email).FirstOrDefault().CustomerName;

            model.Qty = 1;


            return View(model);
        }

     

        [HttpPost]
        public ActionResult Orderconfirm(OrderItemsModel model)
        {
            OrderItem item = new OrderItem();
            item.CatergoryID = model.CatergoryID;
            item.ProductID = model.ProductID;
            item.CustomerID = model.CustomerID;
            item.Qty = model.Qty;
            item.Discount = model.Discount;
            item.OrderTotal = model.OrderTotal;
            item.CreatedOn = DateTime.Now;
            item.CreatedBy = model.CustomerNAME;
            db.OrderItems.Add(item);
            db.SaveChanges();
            return View("Success");


        }
        public ActionResult Success()
        {
            return View();
        }

    }
}