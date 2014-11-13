using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace BootstrapSupport.HtmlHelpers
{
    public static class ToggleButtonHelperExtensions
    {
        // This can be treated as checkbox on the server side.
        // example usage:
        // @Html.ToggleButton("button1","btn btn-info")

        // Add on BootstrapBundleConfig
        // "~/Scripts/bootstrap.toggle.js",

        public static MvcHtmlString ToggleButton(this HtmlHelper helper,
            string id, string caption, bool value, string activeClass="btn-info")
        {
            var button= new TagBuilder("div");
            button.AddCssClass("btn");
            button.Attributes.Add("bshelper", "toggle");
            button.Attributes.Add("bshelper_act", activeClass);
            button.SetInnerText(caption);
            if (value)
            {
                button.AddCssClass(activeClass);
            }

            var check= new TagBuilder("input");
            check.Attributes.Add("type", "checkbox");
            check.Attributes.Add("style", "visibility:hidden;");
            check.Attributes.Add("id", id);
            check.Attributes.Add("name", id);
            check.Attributes.Add("value", "true");
            if (value)
            {
                check.Attributes.Add("checked", "");
            }


            var tag= button.ToString(TagRenderMode.SelfClosing) + check.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(button.ToString(TagRenderMode.Normal) + check.ToString(TagRenderMode.SelfClosing) );
        }

        public static MvcHtmlString ToggleButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression) where TModel : class
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var result = ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model );
            var dispName = string.IsNullOrEmpty(metadata.DisplayName) ? metadata.PropertyName : metadata.DisplayName;

            return htmlHelper.ToggleButton(metadata.PropertyName, dispName, (bool)result);
        }

    }
}
