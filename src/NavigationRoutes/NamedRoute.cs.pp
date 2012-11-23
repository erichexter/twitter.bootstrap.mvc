using System.Web.Mvc;
using System.Web.Routing;

namespace NavigationRoutes
{
    public class NamedRoute : Route
    {
        readonly string _name;
        readonly string _displayName;
        readonly string _role;

        public NamedRoute(string name, string url, IRouteHandler routeHandler, string role = null)
            : base(url, routeHandler)
        {
            _name = name;
            _role = role;
        }

        public NamedRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          IRouteHandler routeHandler, string role = null)
            : base(url, defaults, constraints, routeHandler)
        {
            _name = name;
            _role = role;
        }

        public NamedRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          RouteValueDictionary dataTokens, IRouteHandler routeHandler, string role = null)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            _name = name;
            _role = role;
        }

        public NamedRoute(string name, string displayName, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          RouteValueDictionary dataTokens, IRouteHandler routeHandler, string role = null)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            _name = name;
            _displayName = displayName;
            _role = role;
        }

        public NamedRoute(string name, string displayName, string url, MvcRouteHandler routeHandler, string role = null)
            : base(url, routeHandler)
        {
            _name = name;
            _displayName = displayName;
            _role = role;
        }

        public string Name
        {
            get { return _name; }
        }

        public string DisplayName
        {
            get { return _displayName ?? _name; }
        }

        public string Role
        {
            get { return _role; }
        }
    }
}