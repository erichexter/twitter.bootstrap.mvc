using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace NavigationRoutes
{
    public class CompositeMvcHtmlString : IHtmlString
    {
        readonly IEnumerable<IHtmlString> _strings;

        public CompositeMvcHtmlString(IEnumerable<IHtmlString> strings)
        {
            _strings = strings;
        }

        public string ToHtmlString()
        {
            return string.Join(string.Empty, _strings.Select(x => x.ToHtmlString()));
        }
    }

    public static class NavigationExtensions
    {
        public static IHtmlString Navigation(this HtmlHelper helper)
        {
            return new CompositeMvcHtmlString(
                RouteTable.Routes.OfType<NamedRoute>().Select(
                    namedRoute => helper.NavigationListItemRouteLink(namedRoute.DisplayName, namedRoute.Name)));
        }

        public static MvcHtmlString NavigationListItemRouteLink(this HtmlHelper html, string linkText, string routeName)
        {
            var li = new TagBuilder("li")
                {
                    InnerHtml = html.RouteLink(linkText, routeName).ToString()
                };

            if (CurrentRouteMatchesName(html, routeName))
            {
                li.AddCssClass("active");
            }

            return MvcHtmlString.Create(li.ToString(TagRenderMode.Normal));
        }

        static bool CurrentRouteMatchesName(HtmlHelper html, string routeName)
        {
            var namedRoute = html.ViewContext.RouteData.Route as NamedRoute;
            if (namedRoute != null)
            {
                if (string.Equals(routeName, namedRoute.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static void MapNavigationRoute(this RouteCollection routes, string name, string url, object defaults,
                                              object constraints = null)
        {
            var newRoute = new NamedRoute(name, url, new MvcRouteHandler())
                {Defaults = new RouteValueDictionary(defaults), Constraints = new RouteValueDictionary(constraints)};
            routes.Add(name, newRoute);
        }

        public static void MapNavigationRoute(this RouteCollection routes, string name, string displayName, string url, object defaults,
                                              object constraints = null)
        {
            var newRoute = new NamedRoute(name, displayName, url, new MvcRouteHandler()) { Defaults = new RouteValueDictionary(defaults), Constraints = new RouteValueDictionary(constraints) };
            routes.Add(name, newRoute);
        }
    }
}