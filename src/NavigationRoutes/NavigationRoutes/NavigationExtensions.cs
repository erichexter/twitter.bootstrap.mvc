﻿using Microsoft.Web.Mvc;
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

    public static class NavigationRoutes
    {
        public static List<INavigationRouteFilter> Filters=new List<INavigationRouteFilter>();
    }
    public static class NavigationViewExtensions
    {
                public static IHtmlString Breadcrumbs(this HtmlHelper helper)
        {
            var namedRoute = helper.ViewContext.RouteData.Route as NamedRoute;
            if (namedRoute != null)
            {
                var breadcrumbs = new TagBuilder("ul");
                breadcrumbs.AddCssClass("breadcrumb");
                breadcrumbs.InnerHtml = BuildBreadcrumbTrail(namedRoute, helper).ToString();
                return new HtmlString(breadcrumbs.ToString(TagRenderMode.Normal));
            }
            return new HtmlString(String.Empty);
        }
        
        private static IHtmlString BuildBreadcrumbTrail(NamedRoute namedRoute, HtmlHelper helper)
        {
            var li = new TagBuilder("li");
            var routeLink = helper.RouteLink(namedRoute.DisplayName, namedRoute.Name);
            li.InnerHtml = string.Format("{0}<span class=\"divider\"> / </span>", routeLink);
            var breadcrumbTrailPart = new HtmlString(li.ToString(TagRenderMode.Normal));
            if (namedRoute.Parent == null) return breadcrumbTrailPart;
            return new HtmlString(string.Format("{0}{1}", BuildBreadcrumbTrail(namedRoute.Parent, helper), breadcrumbTrailPart));
        }

        public static IHtmlString Navigation(this HtmlHelper helper)
        {
            return new CompositeMvcHtmlString(
                GetRoutesForCurrentRequest(RouteTable.Routes,NavigationRoutes.Filters).Select(namedRoute => helper.NavigationListItemRouteLink(namedRoute)));
        }

        public static IEnumerable<NamedRoute> GetRoutesForCurrentRequest(RouteCollection routes,IEnumerable<INavigationRouteFilter> routeFilters)
        {
            var navigationRoutes = routes.OfType<NamedRoute>().Where(r=>r.IsChild==false).ToList();
            if (routeFilters.Count() > 0)
            {
                foreach (var route in navigationRoutes.ToArray())
                {
                    foreach (var filter in routeFilters)
                    {
                        if (filter.ShouldRemove(route))
                        {
                            navigationRoutes.Remove(route);
                            break;
                        }
                    }
                }
            }
            return navigationRoutes;
        }

        public static MvcHtmlString NavigationListItemRouteLink(this HtmlHelper html, NamedRoute route)
        {
            var li = new TagBuilder("li")
                {
                    InnerHtml = html.RouteLink(route.DisplayName, route.Name).ToString()
                };
            
            if (CurrentRouteMatchesName(html, route.Name))
            {
                li.AddCssClass("active");
            }
            if (route.Children.Count() > 0)
            {
                //TODO: create a UL of child routes here.
                li.AddCssClass("dropdown");
                li.InnerHtml = "<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">" + route.DisplayName +"<b class=\"caret\"></b></a>";
                var ul = new TagBuilder("ul");
                ul.AddCssClass("dropdown-menu");
                
                foreach (var child in route.Children)
                {
                    var childLi = new TagBuilder("li");
                    childLi.InnerHtml = html.RouteLink(child.DisplayName, child.Name).ToString();
                    ul.InnerHtml += childLi.ToString();
                }
                //that would mean we need to make some quick
                
                li.InnerHtml = "<a href='#' class='dropdown-toggle' data-toggle='dropdown'>"+route.DisplayName + " <b class='caret'></b></a>" + ul.ToString();
                
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

   
}