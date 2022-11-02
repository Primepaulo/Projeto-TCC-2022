using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Projeto_TCC_2022
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Carros",
                url: "{controller}/{action}/{id}",
                new { controller = "Carros", action = "VisualizarCarros", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Cadastro",
                url: "{controller}/{action}/{id}",
                new { controller = "Cadastro", action = "Cadastro", id = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
