using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NavigationRoutes;

namespace NavigationRouteFilterExamples
{
    public class AdministrationRouteFilter :INavigationRouteFilter
    {
        // an excercise for the reader would be to load the role name 
        // from your config file so this isn't compiled in, or add a constructor
        // that accepts a role name to use to make this a more generic filter
        private string AdministrationRole = "admin";

        public bool ShouldRemove(System.Web.Routing.Route navigationRoutes)
        {
            if (navigationRoutes.DataTokens.HasFilterToken())
            {
                var filterToken = navigationRoutes.DataTokens.FilterToken();
                var result = !HttpContext.Current.User.IsInRole(AdministrationRole) && filterToken == AdministrationRole;
                return result;
            }

            return false;

        }
    }
}