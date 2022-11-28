using MANAGMENT.Models;
using MANAGMENT.Models.ViewModels;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;


namespace MANAGMENT.Controllers
{
    
    public class UserController : Controller
    {
        CompanyDBEntities db = new CompanyDBEntities();
        private Customer customer;

        // GET: User
        public ActionResult Index()
        {

            try
            {

            }
            catch (Exception ex)
            {

                Helper.WriteError(ex, "Error");
            }
            return View();
        }


        public ActionResult Test()
        {
            
            try
            {
                int i = 1;
                int j = 0;
                int l = i / j;
            }
            catch (Exception ex)
            {

                Helper.WriteError(ex, "Error");
            }
            return View("login");
        }

        //Get: Login page view
        [HttpGet]

        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                var user = db.Logins.Where(x => x.UserName == model.UserName && x.Password == model.Password).ToList();

                if (user.Count != 0)
                {
                    if (user.FirstOrDefault().Isactive == true)
                    {
                        if (user.FirstOrDefault().Role == "Admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Customer", new { username = user.FirstOrDefault().UserName });
                        }

                    }
                    else
                    {
                        var subject = "RE: Welcome to Mobile Mart";
                        var url = "https://localhost:44386/user/Changestatus?username=" + model.UserName;
                        var button = "<a href='" + url + "' class='btn btn-success'> Click Here</a>";

                        var html = "";
                        html += "<h1> Verify your email and login<br>";
                        html += "<br>";
                        html += "<br>";
                        html += "<br>";
                        html += "click to activate your account   " + button;

                        var body = html;
                        string emailID = model.UserName;

                        sendmail(emailID, subject, body);
                        return View("VerifyEmail");
                    }


                }
            }
            catch (Exception ex)
            {

                Helper.WriteError(ex, "Error");
            }
            
          
           
            return View("Error");

        }
        [HttpGet]

        public ActionResult Register()
        {
            try
            {
                ViewBag.Age = new SelectList(GetAge(), "Id", "Age");
            }
            catch (Exception ex)
            {

                //string error = ex.Message.ToString();
                Helper.WriteError(ex, "Error");

            }
           
            return View();

        }
        [HttpPost]
        public ActionResult Register(CustomerModelcs model)
        {

            try
            {



                ViewBag.Age = new SelectList(GetAge(), "Id", "Age");
                var usercount = db.Logins.Where(x => x.UserName == model.EmailID.ToUpper()).Count();
                if (usercount == 1)
                {
                    ViewBag.msg = "The Email Id already registered";

                }
                else
                {

                    
                    Customer customer = new Customer();



                    Random no = new Random();
                    int id = db.Customers.Count();
                    customer.CustomerID = id +1248+1;
                    model.CustomerID = customer.CustomerID;

                    //model.CustomerID = customer.CustomerID;
                     customer.CustomerID = model.CustomerID;


                    customer.CustomerName = model.CustomerName;
                    customer.Age = model.Age;
                    customer.EmailID = model.EmailID.ToUpper();
                    customer.CreatedBy = model.CustomerName;
                    customer.CreatedOn = System.DateTime.Now;
                    db.Customers.Add(customer);
                    var result = db.SaveChanges();


                    if (result == 1)
                    {
                        var user = new Login { UserName = model.EmailID.ToUpper(), Password = model.Password, Role = "Customer", Id = model.CustomerID,Isactive=false };
                        db.Logins.Add(user);
                        var register = db.SaveChanges();
                        if (register == 1)
                        {
                            var subject = "Registration Successfull.!";
                            var url = "https://localhost:44386/user/Changestatus?username="+customer.EmailID;
                            var button = "<a href='"+url+"' class='btn btn-success'> Click Here</a>";

                            var html = "";
                            html += "<h1> Hi" + model.CustomerName + "<br>";
                            html += "<br>";
                            html += "<br>";
                            html += "<br>";
                            html += "<table class='table table-bordered'>";
                            html += "<tr>";
                            html += "<td>";
                            html += "User Name";
                            html += "</td>";
                            html += "<td>";
                            html += "Password";
                            html += "</td>";
                            html += "<tr>";
                            html += "<tr>";
                            html += "<td>";
                            html += customer.EmailID;
                            html += "</td>";
                            html += "<td>";
                            html += model.Password;
                            html += "</td>";
                            html += "<tr>";
                            html += "</table>";
                            html += "<br>";
                            html += "<br>";
                            html += "<br>";

                            html += "click to activate your account   "+button;

                            var body = html;
                            string emailID = model.EmailID;

                            sendmail(emailID, subject, body);



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
            }
            catch (Exception e)

            { 

            }

            return View();
            
        }


        public ActionResult Changestatus( string username)
        {
            var user = db.Logins.Where(x => x.UserName == username);
            if (user.FirstOrDefault().Isactive == false)
            {
                user.FirstOrDefault().Isactive = true;
                db.SaveChanges();
            }

            return RedirectToAction("login");
        }
        private void sendmail(string emailID, string subject, string body)
        {
            string frommail = "yogiyugender127@gmail.com";
           // string pass = "Yogi@1995";
            string pass = "zvzemiknxwizdkbw";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(frommail);
            mail.To.Add(emailID);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(frommail, pass);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
           // Console.Write("Email Terkirim");
            //    //MailMessage message = new MailMessage();
            //    //SmtpClient smtp = new SmtpClient();
            //    //message.From = new MailAddress("upender.barama@lera.us");
            //    //message.To.Add(new MailAddress(emailID));
            //    //message.Subject = subject;
            //    //message.IsBodyHtml = true; //to make message body as html  
            //    //message.Body = body;
            //    //smtp.Port = 587;
            //    //smtp.Host = "smtp.office365.com"; //for gmail host  
            //    //smtp.EnableSsl = true;
            //    //smtp.UseDefaultCredentials = false;
            //    //smtp.Credentials = new NetworkCredential("upender.barama@lera.us", "Lera@321");
            //    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    //smtp.Send(message);
            //    Console.WriteLine("+++++++++++++++++++++++++++entered into sendEmailmethod+++++++++++++++++++++++++++++");
            //    MailMessage msg = new MailMessage();
            //    msg.From = new MailAddress("yogiyugender127@gmail.com");
            //    msg.To.Add(new MailAddress(emailID));
            //    msg.Subject = subject;
            //    Console.WriteLine("++++++++++++++++++++++attached subject+++++++++++++++++++++++++++++");
            //    //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            //    //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            //    msg.Body = body;
            //    //hbdwvtrvcpdocncs
            //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));

            //    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("yogiyugender127@gmail.com", "prsbmeiyyixpsqbo");
            //    Console.WriteLine("++++++++++++++++++++++validation successfull+++++++++++++++++++++++++++++");
            //    smtpClient.Credentials = credentials;
            //    smtpClient.UseDefaultCredentials = false;
            //    smtpClient.EnableSsl = true;
            //    smtpClient.Send(msg);
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

            for (int i = 18; i <= 20; i++)

            {
                var a = new AgeViewModel { Id = i, Age = i };

                ages.Add(a);

            }
            return ages;
        }
    }
}