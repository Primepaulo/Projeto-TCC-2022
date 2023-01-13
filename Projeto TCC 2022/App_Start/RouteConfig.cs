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
                name: "Administrador",
                url: "Admin/{action}/{id}",
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Orçamento",
                url: "Orçamento/{action}/{id}",
                new { controller = "Orçamento", action = "HistoricoOrçamentos", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Serviços",
                url: "Serviços/{action}/{id}",
                new { controller = "Serviços", action = "VisualizarServiços", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Avaliação",
                url: "Avaliação/{action}/{id}",
                new { controller = "Avaliação", action = "VisualizarAvaliações", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Carros",
                url: "Carros/{action}/{id}",
                new { controller = "Carros", action = "VisualizarCarros", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Cadastro",
                url: "Cadastro/{action}/{id}",
                new { controller = "Cadastro", action = "Cadastro", id = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "Imagem",
                url: "Imagem/{action}/{id}",
                new { controller = "Imagem", action = "MudarImagem", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Oficina",
                url: "Oficina/{action}/{id}",
                new { controller = "Oficina", action = "Page", id = UrlParameter.Optional }
               );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
