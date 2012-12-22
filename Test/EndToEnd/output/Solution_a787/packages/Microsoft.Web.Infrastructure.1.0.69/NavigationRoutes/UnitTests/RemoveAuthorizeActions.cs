using NavigationRoutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;

namespace UnitTests
{
    /// <summary>
    /// this is a contrived sample for unit testing only.
    /// </summary>
    public class RemoveAuthorizeActions:INavigationRouteFilter
    {
        public bool ShouldRemove(Route route)
        {
            
                var controllername = route.Defaults["controller"];
                var actionname = route.Defaults["action"].ToString();

                var type = GetType().Assembly.GetTypes().Where(t => t.Name.Equals(controllername + "Controller") ).FirstOrDefault();
                var action = type.GetMethod(actionname);
                if (action.GetCustomAttributes(true).Any(a =>  a.GetType() == typeof(AuthorizeAttribute)))
                {
                    return false;
                }            
                return true;            
        }
        
    }
}
