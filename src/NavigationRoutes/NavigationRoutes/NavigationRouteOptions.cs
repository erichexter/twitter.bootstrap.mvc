using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NavigationRoutes
{
    public class NavigationRouteOptions
    {
        public NavigationRouteOptions()
        {
            this.AreaName = string.Empty;
            this.HasBreakAfter = false;
            this.IsRightAligned = false;
            this.FilterToken = string.Empty;
            this.ElementId = string.Empty;
        }

        /// <summary>
        /// You can specify an area to properly render your route, if required
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// This will be the ID rendered in the DOM, if specified
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// This will insert a break after the menu item rendered for the route. If this is a top-level
        /// item, the break will be in the nav bar. For child items, the break appears in the dropdown list.
        /// </summary>
        public bool HasBreakAfter { get; set; }

        /// <summary>
        /// Setting this property on any item will cause the UL containing the entire group to be
        /// pulled to right.
        /// </summary>
        public bool IsRightAligned { get; set; }

        /// <summary>
        /// A string value to store additional information about the route for the purposes of
        /// filtering. The value is stored in the DataTokens collection and can be accessed in
        /// an implementation of INavigationRouteFilter.
        /// </summary>
        public string FilterToken { get; set; }

        // todo: figure out other relevant options
        // ? public bool HasBreakBefore { get; set; }
        // ? public string Icon { get; set; }
        
    }
}