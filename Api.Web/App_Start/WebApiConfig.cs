using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Api.Web.Controllers;
//using WebApiContrib.Formatting.Xlsx;
//using System.Web.Http.ExceptionHandling;
//using Elmah.Contrib.WebApi;
//using System.Diagnostics.CodeAnalysis;

namespace Api.Web
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.MessageHandlers.Add(new TokenValidationHandler());
            // Web API routes
            config.MapHttpAttributeRoutes();

            GlobalConfiguration.Configuration.EnableCors();

            //config.Formatters.Add(new XlsxMediaTypeFormatter());
            //config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

            config.Routes.MapHttpRoute(
                name: "Error404",
                routeTemplate: "api/{*url}",
                defaults: new { controller = "Error", action = "Handle404" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}