using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using HackerNews.Service;
namespace HackerNew.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var container = new Container();

            // Make the container registrations, example:
            container.Register<IHackerNewsRestService, HackerNewsRestService>();

            container.RegisterWebApiControllers(config);

            // Create a new SimpleInjectorDependencyResolver that wraps the,
            // container, and register that resolver in MVC.

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();


            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
