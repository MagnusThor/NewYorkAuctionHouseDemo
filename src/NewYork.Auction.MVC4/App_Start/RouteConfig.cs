using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewYork.AuctionHouse.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        routes.MapRoute(
        "Fallback",
        "{controller}/{action}",
        new {controller = "Fallback", action = "Init"},
        new[] { "XSockets.Live.MVC4.Fallback" });
        }
    }
}