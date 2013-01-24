﻿using Microsoft.Web.Mvc; //nuget:mvc4futures
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
    public static class NavigationRouteConfigurationExtensions
    {

        public static void MapNavigationRoute(this RouteCollection routes, string name, string url, object defaults,
                                              object constraints = null, AreaRegistrationContext context = null)
        {
            var newRoute = new NamedRoute(name, url, new MvcRouteHandler()) { Defaults = new RouteValueDictionary(defaults), Constraints = new RouteValueDictionary(constraints), Context = context};
            routes.Add(name, newRoute);
        }

        public static NavigationRouteBuilder MapNavigationRoute(this RouteCollection routes, string name, string displayName, string url, object defaults,
                                              object constraints = null, AreaRegistrationContext context = null)
        {
            var newRoute = new NamedRoute(name, displayName, url, new MvcRouteHandler()) { Defaults = new RouteValueDictionary(defaults), Constraints = new RouteValueDictionary(constraints), Context = context };
            routes.Add(name, newRoute);
            return new NavigationRouteBuilder(routes,newRoute);
        }

        public static void MapNavigationRoute(this RouteCollection routes, string name, string displayName, string url,
                                      object defaults,
                                      string[] namespaces,
                                      object constraints = null,
                                      AreaRegistrationContext context = null)
        {
            var newRoute = new NamedRoute(name, displayName, url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary(),
                Context = context
            };

            if (namespaces != null && namespaces.Length > 0)
                newRoute.DataTokens["Namespaces"] = namespaces;

            routes.Add(name, newRoute);
        }


        public static NavigationRouteBuilder MapNavigationRoute<T>(this RouteCollection routes, string displayName, Expression<Func<T, ActionResult>> action, AreaRegistrationContext context = null, string urlPrefix = "") where T : IController
        {
            var newRoute = new NamedRoute("", "", new MvcRouteHandler());
            newRoute.ToDefaultAction(action, context, urlPrefix);
            newRoute.DisplayName = displayName;
            routes.Add(newRoute.Name, newRoute);
            return new NavigationRouteBuilder(routes, newRoute);
        }

        public static NavigationRouteBuilder AddChildRoute<T>(this NavigationRouteBuilder builder, string DisplayText, Expression<Func<T, ActionResult>> action, AreaRegistrationContext context = null, string urlPrefix = "") where T : IController
        {
            var childRoute = new NamedRoute("", "", new MvcRouteHandler());
            childRoute.ToDefaultAction<T>(action, context, urlPrefix);
            childRoute.DisplayName = DisplayText;
            childRoute.IsChild = true;
            builder._parent.Children.Add(childRoute);
            builder._routes.Add(childRoute.Name,childRoute);
            return builder;
        }

        public static NamedRoute ToDefaultAction<T>(this NamedRoute route, Expression<Func<T, ActionResult>> action, AreaRegistrationContext context = null, string urlPrefix = "") where T : IController
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

            route.Defaults = LinkBuilder.BuildParameterValuesFromExpression(body) ?? new RouteValueDictionary();
            foreach (var pair in route.Defaults.Where(x => x.Value == null).ToList())
                route.Defaults.Remove(pair.Key);

            route.Defaults.Add("controller", controllerName);
            route.Defaults.Add("action", actionName);

            route.Url= CreateUrl(actionName,controllerName,urlPrefix);
            route.Name = String.Join("-", new string[] { "Navigation", urlPrefix, controllerName, actionName }.Where(s => !String.IsNullOrWhiteSpace(s)));

            if(route.DataTokens == null)
                route.DataTokens = new RouteValueDictionary();
            route.DataTokens.Add("Namespaces", new string[] {typeof (T).Namespace});

            route.Context = context;

            return route;
        }

        public static string CreateUrl(string actionName, string controllerName, string urlPrefix = "")
        {
            if (controllerName.Equals("home", StringComparison.CurrentCultureIgnoreCase) && actionName.Equals("index",StringComparison.CurrentCultureIgnoreCase))
            {
                return urlPrefix;
            }
            return String.Join("/", new string[] { urlPrefix, controllerName.ToLower(), actionName.ToLower() }.Where(s => !String.IsNullOrWhiteSpace(s)));
        }

    }
    public class NavigationRouteBuilder
    {
        public NavigationRouteBuilder(RouteCollection routes, NamedRoute parent)
        {
           
            _routes = routes;
            _parent = parent;
        }

        public RouteCollection _routes { get; set; }

        public NamedRoute _parent { get; set; }
    }
}
