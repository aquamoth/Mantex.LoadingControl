using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Mantex.LoadingControl
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api1/{action}/{id}",
                defaults: new { controller = "Api1", id = RouteParameter.Optional }
            );

			config.Formatters.Add(new Mantex.LoadingControl.BrowserJsonFormatter());
		}
    }
}
