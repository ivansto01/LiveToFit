using Autofac;
using Autofac.Integration.WebApi;
using LiveToLift.Data;
using LiveToLift.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace LiveToLift.Web.App_Start
{
    internal class AutofacWebAPI
    {

        public static void Initialize(HttpConfiguration config)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(
                RegisterServices(new ContainerBuilder())
            );
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {


            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            builder.RegisterType<UowData>().As<IUowData>().InstancePerRequest();

            //deal with your dependencies here
            // builder.RegisterType<PostProvider>().As<IPostProvider>().InstancePerRequest();
            // Register services
            builder.RegisterAssemblyTypes(typeof(IFitnessProgramService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            ////To give only one instance for the cache
            //builder.RegisterType<RedisCacheService>().As<ICache>().SingleInstance();

            // Register dependencies in controllers
            builder.RegisterApiControllers(typeof(Startup).Assembly);


            return builder.Build();
        }
    }
}