using System.Web;
using System.Web.Optimization;

namespace ApexIelts
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/jquery.min.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/animsition.js",
                      "~/Scripts/waypoint.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/parallax.js",
                      "~/Scripts/matchMedia.js",
                      "~/Scripts/fitvids.js",
                      "~/Scripts/easing.js",
                      "~/Scripts/countTo.js",
                      "~/Scripts/owl.carousel.min.js",
                      "~/Scripts/cubeportfolio.js",
                      "~/Scripts/magnific.popup.min.js",
                      "~/Scripts/shortcodes.js",
                      "~/Scripts/main.js",
                      
                      "~/Scripts/responsiveslides.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/color-1.css",
                      "~/Content/responsiveslides.css"));
        }
    }
}
