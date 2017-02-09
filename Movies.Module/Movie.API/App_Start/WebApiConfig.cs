using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace Movie.API
{
    using System.Net.Http.Formatting;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Users",
                routeTemplate: "MovieKeep/Users({id})",
                defaults: new { controller = "Users", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "Movies",
                routeTemplate: "MovieKeep/Users({userId})/MoviesOwned({id})",
                defaults: new { controller = "MoviesOwned", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "MovieTitles",
                routeTemplate: "MovieKeep/MovieTitles({id})",
                defaults: new { controller = "MovieTitles", id = RouteParameter.Optional });

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // Consumers will be JavaScript? This will make it easier for them to use camel case.
        }
    }
}
