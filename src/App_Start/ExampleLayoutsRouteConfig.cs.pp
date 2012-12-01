using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NavigationRoutes;

namespace $rootnamespace$
{
    public class ExampleLayoutsRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapNavigationRoute("starter", "Starter", "ExampleLayouts/Starter", new {controller = "ExampleLayouts", action = "Starter"});
            routes.MapNavigationRoute("marketing", "Marketing Site", "ExampleLayouts/Marketing", new { controller = "ExampleLayouts", action = "Marketing" });
            routes.MapNavigationRoute("fluid", "Fluid", "ExampleLayouts/Fluid", new { controller = "ExampleLayouts", action = "Fluid" });
            routes.MapNavigationRoute("narrow", "Narrow Fluid", "ExampleLayouts/Narrow", new { controller = "ExampleLayouts", action = "Narrow" });
            routes.MapNavigationRoute("signin", "Sign In", "ExampleLayouts/SignIn", new { controller = "ExampleLayouts", action = "SignIn" });
            routes.MapNavigationRoute("stickyfooter", "Sticky Footer", "ExampleLayouts/StickyFooter", new { controller = "ExampleLayouts", action = "StickyFooter" });
            routes.MapNavigationRoute("carousel", "Carousel", "ExampleLayouts/Carousel", new { controller = "ExampleLayouts", action = "Carousel" });
        }
    }
}
