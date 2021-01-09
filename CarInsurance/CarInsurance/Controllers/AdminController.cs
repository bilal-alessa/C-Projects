﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class AdminController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Admin
        public ActionResult Index()
        {
            using(InsuranceEntities db = new InsuranceEntities())
            {
                // using LINQ as Jesse described will help reduce the amount of code needed to query the db

                var insurees = (from c in db.Insurees
                                where c.Id > 0
                                select c).ToList();
                // instantiate a new object and call it by using the View Model for reference 
                var insureeVms = new List<Insuree>();

                foreach (Insuree insuree in insurees)
                {
                    var insureeVm = new Insuree();
                    insureeVm.Id = insuree.Id;
                    insureeVm.FirstName = insuree.FirstName;
                    insureeVm.LastName = insuree.LastName;
                    insureeVm.EmailAddress = insuree.EmailAddress;
                    insureeVm.Quote = insuree.Quote;
                    insureeVms.Add(insureeVm);
                }
                return View(insureeVms);
            }

        }
    }
}
