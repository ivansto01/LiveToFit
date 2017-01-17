using LiveToLift.Data;
using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LiveToLift.Models;
using LiveToLift.Data.Migrations;
using LiveToLift.Web.App_Start;
using LiveToLift.Web.Infrastructure.Mapping;

//using LiveToLift.Data.Migrations;

namespace LiveToLift.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            AutofacWebAPI.Initialize(GlobalConfiguration.Configuration);

            AutoMapperConfig.Execute();

        }
    }
}
