using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace NavigationRoutes
{
    public class NamedRoute : Route
    {
        string _name = string.Empty;
        string _displayName = string.Empty;
        string _navigationRoute = string.Empty;

        List<NamedRoute> _childRoutes = new List<NamedRoute>();

        public NamedRoute(string name, string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            _name = name;
        }

        public NamedRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
            _name = name;
        }

        public NamedRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            _name = name;
        }

        public NamedRoute(string name, string displayName, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            _name = name;
            _displayName = displayName;
        }

        public NamedRoute(string name, string displayName, string url, MvcRouteHandler routeHandler) : base(url, routeHandler)
        {
            _name = name;
            _displayName = displayName;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string DisplayName
        {
            get { return _displayName ?? _name; }
            set { _displayName = value; }
        }
        
        public List<NamedRoute> Children { get { return _childRoutes; } }
        public bool IsChild { get; set; }
        
        /// <summary>
        /// The ID to be rendered for the UL containing the navigation items. Ignored on child routes.
        /// If set, each distinct ID will be rendered as a seperate UL.
        /// </summary>
        public string NavigationGroup
        {
            get { return _navigationRoute; }
            set { _navigationRoute = value.Replace(" ", "-"); }
        }
        
        /// <summary>
        /// Optional settings to control the rendering behavior of the navigation route in the menu.
        /// </summary>
        public NavigationRouteOptions Options { get; set; }



    }
}