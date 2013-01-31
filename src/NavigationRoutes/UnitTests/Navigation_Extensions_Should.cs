using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NavigationRoutes;
using System.Web.Routing;
using Should;

namespace UnitTests
{
    [TestFixture]
    public class Navigation_Extensions_Should: RouteTesterBase
    {
        [Test]
        public void produce_a_proper_url_for_home_index()
        {            
            var routes = new RouteCollection();
            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());
            var helper = new HtmlHelper(new TestViewContext(), new TestViewDataContainer(), routes);

            var result = helper.NavigationListItemRouteLink(routes["Navigation-Home-Index"] as NamedRoute).ToString();

            result.ShouldContain("href=\"/\"");
        }

        [Test]
        public void produce_a_proper_url_for_home_about()
        {
            var routes = new RouteCollection();
            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());
            routes.MapNavigationRoute<HomeController>("Home", c => c.About());
            var helper = new HtmlHelper(new TestViewContext(), new TestViewDataContainer(), routes);

            var result = helper.NavigationListItemRouteLink(routes["Navigation-Home-About"] as NamedRoute).ToString();

            result.ShouldContain("href=\"/home/about\"");
        }
    }
}
