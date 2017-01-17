using LiveToLift.Data;
using LiveToLift.Models;
using LiveToLift.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiveToLift.Web.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Exercises.Add(new Exercise() { Name = "test" });
            db.SaveChanges();

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
