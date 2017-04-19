using System.Web;
using System.Web.Optimization;

namespace RPGSupport
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/libs/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/libs/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/libs/bootstrap.js",
                      "~/Scripts/libs/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/libs/angular.js",
                      "~/Scripts/libs/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/RPGSupport")
                .IncludeDirectory("~/Scripts/Angular/Controllers", "*.js")
                .IncludeDirectory("~/Scripts/Angular/Services", "*.js")
                .Include("~/Scripts/Angular/app.js"));
        }
    }
}
