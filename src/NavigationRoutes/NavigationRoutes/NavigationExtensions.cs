using Microsoft.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    public static class NavigationViewExtensions
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
    }

    public static class NavigationRouteConfigurationExtensions{
        
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


        public static void MapNavigationRoute<T>(this RouteCollection routes, string displayName, Expression<Func<T, ActionResult>> action) where T : IController
        {
            var newRoute = new NamedRoute("", "", new MvcRouteHandler());
            newRoute.ToDefaultAction(action);
            //newRoute.Constraints = new RouteValueDictionary(new { @namespace=typeof(T).Namespace});
            newRoute.DisplayName = displayName;
            routes.Add(newRoute.Name, newRoute);
        }

        public static NamedRoute ToDefaultAction<T>(this NamedRoute route, Expression<Func<T, ActionResult>> action) where T : IController
        {
            var body = action.Body as MethodCallExpression;

            if (body == null)
            {
                throw new ArgumentException("Expression must be a method call");
            }

            if (body.Object != action.Parameters[0])
            {
                throw new ArgumentException("Method call must target lambda argument");
            }

            string actionName = body.Method.Name;

            // check for ActionName attribute
            var attributes = body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
            if (attributes.Length > 0)
            {
                var actionNameAttr = (ActionNameAttribute)attributes[0];
                actionName = actionNameAttr.Name;
            }

            string controllerName = typeof(T).Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);
            }

            ;
            route.Defaults = LinkBuilder.BuildParameterValuesFromExpression(body) ?? new RouteValueDictionary();
            foreach (var pair in route.Defaults.Where(x => x.Value == null).ToList())
                route.Defaults.Remove(pair.Key);

            route.Defaults.Add("controller", controllerName);
            route.Defaults.Add("action", actionName);

            route.Url = actionName.ToLower();
            route.Name = "Navigation-" + controllerName + "-" + actionName;
            return route;
        }
    }
}