using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public void Map_a_route_to_a_url_using_an_area()
        {
            var routes = new System.Web.Routing.RouteCollection();

            routes.MapNavigationRoute<HomeController>("Home", c => c.Index(), "", new NavigationRouteOptions{ AreaName = "Admin"});
            routes.MapNavigationRoute<HomeController>("About", c => c.About(), "", new NavigationRouteOptions { AreaName = "AdMin" });

            routes.Count.ShouldNotEqual(0);

            var uh = GetUrlHelper(routes);

            uh.RouteUrl("Navigation-Admin-Home-About").ShouldEqual("/admin/about");
            var r = (Route)routes["Navigation-Admin-Home-About"];
            r.DataTokens["area"].ToString().ShouldEqual("admin");
            
            uh.RouteUrl("Navigation-AdMin-Home-Index").ShouldEqual("/admin");
        }
        [Test]
        public void Map_a_route_to_a_url_matching_the_action_name()
        {
            var routes = new System.Web.Routing.RouteCollection();

            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());
            routes.MapNavigationRoute<HomeController>("About", c => c.About());

            routes.Count.ShouldNotEqual(0);

            var uh = GetUrlHelper(routes);

            uh.RouteUrl("Navigation-Home-About").ShouldEqual("/about");
            uh.RouteUrl("Navigation-Home-Index").ShouldEqual("/"); 
        }

        [Test]
        public void Map_home_index_to_the_site_root()
        {
            var routes = new System.Web.Routing.RouteCollection();

            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());
            routes.MapNavigationRoute<HomeController>("About", c => c.About());
            routes.MapNavigationRoute<HomeController>("Logout", c => c.Logout());

            routes.Count.ShouldNotEqual(0);

            var uh = GetUrlHelper(routes);
            uh.RouteUrl("Navigation-Home-Index").ShouldEqual("/");
        }
        [Test]
        public void Apply_filter_to_the_current_request()
        {
            var routes = RouteTable.Routes;
            var filter = new NullFilter();
            NavigationRoutes.NavigationRouteFilters.Filters.Add(filter);
            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());

            NavigationRoutes.NavigationViewExtensions.Navigation(null);

            filter._wasCalled.ShouldBeTrue();
        }
        [Test]
        public void should_apply_filters()
        {
            
            var filters = new List<INavigationRouteFilter>();
            filters.Add(new RemoveAuthorizeActions());
            
            var routes = new System.Web.Routing.RouteCollection();
            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());
            routes.MapNavigationRoute<HomeController>("Logout", c => c.Logout());

            var currentRoutes = NavigationRoutes.NavigationViewExtensions.GetRoutesForCurrentRequest(routes, filters);
            currentRoutes.Count().ShouldEqual(1);
        }

        [Test]
        public void should_contain_child_routes()
        {
            var routes = new System.Web.Routing.RouteCollection();
            routes.MapNavigationRoute<HomeController>("Home", c => c.Index())
            .AddChildRoute<HomeController>("Logout", c => c.Logout())
            .AddChildRoute<HomeController>("Foobar", c => c.ChangePassword());

            routes.Count().ShouldEqual(3);
            ((NamedRoute)routes["Navigation-Home-Index"]).Children.Count().ShouldEqual(2);
        }

        [Test]
        public void add_namespaces()
        {
            var routes = new System.Web.Routing.RouteCollection();
            routes.MapNavigationRoute("Home-navigation", "Home", "",
                                        defaults: new { controller = "Home", action = "Index" },
                                        namespaces: new[] { "UnitTests" });
            routes.Count().ShouldEqual(1);

            var namespaces = (string[]) ((NamedRoute) routes["Home-navigation"]).DataTokens["Namespaces"];
            namespaces.ShouldContain("UnitTests");
        }
        [Test]
        public void add_namespaces_for_controller()
        {
            var routes = new System.Web.Routing.RouteCollection();

            routes.MapNavigationRoute<HomeController>("Home", c => c.Index());

            routes.Count().ShouldEqual(1);
            var namespaces = (string[]) ((NamedRoute) routes["Navigation-Home-Index"]).DataTokens["Namespaces"];
            namespaces.ShouldContain("UnitTests");
        }

        [Test]
        public void add_filter_tokens()
        {
            var filterTokenText = "foo";
            var routes = new System.Web.Routing.RouteCollection();
            routes.MapNavigationRoute<HomeController>("Home", c => c.Index(), "", new NavigationRouteOptions { FilterToken = filterTokenText });

            Assert.IsTrue(((NamedRoute)routes["Navigation-Home-Index"]).DataTokens.HasFilterToken());

            var filterToken = (string)((NamedRoute)routes["Navigation-Home-Index"]).DataTokens.FilterToken();
            filterToken.ShouldEqual(filterTokenText);
        }
    }
}
