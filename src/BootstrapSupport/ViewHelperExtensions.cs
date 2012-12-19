using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace BootstrapSupport
{
    public static class DefaultScaffoldingExtensions
    {
        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace("Controller", String.Empty);
        }

        public static string GetActionName(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression)actionExpression.Body).Method.Name;
        }

        public static PropertyInfo[] VisibleProperties(this IEnumerable Model)
        {
            var elementType = Model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = Model.GetType().GetGenericArguments()[0];
            }
            return elementType.GetProperties().Where(info => info.Name != elementType.IdentifierPropertyName()).ToArray();
        }

        public static PropertyInfo[] VisibleProperties(this Object model)
        {
            return model.GetType().GetProperties().Where(info => info.Name != model.IdentifierPropertyName()).ToArray();
        }

        public static RouteValueDictionary GetIdValue(this object model)
        {
            var v = new RouteValueDictionary();
            v.Add(model.IdentifierPropertyName(), model.GetId());
            return v;
        }

        public static object GetId(this object model)
        {
            return model.GetType().GetProperty(model.IdentifierPropertyName()).GetValue(model,new object[0]);
        }


        public static string IdentifierPropertyName(this Object model)
        {
            return IdentifierPropertyName(model.GetType());
        }

        public static string IdentifierPropertyName(this Type type)
        {
            if (type.GetProperties().Any(info => info.PropertyType.AttributeExists<System.ComponentModel.DataAnnotations.KeyAttribute>()))
            {
                return
                    type.GetProperties().First(
                        info => info.PropertyType.AttributeExists<System.ComponentModel.DataAnnotations.KeyAttribute>())
                        .Name;
            }
            else if (type.GetProperties().Any(p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)))
            {
                return
                    type.GetProperties().First(
                        p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)).Name;
            }
            return "";
        }

        public static string GetLabel(this PropertyInfo propertyInfo)
        {
            var meta = ModelMetadataProviders.Current.GetMetadataForProperty(null, propertyInfo.DeclaringType, propertyInfo.Name);
            return meta.GetDisplayName();
        }

        public static string ToSeparatedWords(this string value)
        {
            return Regex.Replace(value, "([A-Z][a-z])", " $1").Trim();
        }

    }

    public static class PropertyInfoExtensions
    {
        public static bool AttributeExists<T>(this PropertyInfo propertyInfo) where T : class
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false)
                                .FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static bool AttributeExists<T>(this Type type) where T : class
        {
            var attribute = type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static T GetAttribute<T>(this Type type) where T : class
        {
            return type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : class
        {
            return propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }
		
        public static string LabelFromType(Type @type)
        {
            var att = GetAttribute<DisplayNameAttribute>(@type);
            return att != null ? att.DisplayName 
                : @type.Name.ToSeparatedWords();
        }
		
        public static string GetLabel(this Object Model)
        {
            return LabelFromType(Model.GetType());
        }

        public static string GetLabel(this IEnumerable Model)
        {
            var elementType = Model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = Model.GetType().GetGenericArguments()[0];
            }
            return LabelFromType(elementType);
        }
    }

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString TryPartial(this HtmlHelper helper, string viewName, object model)
        {
            try
            {
                return helper.Partial(viewName, model);
            }
            catch (Exception)
            {
            }
            return MvcHtmlString.Empty;
        }
		
        // example usage:
        // <li>@Html.ActionLinkWithGlyphIcon(Url.Action("Index"),
        //                                   "Back to List",
        //                                   "icon-list")</li>
        // instead of "icon-list", we could also use GlyphIcons.list
        public static MvcHtmlString ActionLinkWithGlyphIcon(this HtmlHelper helper, 
            string action, 
            string text, 
            string glyphs,
            string tooltip = "",
            IDictionary<string, object> htmlAttributes = null)
        {
            var glyph = new TagBuilder("i");
            glyph.MergeAttribute("class", glyphs);

            var anchor = new TagBuilder("a");
            anchor.MergeAttribute("href", action);
            
            if(!string.IsNullOrEmpty(tooltip))
                anchor.MergeAttributes(
                    new Dictionary<string, object>()
                        {
                            { "rel", "tooltip" }, 
                            { "data-placement", "top" }, 
                            { "title", tooltip }
                        }
                    );

            if(htmlAttributes != null)
                anchor.MergeAttributes(htmlAttributes, true);

            anchor.InnerHtml = glyph + " " + text;

            return MvcHtmlString.Create(anchor.ToString());
        }
    }
	
    // helper class with all bootstrap icons
    public class GlyphIcons
    {
        public const string glass = "icon-glass";
        public const string music = "icon-music";
        public const string search = "icon-search";
        public const string envelope = "icon-envelope";
        public const string heart = "icon-heart";
        public const string star = "icon-star";
        public const string star_empty = "icon-star-empty";
        public const string user = "icon-user";
        public const string film = "icon-film";
        public const string th_large = "icon-th-large";
        public const string th = "icon-th";
        public const string th_list = "icon-th-list";
        public const string ok = "icon-ok";
        public const string remove = "icon-remove";
        public const string zoom_in = "icon-zoom-in";
        public const string zoom_out = "icon-zoom-out";
        public const string off = "icon-off";
        public const string signal = "icon-signal";
        public const string cog = "icon-cog";
        public const string trash = "icon-trash";
        public const string home = "icon-home";
        public const string file = "icon-file";
        public const string time = "icon-time";
        public const string road = "icon-road";
        public const string download_alt = "icon-download-alt";
        public const string download = "icon-download";
        public const string upload = "icon-upload";
        public const string inbox = "icon-inbox";
        public const string play_circle = "icon-play-circle";
        public const string repeat = "icon-repeat";
        public const string refresh = "icon-refresh";
        public const string list_alt = "icon-list-alt";
        public const string @lock = "icon-lock";
        public const string flag = "icon-flag";
        public const string headphones = "icon-headphones";
        public const string volume_off = "icon-volume-off";
        public const string volume_down = "icon-volume-down";
        public const string volume_up = "icon-volume-up";
        public const string qrcode = "icon-qrcode";
        public const string barcode = "icon-barcode";
        public const string tag = "icon-tag";
        public const string tags = "icon-tags";
        public const string book = "icon-book";
        public const string bookmark = "icon-bookmark";
        public const string print = "icon-print";
        public const string camera = "icon-camera";
        public const string font = "icon-font";
        public const string bold = "icon-bold";
        public const string italic = "icon-italic";
        public const string text_height = "icon-text-height";
        public const string text_width = "icon-text-width";
        public const string align_left = "icon-align-left";
        public const string align_center = "icon-align-center";
        public const string align_right = "icon-align-right";
        public const string align_justify = "icon-align-justify";
        public const string list = "icon-list";
        public const string indent_left = "icon-indent-left";
        public const string indent_right = "icon-indent-right";
        public const string facetime_video = "icon-facetime-video";
        public const string picture = "icon-picture";
        public const string pencil = "icon-pencil";
        public const string map_marker = "icon-map-marker";
        public const string adjust = "icon-adjust";
        public const string tint = "icon-tint";
        public const string edit = "icon-edit";
        public const string share = "icon-share";
        public const string check = "icon-check";
        public const string move = "icon-move";
        public const string step_backward = "icon-step-backward";
        public const string fast_backward = "icon-fast-backward";
        public const string backward = "icon-backward";
        public const string play = "icon-play";
        public const string pause = "icon-pause";
        public const string stop = "icon-stop";
        public const string forward = "icon-forward";
        public const string fast_forward = "icon-fast-forward";
        public const string step_forward = "icon-step-forward";
        public const string eject = "icon-eject";
        public const string chevron_left = "icon-chevron-left";
        public const string chevron_right = "icon-chevron-right";
        public const string plus_sign = "icon-plus-sign";
        public const string minus_sign = "icon-minus-sign";
        public const string remove_sign = "icon-remove-sign";
        public const string ok_sign = "icon-ok-sign";
        public const string question_sign = "icon-question-sign";
        public const string info_sign = "icon-info-sign";
        public const string screenshot = "icon-screenshot";
        public const string remove_circle = "icon-remove-circle";
        public const string ok_circle = "icon-ok-circle";
        public const string ban_circle = "icon-ban-circle";
        public const string arrow_left = "icon-arrow-left";
        public const string arrow_right = "icon-arrow-right";
        public const string arrow_up = "icon-arrow-up";
        public const string arrow_down = "icon-arrow-down";
        public const string share_alt = "icon-share-alt";
        public const string resize_full = "icon-resize-full";
        public const string resize_small = "icon-resize-small";
        public const string plus = "icon-plus";
        public const string minus = "icon-minus";
        public const string asterisk = "icon-asterisk";
        public const string exclamation_sign = "icon-exclamation-sign";
        public const string gift = "icon-gift";
        public const string leaf = "icon-leaf";
        public const string fire = "icon-fire";
        public const string eye_open = "icon-eye-open";
        public const string eye_close = "icon-eye-close";
        public const string warning_sign = "icon-warning-sign";
        public const string plane = "icon-plane";
        public const string calendar = "icon-calendar";
        public const string random = "icon-random";
        public const string comment = "icon-comment";
        public const string magnet = "icon-magnet";
        public const string chevron_up = "icon-chevron-up";
        public const string chevron_down = "icon-chevron-down";
        public const string retweet = "icon-retweet";
        public const string shopping_cart = "icon-shopping-cart";
        public const string folder_close = "icon-folder-close";
        public const string folder_open = "icon-folder-open";
        public const string resize_vertical = "icon-resize-vertical";
        public const string resize_horizontal = "icon-resize-horizontal";
        public const string hdd = "icon-hdd";
        public const string bullhorn = "icon-bullhorn";
        public const string bell = "icon-bell";
        public const string certificate = "icon-certificate";
        public const string thumbs_up = "icon-thumbs-up";
        public const string thumbs_down = "icon-thumbs-down";
        public const string hand_right = "icon-hand-right";
        public const string hand_left = "icon-hand-left";
        public const string hand_up = "icon-hand-up";
        public const string hand_down = "icon-hand-down";
        public const string circle_arrow_right = "icon-circle-arrow-right";
        public const string circle_arrow_left = "icon-circle-arrow-left";
        public const string circle_arrow_up = "icon-circle-arrow-up";
        public const string circle_arrow_down = "icon-circle-arrow-down";
        public const string globe = "icon-globe";
        public const string wrench = "icon-wrench";
        public const string tasks = "icon-tasks";
        public const string filter = "icon-filter";
        public const string briefcase = "icon-briefcase";
        public const string fullscreen = "icon-fullscreen";
    }
}