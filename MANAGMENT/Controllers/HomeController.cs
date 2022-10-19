using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Serilog;
using MANAGMENT;
using Serilog.Sinks.File;
using Serilog.Events;
using MANAGMENT.Models;
using MANAGMENT.Models.ViewModels;



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
        public ActionResult Details()
        {

            var model = db.Products.ToList();

            return View(model);
        }




    }
}