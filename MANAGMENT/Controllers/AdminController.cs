using MANAGMENT.Models;
using MANAGMENT.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer user = db.Customers.Find(id);
           
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult Delete(int? id)

        {
           
                var product = db.Products.Find(id);
           // var pid = db.Orders.Where(x => x.ProductID == product.ProductID).ToList();
           // var orderpid = db.Orders.Find(pid.FirstOrDefault().OrderID);
           // db.Orders.Remove(orderpid);
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("product", "Admin");
            

                  
        }
        public ActionResult Deletecustomer(int? id)

        {
           
                var customer = db.Customers.Find(id);
          //  var ordercustomer = db.Orders.Where(x=>x.CustomerID==customer.CustomerID).FirstOrDefault();
           // var ordercustomer = db.Orders.Find(customer.CustomerID);
         //   db.Customers.Remove(ordercustomer);
         
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

        public ActionResult Edit(int ?id)
        {
            //here, get the student from the database in the real application

            //getting a student from collection for demo purpose
            var customer = db.Customers.Find(id);

            return View(customer);
        }
        [HttpPost]
        public ActionResult EditCustomer(Customer obj)
        {
            //here, get the student from the database in the real application

            //getting a student from collection for demo purpose
            //db.Customers.Remove(Customer);
            var customer = db.Customers.Where(s => s.CustomerID == obj.CustomerID).FirstOrDefault();

            //var student = studentList.Where(s => s.StudentId == std.StudentId).FirstOrDefault();

            db.Customers.Remove(customer);
            db.Customers.Add(obj);
            db.SaveChanges();

            return RedirectToAction("Customer");
        }






        public ActionResult Product()
        {
            var product = db.Products.ToList();
            return View(product);
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
                product.id = id + 100;


                if (file != null)
                {

                    string _filename = Path.GetFileName(file.FileName);

                    var extention = Path.GetExtension(_filename);
                    if (extention == ".jpg" || extention == ".jpeg")
                    {
                        // if(_filename.Length)
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
                p.CategoryID = 111;
                p.Description = product.Description;

                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("product", "admin");
            }


            return View(product);
        }

    }

}