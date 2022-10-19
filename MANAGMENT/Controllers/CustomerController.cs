﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MANAGMENT.Models;

using MANAGMENT.Models.ViewModels;

namespace MANAGMENT.Controllers
{
    private CompanyDBEntities db = new CompanyDBEntities();
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(string username)
        {
            ViewBag.customer = username;
            return View();
        }
        public ActionResult Details()
        {

            var model = db.Products.ToList();

            return View(model);
        }



    }
}