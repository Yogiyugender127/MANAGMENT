using MANAGMENT.Models;
using MANAGMENT.Models.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MANAGMENT.Controllers;
using MANAGMENT.Models.ViewModels;

namespace MANAGMENT.Controllers
{
    public class AdminController : Controller
    {
        CompanyDBEntities db = new CompanyDBEntities();
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Customer()
        {
            var customer = db.Customers.ToList();
            return View(customer);
        }

        public ActionResult Placeorder()
        {
            var product = db.OrderItems.ToList();
            return View(product);
        }



        public ActionResult Delete(int? id)

        {

            var product = db.Products.Find(id);
        

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Product", "Admin");
            //var product = db.OrderItems.Find(id);

            //db.OrderItems.Remove(product);
            //    db.SaveChanges();
            //    return RedirectToAction("Placeorder", "Admin");



        }
        public ActionResult Deletecustomer(int? id)

        {
           
                var customer = db.Customers.Find(id);

                db.Customers.Remove(customer);
               var result= db.SaveChanges();
            if(result==1)
            {
                var logincustomer = db.Logins.Where(x => x.UserName == customer.EmailID).ToList();
                var deletecustomer = db.Logins.Find(logincustomer.FirstOrDefault().Id);
                db.Logins.Remove(deletecustomer);
                db.SaveChanges();
            }

                return RedirectToAction("customer", "Admin");
            

                  
        }
        

        public ActionResult Edit(int? id)
        {
            var customer = db.Customers.Find(id);

            return View(customer);
        }
        [HttpPost]
        public ActionResult EditCustomer(Customer obj)
        {
           
         
            var customer = db.Customers.Where(s => s.CustomerID == obj.CustomerID).FirstOrDefault();
            obj.UpdatedBy = "Admin";
            obj.UpdatedOn = DateTime.Now;


            db.Customers.Remove(customer);
            db.Customers.Add(obj);
            db.SaveChanges();

            return RedirectToAction("Customer");
        }
        public ActionResult Product()
        {
            var product = db.Products.ToList();
            return View(product);

            //Context context = new Context();





            //        var grouped = db.GroupBy(x => new { x.Category })
            //.Select(x => new {
            //    Category = x.Key.Category,
            //    Products = x.ToList().Select(x => x.Product).ToList()
            //}); ;




        }

        public ActionResult Mobile()
        {
            var mobile = db.Mobiles.ToList();
            return View(mobile);
        }
        public ActionResult CreateMobile()
        {
            return View();
        }









        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public  ActionResult create(ProductModel product , HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var id = db.Products.ToList().Count();
                product.id = id + 109;


                if (file != null)
                {

                    string _filename = Path.GetFileName(file.FileName);

                    var extention = Path.GetExtension(_filename);
                    if (extention == ".jpg" || extention == ".jpeg")
                    {
                      
                        string _path = Path.Combine(Server.MapPath("~/Images"), product.id.ToString()+extention);
                        file.SaveAs(_path);
                        product.Image = "Images/" + product.id+extention;
                    }
                    else
                    {
                        ViewBag.msg = "Please .jpg or .jpeg files only";
                        return View();
                    }

                }
                else
                {
                    ViewBag.msg = "Please upload photo";
                    return View();
                }
                Product p = new Product();
                p.ProductID = product.id;
                p.DisplayName = product.Name;
                p.Image = product.Image;
                p.price = product.price;
                p.CreatedOn = DateTime.Now;
                p.CreatedBy = "Admin";
                p.UpdatedOn = product.UpdatedOn;
                if(product.CategoryName == "Mobiles")
                {
                    p.CategoryID = 111;
                }
                else
                {
                    p.CategoryID = 101;
                }
             
                p.Description = product.Description;

                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("product", "admin");
            }


            return View(product);
        }

        public ActionResult Editproduct(int? id)
        {
            var product = db.Products.Find(id);

            return View(product);
        }
        [HttpPost]
        public ActionResult Editproduct(Product obj)
        {


            var product = db.Products.Where(s => s.ProductID == obj.ProductID).FirstOrDefault();

            db.Products.Remove(product);
            db.Products.Add(obj);

            obj.UpdatedBy = "Admin";
            obj.UpdatedOn = DateTime.Now;
           


            db.SaveChanges();

            return RedirectToAction("Product");

        }


        public ActionResult EditOrder(int? id)
        {
            var order = db.OrderItems.Find(id);

            return View(order);
        }
        [HttpPost]
        public ActionResult EditOrder(OrderItem obj)
        {


            var product = db.OrderItems.Where(s => s.OrderID == obj.OrderID).FirstOrDefault();

            db.OrderItems.Remove(product);
            db.OrderItems.Add(obj);

            obj.UpdatedBy = "Admin";
            obj.UpdatedOn = DateTime.Now;



            db.SaveChanges();

            return RedirectToAction("Product");
        }
        public ActionResult Deleteorder(int? id)

        {

            var Order = db.OrderItems.Find(id);


            db.OrderItems.Remove(Order);
            db.SaveChanges();

            //var user = db.Logins.FirstOrDefault().Role == "Admin";



            //if (user == "Admin")
                //    {
                //        return RedirectToAction("Index", "Admin");
                //    }
                //    else
                //    {
                //        return RedirectToAction("Index", "Customer", new { username = user.FirstOrDefault().UserName });
                //    }
                //}

                //if(db.AspNetUserLogins=="Admin")
                return RedirectToAction("Placeorder", "Admin");
            //var product = db.OrderItems.Find(id);

            //db.OrderItems.Remove(product);
            //    db.SaveChanges();
            //    return RedirectToAction("Placeorder", "Admin");



        }









    }

}