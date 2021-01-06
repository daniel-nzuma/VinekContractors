using System.Web.Optimization;

namespace Tracking
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/gmap3.js",
                      "~/Scripts/ini.isotope.js",
                      "~/Scripts/isotope.js",
                      "~/Scripts/jquery.js",
                      "~/Scripts/jquery.validate.js",
                      "~/Scripts/owl.carousel.min.js",
                      "~/Scripts/wow.min.js",
                      "~/Scripts/custom.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/colorbox.css",
                      "~/Content/fontawesome-free-5.13.0-web/css/all.css",
                      "~/Content/services_icons/flaticon.css",
                      "~/Content/owl.carousel.min.css",
                      "~/Content/owl.theme.default.min.css",
                      "~/Content/animate.css",
                      "~/Content/hover-min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/style.css",
                      "~/Content/layout.css",
                      "~/Content/responsive.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/dashboardScripts").Include(
                     "~/Scripts/jquery.min.js",
                     "~/Scripts/bootstrap.bundle.min.js",
                      "~/Scripts/jquery.easing.min.js",
                     "~/Scripts/sb-admin-2.min.js",
                     "~/Scripts/Chart.min.js",
                     "~/Scripts/chart-area-demo.js",
                     "~/Scripts/chart-pie-demo.js"));

        }
    }
}

 