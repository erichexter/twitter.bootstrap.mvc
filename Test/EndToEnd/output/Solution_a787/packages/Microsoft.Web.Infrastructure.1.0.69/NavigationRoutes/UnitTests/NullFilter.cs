using NavigationRoutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace UnitTests
{
    public class NullFilter:INavigationRouteFilter
    {
        public  bool _wasCalled=false;
        public bool ShouldRemove(Route navigationRoutes)
        {
            _wasCalled = true;
            return false;
        }
    }
}
