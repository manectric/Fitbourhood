using System.Web;
using System.Web.Optimization;

namespace Fitbourhood
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/clockpicker.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/clockpicker.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/fontello").Include(
                "~/Content/css/animation.css",
                "~/Content/css/fontello-codes.css",
                "~/Content/css/fontello-embedded.css",
                "~/Content/css/fontello-ie7-codes.css",
                "~/Content/css/fontello-ie7.css",
                "~/Content/css/fontello.css"));
        }
    }
}
