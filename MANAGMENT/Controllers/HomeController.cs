using MANAGMENT.Models;
using MANAGMENT.Models.ViewModels;
using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Serilog;
using static System.Net.WebRequestMethods;


using System.IO;


namespace MANAGMENT.Controllers
{
    public class HomeController : Controller
    {

        CompanyDBEntities db = new CompanyDBEntities();

        //public ActionResult Index()
        //{ 
        //    return View();
        //}
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
           
                try
                {
                    Helper.WriteDebug(null, "Debug ");
                    Helper.WriteWarning(null, "Warning ");
                throw new NotImplementedException();
            }
                catch (Exception e)
                {
                    Helper.WriteError(e, "Error");
                    Helper.WriteFatal(e, "Fatal");
                    Helper.WriteVerbose(e, "Verbose");
                throw;
            }




            return View();
        }
       public ActionResult ProductDetails( )


        {

            var product = db.Products.ToList();

            //  if(category.CategoryName=="Mobiles")
            //  { 

            //var x = db.Products.Where(a => a.CategoryID ==105).ToList();
            //      return View(x);

            //  }
            //  else
            //  {
            //    var b=  db.Products.Where(c => c.CategoryID == 111).ToList();
            //      return View(b);
            //  }


            return View(product);


        }
        public ActionResult ViewDetails(int? id)
        {

            TempData["productid"] = id;
            var item = db.Products.Find(id);
            return View(item);
        }
        public ActionResult carousel()
        {
            throw new Exception("Something went wrong");
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
            var customername = db.Customers.Where(x => x.CustomerID == model.CustomerID).FirstOrDefault().EmailID;
            return RedirectToAction("Index", "Customer", new { username = customername });
            //return View("Success");


            //return View("ProductDetails");


        }
        public ActionResult Success()
        {
            return View();
        }

    }
}