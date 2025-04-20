using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Huellitas.Handler;

namespace Huellitas
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.MessageHandlers.Add(new TokenValidationHandler());
            // Configuración y servicios de Web API

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
