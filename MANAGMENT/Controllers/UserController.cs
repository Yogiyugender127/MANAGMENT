using MANAGMENT.Models;
using MANAGMENT.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MANAGMENT.Controllers
{
    
    public class UserController : Controller
    {
        CompanyDBEntities db = new CompanyDBEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //Get: Login page view
        [HttpGet]

        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var user = db.Logins.Where(x => x.UserName == model.UserName && x.Password == model.Password).ToList();
            
            if(user.Count!=0)
            {
                if(user.FirstOrDefault().Role=="Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Customer", new { username = user.FirstOrDefault().UserName });
                }
            }
           
            return View("Error");

        }
        [HttpGet]

        public ActionResult Register()
        {
            ViewBag.Age = new SelectList(GetAge(), "Id", "Age");
            return View();

        }
        [HttpPost]
        public ActionResult Register(CustomerModelcs model)
        {
            ViewBag.Age = new SelectList(GetAge(), "Id", "Age");
            var usercount = db.Logins.Where(x => x.UserName == model.EmailID.ToUpper()).Count();
            if (usercount == 1)
            {
                ViewBag.msg = "The Email Id already registered";
            }
            else
            {

                int id = db.Customers.Count();
                Customer customer = new Customer();
                customer.CustomerID = id + 1191;
                model.CustomerID = customer.CustomerID;
                customer.CustomerName = model.CustomerName;
                customer.Age = model.Age;
                customer.EmailID = model.EmailID.ToUpper();
                customer.CreatedBy = model.CustomerName;
                customer.CreatedOn = System.DateTime.Now;
                db.Customers.Add(customer);
                var result = db.SaveChanges();
                if (result == 1)
                {
                    var user = new Login { UserName = model.EmailID.ToUpper(),Password=model.Password,Role="Customer",Id=model.CustomerID };
                     db.Logins.Add(user);
                    var register = db.SaveChanges();
                    if(register==1 )
                    {

                        return RedirectToAction("Success", new { UserName = model.CustomerName });
                    }
                    else
                    {
                        var dbcustomer = db.Customers.Find(model.CustomerID);
                        db.Customers.Remove(customer);
                        db.SaveChanges();

                    }
                }
            }

            return View();

        }
        public ActionResult Success(string UserName)
        {
            // UserName = "hi";
            ViewBag.UserName = UserName;
            return View();
        }
        private List<AgeViewModel> GetAge()
        {
            List<AgeViewModel> ages = new List<AgeViewModel>();

            for (int i = 18; i <= 100; i++)

            {
                var a = new AgeViewModel { Id = i, Age = i };

                ages.Add(a);

            }
            return ages;
        }
    }
}