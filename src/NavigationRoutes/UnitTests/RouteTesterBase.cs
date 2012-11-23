using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UnitTests
{
    public class RouteTesterBase
    {
        public UrlHelper GetUrlHelper( RouteCollection routeCollection,string fileName = "/",string url = "http://localhost",string queryString = "")
        {
            //Make a request context
            var request = new HttpRequest(fileName, url, queryString);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, response);
            var httpContextBase = new HttpContextWrapper(httpContext);

            // Make the UrlHelper with empty route data
            var requestContext = new RequestContext(httpContextBase, new RouteData());
            return new UrlHelper(requestContext, routeCollection);
        }

    }
}
