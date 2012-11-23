using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Should;
using NUnit.Framework;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.IO;
using NavigationRoutes;
namespace UnitTests
{
    [TestFixture]
    public class Navigation_Route_Should:RouteTesterBase
    {
        [Test]
        public void Map_a_route_to_a_url_matching_the_action_name()
        {
            var routes = new System.Web.Routing.RouteCollection();

            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());
            routes.MapNavigationRoute<HomeController>("About", c => c.About());

            routes.Count.ShouldNotEqual(0);

            var uh = GetUrlHelper(routes);

            uh.RouteUrl("Navigation-Home-About").ShouldEqual("/about"); 
        }

        [Test]
        public void Map_home_index_to_the_site_root()
        {
            var routes = new System.Web.Routing.RouteCollection();

            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());
            routes.MapNavigationRoute<HomeController>("About", c => c.About());

            routes.Count.ShouldNotEqual(0);

            var uh = GetUrlHelper(routes);
            uh.RouteUrl("Navigation-Home-Index").ShouldEqual("/");
        }
    }
}
