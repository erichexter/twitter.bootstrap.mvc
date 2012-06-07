using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace $rootnamespace$
{
    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-1.*",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js",
                "~/Scripts/bootstrap.min.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.min.css",
                "~/Content/QD.css"
                ));
        }
    }
}